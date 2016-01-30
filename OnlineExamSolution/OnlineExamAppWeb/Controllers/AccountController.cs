using OnlineExamApp.Business;
using OnlineExamApp.Business.ViewModels;
using OnlineExamApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineExamApp.Controllers
{
    public class AccountController : Controller
    {
        ExamUtil examUtil = null;

        public AccountController()
        {
            
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            examUtil = new ExamUtil(this.Session);
        }

        //
        // GET: /Account/
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            ExamManagerEF examManager = examUtil.GetExamManager();
            LoginViewModel loginViewModelUpdated = examManager.Login(loginViewModel);

            if (!string.IsNullOrEmpty(loginViewModelUpdated.ErrorDescription))
                return View(loginViewModelUpdated);
            else
                return this.RedirectToAction("Exam", "Exam");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            ExamManagerEF examManager = examUtil.GetExamManager();
            RegisterViewModel registerViewModelUpdated = examManager.RegisterUser(registerViewModel);

            if (!string.IsNullOrEmpty(registerViewModelUpdated.ErrorDescription))
                return View(registerViewModelUpdated);
            else
                return this.RedirectToAction("Exam", "Exam");
        }
	}
}