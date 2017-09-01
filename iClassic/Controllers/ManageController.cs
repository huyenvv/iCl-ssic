﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using iClassic.Models;
using iClassic.Helper;
using PagedList;
using iClassic.Services.Implementation;
using log4net;
using System.Net;

namespace iClassic.Controllers
{
    [Override.Authorize(RoleList.Admin, RoleList.SupperAdmin)]
    public class ManageController : BaseController
    {
        private readonly ILog _log;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private UsersRepository _userRepository;
        private BranchRepository _branchRepository;

        public ManageController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _userRepository = new UsersRepository(_entities);
            _branchRepository = new BranchRepository(_entities);
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            UserManager = userManager;
            SignInManager = signInManager;
            _userRepository = new UsersRepository(_entities);
            _branchRepository = new BranchRepository(_entities);
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        public ActionResult Employees(EmployeeSearch model)
        {
            model.BranchId = CurrentBranchId;
            var result = _userRepository.Search(model, User.IsInRole(RoleList.SupperAdmin));
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AddEmployee()
        {
            CreateBranchViewBag();
            return View(new EmployeeModel { BranchId = CurrentBranchId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEmployee(EmployeeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!RoleList.GetAll().Contains(model.Role))
                    {
                        ShowMessageError(Message.RoleNotExists);
                        CreateBranchViewBag();
                        return View(model);
                    }

                    if (User.IsInRole(RoleList.Admin))
                    {
                        model.BranchId = CurrentBranchId;
                        model.Role = RoleList.Employee;
                    }

                    var user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Name = model.Name,
                        BranchId = model.BranchId,
                        IsActive = true
                    };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        result = await UserManager.AddToRoleAsync(user.Id, model.Role);
                        if (result.Succeeded)
                        {
                            ShowMessageSuccess(Message.Update_Successfully);
                            return RedirectToAction("Employees");
                        }
                        await UserManager.DeleteAsync(user);
                    }

                    AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                _log.Info(ex.ToString());

                ShowMessageError(Message.Update_Fail);
            }
            CreateBranchViewBag();
            return View(model);
        }

        public async Task<ActionResult> EditEmployee(string id)
        {
            var obj = await UserManager.FindByIdAsync(id);
            if (obj == null || User.IsInRole(RoleList.Admin) && UserManager.IsInRole(obj.Id, RoleList.Admin) || !IsValidBranch(obj.BranchId))
            {
                return HttpNotFound();
            }
            CreateBranchViewBag();
            var roleUser = UserManager.GetRoles(obj.Id).FirstOrDefault();
            return View(obj.ToModel(roleUser));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEmployee(EmployeeEditModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (User.IsInRole(RoleList.Admin) && UserManager.IsInRole(model.Id, RoleList.Admin))
                        return HttpNotFound();

                    if (!RoleList.GetAll().Contains(model.Role))
                    {
                        ShowMessageError(Message.RoleNotExists);
                        CreateBranchViewBag();
                        return View(model);
                    }

                    if (User.IsInRole(RoleList.Admin))
                    {
                        model.BranchId = CurrentBranchId;
                        model.Role = RoleList.Employee;
                    }

                    var user = await UserManager.FindByIdAsync(model.Id);

                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Name = model.Name;
                    user.BranchId = model.BranchId;
                    user.IsActive = model.IsActive;

                    var result = await UserManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                        CreateBranchViewBag();
                        return View(model);
                    }

                    var oldRole = UserManager.GetRoles(user.Id).FirstOrDefault();
                    if (oldRole != model.Role)
                    {
                        result = await UserManager.RemoveFromRolesAsync(user.Id, oldRole);
                        if (!result.Succeeded)
                        {
                            AddErrors(result);
                            CreateBranchViewBag();
                            return View(model);
                        }

                        result = UserManager.AddToRole(user.Id, model.Role);
                        if (!result.Succeeded)
                        {
                            AddErrors(result);
                            CreateBranchViewBag();
                            return View(model);
                        }
                    }

                    ShowMessageSuccess(Message.Update_Successfully);
                    return RedirectToAction("Employees");
                }
            }
            catch (Exception ex)
            {
                _log.Info(ex.ToString());

                ShowMessageError(Message.Update_Fail);
            }
            CreateBranchViewBag();
            return View(model);
        }

        public async Task<ActionResult> SetPasswordEmployee(string id)
        {
            var obj = await UserManager.FindByIdAsync(id);
            if (obj == null || User.IsInRole(RoleList.Admin) && UserManager.IsInRole(obj.Id, RoleList.Admin) || !IsValidBranch(obj.BranchId))
            {
                return HttpNotFound();
            }
            return View(new ChangePassword { Id = obj.Id, Name = obj.Name });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPasswordEmployee(ChangePassword model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByIdAsync(model.Id);
                    if (User.IsInRole(RoleList.Admin) && UserManager.IsInRole(model.Id, RoleList.Admin) || !IsValidBranch(user.BranchId))
                        return HttpNotFound();

                    var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    var result = await UserManager.ResetPasswordAsync(user.Id, token, model.Password);
                    if (result.Succeeded)
                    {
                        ShowMessageSuccess(Message.Update_Successfully);
                        return RedirectToAction("Employees");
                    }

                    AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                _log.Info(ex.ToString());

                ShowMessageError(Message.Update_Fail);
            }
            return View(model);
        }

        public async Task<ActionResult> DeleteEmployee(string id = "")
        {
            try
            {
                if (id == "")
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var obj = await UserManager.FindByIdAsync(id);
                if (obj == null || User.IsInRole(RoleList.Admin) && UserManager.IsInRole(obj.Id, RoleList.Admin) || !IsValidBranch(obj.BranchId))
                {
                    return HttpNotFound();
                }
                await UserManager.DeleteAsync(obj);

                ShowMessageSuccess(Message.Delete_Successfully);
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            return RedirectToAction("Employees");
        }

        [Override.Authorize(RoleList.SupperAdmin)]
        public ActionResult ChangeBranch()
        {
            var listBranch = _branchRepository.GetAll();
            if (!User.IsInRole(RoleList.SupperAdmin))
            {
                listBranch = listBranch.Where(m => m.Id == CurrentBranchId);
            }
            CreateBranchViewBag();
            return PartialView("_ChangeBranch");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Override.Authorize(RoleList.SupperAdmin)]
        public ActionResult ChangeBranch(int branchId)
        {
            try
            {
                var branch = _branchRepository.GetById(branchId);
                if (branch != null)
                {
                    SessionHelpers.Set(Constant.SESSION_CurrentBrach, branch);
                    return JavaScript("location.href = '';");
                }
                ModelState.AddModelError("", "Chi nhánh không tồn tại!");

            }
            catch (Exception ex)
            {
                _log.Info(ex.ToString());
            }
            CreateBranchViewBag();
            return PartialView("_ChangeBranch");
        }

        private void CreateBranchViewBag()
        {
            var listBranch = _branchRepository.GetAll();
            if (!User.IsInRole(RoleList.SupperAdmin))
            {
                listBranch = listBranch.Where(m => m.Id == CurrentBranchId);
            }
            ViewBag.BranchId = new SelectList(listBranch, "Id", "Name", CurrentBranchId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                if (error.Contains("Passwords must have at least one"))
                {
                    ModelState.AddModelError("", "Mật khẩu phải chứa ít nhất 1 kí tự hoa, 1 kí tự số và 1 kí tự đặc biệt.");
                    continue;
                }
                if (error.Contains("Name") && error.Contains("is already taken"))
                {
                    ModelState.AddModelError("", "Tài khoản đã có người sử dụng");
                    continue;
                }
                if (error.Contains("Email") && error.Contains("is invalid"))
                {
                    ModelState.AddModelError("", "Sai định dạng email");
                    continue;
                }

                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}