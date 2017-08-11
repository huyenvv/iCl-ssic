using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using iClassic.Models;
using iClassic.Services.Implementation;
using PagedList;
using log4net;
using iClassic.Helper;
using System.Web;

namespace iClassic.Controllers
{
    [Override.Authorize]
    public class CustomersController : BaseController
    {
        private readonly ILog _log;
        private CustomerRepository _customerRepository;
        private BranchRepository _branchRepository;

        public CustomersController()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _customerRepository = new CustomerRepository(_entities);
            _branchRepository = new BranchRepository(_entities);
        }

        // GET: Customeres
        public ActionResult Index(CustomerSearch model)
        {
            var result = _customerRepository.Search(model);
            int pageSize = model?.PageSize ?? _pageSize;
            int pageNumber = (model?.Page ?? 1);

            ViewBag.SearchModel = model;
            CreateBrachViewBag(model.BranchId);
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        // GET: Customeres/NewOrEdit/5
        public async Task<ActionResult> NewOrEdit(int id = 0)
        {
            var model = await _customerRepository.GetByIdAsync(id);
            if (model == null)
            {
                model = new Customer();
            }
            CreateBrachViewBag(CurrentUser.BranchId);
            return View(model);
        }

        // POST: Customeres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOrEdit([Bind(Include = "Id,TenKH,Address,SDT,SoDo,BranchId")] Customer model, HttpPostedFileBase fileImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (fileImage != null)
                    {
                        var fileName = FileHelper.CreateFile(fileImage, UploadFolder.KhachHang);
                        model.Image = fileName;
                    }

                    if (model.Id == 0)
                    {
                        model.CreateBy = CurrentUserId;
                        _customerRepository.Insert(model);
                    }
                    else
                    {
                        _customerRepository.Update(model);
                    }                    
                    await _customerRepository.SaveAsync();

                    ShowMessageSuccess(Message.Update_Successfully);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            CreateBrachViewBag(model.BranchId);
            return View(model);
        }

        // GET: Customeres/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var obj = await _customerRepository.GetByIdAsync(id);
                if (obj == null)
                {
                    return HttpNotFound();
                }
                _customerRepository.Delete(obj);

                await _customerRepository.SaveAsync();

                ShowMessageSuccess(Message.Update_Successfully);
            }
            catch (Exception ex)
            {
                ShowMessageError(Message.Update_Fail);

                _log.Info(ex.ToString());
            }
            return RedirectToAction("Index");
        }

        private void CreateBrachViewBag(int selectedBranchId)
        {
            ViewBag.BranchId = new SelectList(_branchRepository.GetAll(), "Id", "Name", selectedBranchId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _branchRepository.Dispose();
                _customerRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
