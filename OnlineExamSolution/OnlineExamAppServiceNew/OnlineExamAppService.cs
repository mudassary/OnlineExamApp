using OnlineExamApp.Business;
using OnlineExamApp.Business.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OnlineExamAppServiceNew
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class OnlineExamAppService : IOnlineExamAppService
    {
        public QuestionProposedAnswersViewModel GetQuestion(int? questionId)
        {
            //return new ExamManager().GetQuestion(questionId);

            //QuestionOptionsViewModel a = new QuestionOptionsViewModel();
            //a.Question = new QuestionViewModel() { ID = 1, Text = "Test" };
            //a.IsMultiChoice = true;

            //return a;

            return null;
        }

        public CandidateViewModel Test()
        {
            return new CandidateViewModel() { Name = "Mudassar", ID = 1 };
        }
    }
}
