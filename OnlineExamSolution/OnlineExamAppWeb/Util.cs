using OnlineExamApp.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamApp.Utils
{
    public class ExamUtil
    {
        HttpSessionStateBase _session;

        public ExamUtil(HttpSessionStateBase session)
        {
            _session = session;
        }

        public ExamManagerEF GetExamManager()
        {
            ExamManagerEF examManager = _session["ExamManager"] as ExamManagerEF;

            if (examManager == null)
            {
                examManager = new ExamManagerEF();
                _session["ExamManager"] = examManager;
            }

            return examManager;
        }

        public void SetExamManager(ExamManagerEF examManager)
        {
            _session["ExamManager"] = examManager;
        }
    }
}