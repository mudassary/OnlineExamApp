using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamApp.Business.ViewModel
{
    public class ExamResultViewModel
    {
        string _userName = string.Empty;
        List<QuestionResultViewModel> _questionResults;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public List<QuestionResultViewModel> Results
        {
            get { return _questionResults; }
            set { _questionResults = value; }
        }
    }
}
