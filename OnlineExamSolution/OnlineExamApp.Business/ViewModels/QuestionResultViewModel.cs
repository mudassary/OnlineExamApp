using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamApp.Business.ViewModel
{
    [DataContract]
    public class QuestionResultViewModel
    {
        string _question;
        bool _result;

        [DataMember]
        public string Question
        {
            get { return _question; }
            set { _question = value; }
        }

        [DataMember]
        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
