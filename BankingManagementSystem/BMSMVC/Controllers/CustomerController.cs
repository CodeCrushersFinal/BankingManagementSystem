using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMSMVC.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index(int id)
        {
            ViewBag.Message = id;
            return View(id);

        }
        public ActionResult TransferMoney(int id)
        {
            ViewBag.Message = id;
            return View(id);
        }

        public ActionResult TransactionHistory(int id)
        {
            ViewBag.Message = id;
            return View(id);
        }

        public ActionResult CheckBalance(int id)
        {
            ViewBag.Message = id;
            return View(id);
        }

        public ActionResult ApplyLoan(int id)
        {
            ViewBag.Message = id;
            return View();
        }

        public ActionResult CreateAccount(int id)
        {
            ViewBag.Message = id;
            return View();
        }

        public ActionResult DepositWithdraw(int id)
        {
            ViewBag.Message = id;
            return View(id);
        }

        public ActionResult Profile(int id)
        {
            ViewBag.Message = id;
            return View(id);
        }
    }
}