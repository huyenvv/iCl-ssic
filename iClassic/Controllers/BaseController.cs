using iClassic.Helper;
using iClassic.Models;
using iClassic.Services.Implementation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iClassic.Controllers
{
    public class BaseController : Controller
    {
        public iClassicEntities _entities;
        public int _pageSize = 10;
        private AspNetUsers _currentUser;
        private BranchRepository _branchRepository;
        private CustomerRepository _customerRepository;
        private ProductTypeRepository _productTypeRepository;
        public BaseController()
        {
            _entities = new iClassicEntities();
            _branchRepository = new BranchRepository(_entities);
            _customerRepository = new CustomerRepository(_entities);
            _productTypeRepository = new ProductTypeRepository(_entities);
        }

        public AspNetUsers CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = _entities.AspNetUsers.FirstOrDefault(x => x.Id == CurrentUserId);
                }
                return _currentUser;
            }
        }

        public Branch CurrentBrach
        {
            get
            {
                return CurrentUser.Branch;
            }
        }

        public int CurrentBranchId
        {
            get
            {
                return CurrentUser.BranchId;
            }
        }

        public string CurrentUserId
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }

        public int GetPermissionBranchId(int? branchId)
        {
            if (!User.IsInRole(RoleList.SupperAdmin) || !branchId.HasValue || branchId == 0)
            {
                branchId = CurrentBranchId;
            }

            return branchId.Value;
        }

        public void ShowMessageError(string message)
        {
            SessionHelpers.Set(Constant.SESSION_MessageError, message);
        }

        public void ShowMessageSuccess(string message)
        {
            SessionHelpers.Set(Constant.SESSION_MessageSuccess, message);
        }


        public void CreateCustomerViewBag(int selectedId)
        {
            var data = _customerRepository.GetByBranchId(CurrentBranchId).Select(m => new { m.Id, Title = m.TenKH + " (" + m.SDT + ")" });
            ViewBag.CustomerId = new SelectList(data, "Id", "Title", selectedId);
        }

        public void CreateListProductTypeViewBag()
        {
            ViewBag.ProductTypeList = _productTypeRepository.GetAll();
        }

        public int SoNgayTraSauKhiSua
        {
            get
            {
                int value;
                int.TryParse(ConfigurationSettings.AppSettings["SoNgayTraSauKhiSua"], out value);
                return value;
            }
        }
        public int SoNgayThuSauKhiLam
        {
            get
            {
                int value;
                int.TryParse(ConfigurationSettings.AppSettings["SoNgayThuSauKhiLam"], out value);
                return value;
            }
        }

        public ActionResult AccessDenied()
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        public bool IsValidBranch(int branchId)
        {
            if (User.IsInRole(RoleList.SupperAdmin) || CurrentBranchId == branchId)
            {
                return true;
            }
            return false;
        }
    }
}