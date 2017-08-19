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
        public BaseController()
        {
            _entities = new iClassicEntities();
            _branchRepository = new BranchRepository(_entities);
            _customerRepository = new CustomerRepository(_entities);
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

        public string CurrentUserId
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }

        public void ShowMessageError(string message)
        {
            SessionHelpers.Set(Constant.SESSION_MessageError, message);
        }

        public void ShowMessageSuccess(string message)
        {
            SessionHelpers.Set(Constant.SESSION_MessageSuccess, message);
        }

        public void CreateBrachViewBag(int selectedId)
        {
            ViewBag.BranchId = new SelectList(_branchRepository.GetAll(), "Id", "Name", selectedId);
        }

        public void CreateCustomerViewBag(int selectedId)
        {
            var data = _customerRepository.GetAll().Select(m => new { m.Id, Title = m.TenKH + " (" + m.SDT + ")" });
            ViewBag.KhachHangId = new SelectList(data, "Id", "Title", selectedId);
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
    }
}