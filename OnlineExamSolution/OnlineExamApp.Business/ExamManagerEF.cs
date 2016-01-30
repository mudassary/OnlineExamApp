using OnlineExamApp.Business.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineExamApp.Model.EFSQL;
using OnlineExamApp.Business.ViewModel;
using OnlineExamApp.Business.ViewModels;

namespace OnlineExamApp.Business
{
    public class ExamManagerEF
    {
        Exam _exam = null;
        CandidateViewModel _candidate = null;
        UserResponseHeader _userResponseHeader = null;
        
        public QuestionProposedAnswersViewModel GetQuestion(int? currentQuestionId)
        {
           OnlineExamAppDBEntities3 dbContext = new OnlineExamAppDBEntities3();
           Question question = null;

           if (currentQuestionId != null)
           {
               int detailID = _exam.ExamDetails.Where(ed => ed.QuestionID == currentQuestionId).FirstOrDefault().ID;
               question = _exam.ExamDetails.Where(ed => ed.ID > detailID).FirstOrDefault().Question;
           }
           else
           {
               question = _exam.ExamDetails.FirstOrDefault().Question;
           }

            return GetQuestionViewModel(dbContext, question);
        }

        public ICollection<ExamViewModel> GetExams()
        {
            List<ExamViewModel> examList = new List<ViewModel.ExamViewModel>(); 

            OnlineExamAppDBEntities3 dbContext = new OnlineExamAppDBEntities3();
            List<Exam> exams = dbContext.Exams.ToList<Exam>();

            foreach(Exam exam in exams)
            {
                examList.Add(GetExamViewModel(exam));
            }

            return examList;
        }

        private QuestionProposedAnswersViewModel GetQuestionViewModel(OnlineExamAppDBEntities3 dbContext, Question question)
        {
            QuestionProposedAnswersViewModel questionOptionsViewModel = new QuestionProposedAnswersViewModel();        
            List<ProposedAnswerViewModel> options = new List<ProposedAnswerViewModel>();

            questionOptionsViewModel.Question = new QuestionViewModel() { ID = question.ID, Text = question.QuestionText };

            List<QuestionProposedAnswer> questionProposedAnswers = dbContext.QuestionProposedAnswers.Where(qpa => qpa.QuestionID == question.ID).ToList<QuestionProposedAnswer>();

            foreach (QuestionProposedAnswer questionProposedAnswer in questionProposedAnswers)
            {
                options.Add(new ProposedAnswerViewModel() { ID = questionProposedAnswer.ProposedAnswer.ID, Text = questionProposedAnswer.ProposedAnswer.AnswerText });
            }

            questionOptionsViewModel.ProposedAnswers = options;
            return questionOptionsViewModel;
        }

        private ExamViewModel GetExamViewModel(Exam exam)
        {
            ExamViewModel examViewModel = new ExamViewModel();
            examViewModel.ID = exam.ID;
            examViewModel.Title = exam.Title;
            examViewModel.Category = exam.Category;
            examViewModel.Description = exam.Description;
            examViewModel.QuestionCount = exam.ExamDetails.Count();
            
            return examViewModel;
        }

        public void PopulateExamDetails(int examID)
        {
            OnlineExamAppDBEntities3 dbContext = new OnlineExamAppDBEntities3();
            _exam = dbContext.Exams.Where(qpa => qpa.ID == examID).FirstOrDefault();            
        }

        public CandidateViewModel LoggedInUser
        {
            get { return _candidate; }
            set { _candidate = value; }
        }

        public void UpdateResponse(QuestionProposedAnswersViewModel questionOptionsViewModel)
        {
            //OnlineExamAppDBEntities3 dbContext = new OnlineExamAppDBEntities3();
            
            if (_userResponseHeader == null)
            {
                _userResponseHeader = new UserResponseHeader();
                _userResponseHeader.ExamID = _exam.ID;
                _userResponseHeader.AttemptedOn = DateTime.Today;

                //dbContext.UserResponseHeaders.Add(_userResponseHeader);                
            }

            UserResponseDetail userResponseDetail = null;

            foreach(ProposedAnswerViewModel optionViewModel in questionOptionsViewModel.ProposedAnswers)
            {
                if (!optionViewModel.IsSelected)
                    continue;

                userResponseDetail = new UserResponseDetail();
                userResponseDetail.UserResponseHeader = _userResponseHeader;
                userResponseDetail.QuestionID = questionOptionsViewModel.Question.ID;
                userResponseDetail.AnswerID = optionViewModel.ID;

                _userResponseHeader.UserResponseDetails.Add(userResponseDetail);
            }
            
            //dbContext.SaveChanges();                                
        }

        public bool IsLastQuestion(int questionId)
        {
            return _exam.ExamDetails.OrderByDescending(ed => ed.ID).FirstOrDefault().QuestionID == questionId;
        }

        private void CommitUserResponse()
        {
            if (_userResponseHeader != null)
            {
                OnlineExamAppDBEntities3 dbContext = new OnlineExamAppDBEntities3();
                dbContext.UserResponseHeaders.Add(_userResponseHeader);

                dbContext.SaveChanges();
            }
        }

        public ExamResultViewModel EvaluateExam()
        {
            CommitUserResponse();

            OnlineExamAppDBEntities3 dbContext = new OnlineExamAppDBEntities3();
            //UserResponseHeader userResponse = dbContext.UserResponseHeaders.Where(urh => urh.ExamID == _exam.ID).FirstOrDefault();

            ExamResultViewModel examResult = new ViewModel.ExamResultViewModel();
            List<QuestionResultViewModel> questionResults = new List<ViewModel.QuestionResultViewModel>();

            bool result = false;

            foreach(ExamDetail examDetail in _exam.ExamDetails)
            {
                List<QuestionAnswer> questionAnswers = dbContext.QuestionAnswers.Where(qa => qa.QuestionID == examDetail.QuestionID).ToList<QuestionAnswer>();
                List<UserResponseDetail> userResponseDetails = dbContext.UserResponseDetails.Where(urd => urd.UserResponseHeaderID == _userResponseHeader.ID && urd.QuestionID == examDetail.QuestionID).ToList<UserResponseDetail>();

                foreach(QuestionAnswer questionAnswer in questionAnswers)
                {
                    result = userResponseDetails.Exists(urd => urd.AnswerID == questionAnswer.AnswerID);

                    if (!result)
                        break;
                }

                questionResults.Add(new ViewModel.QuestionResultViewModel() { Question = examDetail.Question.QuestionText, Result = result});
            }

            examResult.UserName = this._candidate.Name;
            examResult.Results = questionResults;
            
            return examResult;
        }

        public RegisterViewModel RegisterUser(RegisterViewModel registerViewModel)
        {
            OnlineExamAppDBEntities3 dbContext = new OnlineExamAppDBEntities3();
            User existingUser = dbContext.Users.Where(u => u.UserName == registerViewModel.UserName).FirstOrDefault();

            if (existingUser == null)
            {
                User user = new User();
                user.FirstName = registerViewModel.FirstName;
                user.LastName = registerViewModel.LastName;
                user.UserName = registerViewModel.UserName;
                user.Password = registerViewModel.Password;

                dbContext.Users.Add(user);
                dbContext.SaveChanges();

                _candidate = new ViewModel.CandidateViewModel() { Name = registerViewModel.UserName };
                registerViewModel.ErrorDescription = string.Empty;
            }
            else
                registerViewModel.ErrorDescription = "The User Name already exists. Please supply a new User Name";

            return registerViewModel;
        }

        public LoginViewModel Login(LoginViewModel loginViewModel)
        {
            OnlineExamAppDBEntities3 dbContext = new OnlineExamAppDBEntities3();
            User existingUser = dbContext.Users.Where(u => u.UserName == loginViewModel.UserName && u.Password == loginViewModel.Password).FirstOrDefault();

            if (existingUser != null)
            {
                _candidate = new ViewModel.CandidateViewModel() { Name = existingUser.FirstName };
                loginViewModel.ErrorDescription = string.Empty;                
            }
            else
                loginViewModel.ErrorDescription = "Unable to login. Please check the User Name and Password and try again !!";

            return loginViewModel;
        }
    }
}
