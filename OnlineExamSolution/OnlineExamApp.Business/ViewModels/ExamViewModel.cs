using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamApp.Business.ViewModel
{
    public class ExamViewModel
    {
        private int _id;
        private string _title;
        private string _category;
        private string _description;
        private int _count;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int QuestionCount
        {
            get { return _count; }
            set { _count = value; }
        }
    }
}
