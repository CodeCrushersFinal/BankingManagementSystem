using BMSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using BMSMVC.DTO;
using System.Threading.Tasks;

namespace BMSMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<ActionResult> Index()
        {
            SessionModel sessionModel = new SessionModel();
            Session.Add("UserName", User.Identity.Name);
            string email = User.Identity.Name;
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"https://localhost:7000/api/Customer/GetEmail/{email}");
            string response = await httpResponseMessage.Content.ReadAsStringAsync();
            var userResponse = JsonConvert.DeserializeObject<CustomerResponseDTO>(response);

            if(userResponse.RoleId == 1003)
            {
                sessionModel.Role = "Admin";
                sessionModel.Addess = "dsfdfsdfds";
                sessionModel.Name = User.Identity.Name;

                Session.Add("SessionModel", sessionModel);


                Session.Add("Role", "Admin");
                ViewBag.Role = "Admin";
                return RedirectToAction("MainBody", "Admin");
            }
            else if(userResponse.RoleId == 1002 || userResponse.RoleId == 1001)
            {
                Session.Add("Role", "Customer");
                ViewBag.Role = "Customer";
                return RedirectToAction("Index","Customer");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}