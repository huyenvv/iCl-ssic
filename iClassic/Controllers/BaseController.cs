using iClassic.Helper;
using iClassic.Models;
using iClassic.Services.Implementation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
        public BaseController()
        {
            _entities = new iClassicEntities();
            _branchRepository = new BranchRepository(_entities);
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

        public void CreateBrachViewBag(int selectedBranchId)
        {
            ViewBag.BranchId = new SelectList(_branchRepository.GetAll(), "Id", "Name", selectedBranchId);
        }
    }
}