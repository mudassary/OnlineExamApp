using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineExamApp.Business.ViewModel;
using OnlineExamApp.Business;
using OnlineExamApp.Utils;

namespace OnlineExamApp.Controllers
{
    public class ExamController : Controller
    {
        Dictionary<int, List<int>> userResponse;
        ExamUtil examUtil = null;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            examUtil = new ExamUtil(this.Session);
        }

        [HttpPost]
        public ActionResult StartTest(CandidateViewModel candidate)
        {
            ExamManagerEF examManager = examUtil.GetExamManager();
            examManager.LoggedInUser = candidate;

            examUtil.SetExamManager(examManager);
            return this.RedirectToAction("Exam");
        }

        [HttpGet]
        public ActionResult GetExamDetails(int examID)
        {
            ExamManagerEF examManager = examUtil.GetExamManager();

            examManager.PopulateExamDetails(examID);
            examUtil.SetExamManager(examManager);

            return this.RedirectToAction("NextQuestion");
        }

        public ActionResult NextQuestion(int? questionId)
        {
            ExamManagerEF examManagerEF = examUtil.GetExamManager();
            QuestionProposedAnswersViewModel questionOptionsViewModel = examManagerEF.GetQuestion(questionId);

            ViewBag.UserName = examManagerEF.LoggedInUser.Name;

            return View(questionOptionsViewModel);     
        }        

        [HttpPost]
        public ActionResult SaveUserResponse(QuestionProposedAnswersViewModel questionOptionsViewModel)
        {   
            ExamManagerEF examManagerEF = Session["ExamManager"] as ExamManagerEF;
            examManagerEF.UpdateResponse(questionOptionsViewModel);

            if (!examManagerEF.IsLastQuestion(questionOptionsViewModel.Question.ID))
                return this.RedirectToAction("NextQuestion", new { questionId = questionOptionsViewModel.Question.ID });
            else
                return this.RedirectToAction("EvaluateExam");
        }

        public ActionResult EvaluateExam()
        {
            ExamManagerEF examManagerEF = examUtil.GetExamManager();
            ExamResultViewModel examResult = examManagerEF.EvaluateExam();

            return View(examResult);            
        }        

        public ActionResult Exam()
        {
            ExamManagerEF examManagerEF = examUtil.GetExamManager();

            this.ViewBag.UserName = examManagerEF.LoggedInUser.Name;
            return View(examManagerEF.GetExams());
        }        
	}
}