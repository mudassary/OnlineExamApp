using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace OnlineExamApp.Business.ViewModel
{
    [DataContract]
    public class ProposedAnswerViewModel
    {
        int _id = 0;
        string _text = string.Empty;
        bool _isSelected = false;
        string _selected = string.Empty;

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

        [DataMember]
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        [DataMember]
        public string Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
    }
}