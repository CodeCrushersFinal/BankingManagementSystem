using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMSMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult TransactionDetails()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}