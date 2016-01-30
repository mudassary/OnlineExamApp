using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamApp.Business.ViewModel
{
    [DataContract]
    public class QuestionViewModel
    {
        int _id = 0;
        string _text = string.Empty;

        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}
