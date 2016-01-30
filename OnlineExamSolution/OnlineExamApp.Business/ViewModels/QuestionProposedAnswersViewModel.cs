using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineExamApp.Model;
using System.Runtime.Serialization;

namespace OnlineExamApp.Business.ViewModel
{
    [DataContract]
    public class QuestionProposedAnswersViewModel //: IQuestionOptionsViewModel
    {
        QuestionViewModel _question;
        List<ProposedAnswerViewModel> _proposedAnswers;
        bool _isMultiChoice = false;

        [DataMember]
        public QuestionViewModel Question
        {
            get { return _question; }
            set { _question = value; }
        }

        [DataMember]
        public List<ProposedAnswerViewModel> ProposedAnswers
        {
            get { return _proposedAnswers; }
            set { _proposedAnswers = value; }
        }

        [DataMember]
        public bool IsMultiChoice
        {
            get { return _isMultiChoice; }
            set { _isMultiChoice = value; }
        }
    }
}