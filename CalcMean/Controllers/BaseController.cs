using System.Linq;
using System.Web.Mvc;
using CalcMean.Models;
using Microsoft.AspNet.Identity;

namespace CalcMean.Controllers
{
    public class BaseController : Controller
    {
        private readonly CmContext _db = new CmContext();

        public CmUser CurrentUser()
        {
            return _db.Users.Find(User.Identity.GetUserId());
        }

        public void CreateViewBagDotChotSo(int id = 0)
        {
            ViewBag.ChotSoId = ViewBag.dotChotId = _db.DotChotSo.OrderByDescending(m => m.Id).ToList().Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = Common.Decode(m.Title),
                Selected = id == m.Id
            });
        }
    }
}