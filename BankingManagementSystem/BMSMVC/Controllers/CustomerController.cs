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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TransferMoney()
        {
            return View();
        }

        public ActionResult TransactionHistory()
        {
            return View();
        }

        public ActionResult CheckBalance()
        {
            return View();
        }

        public ActionResult ApplyLoan()
        {
            return View();
        }

        public ActionResult CreateAccount()
        {
            return View();
        }

        public ActionResult DepositWithdraw()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }
    }
}