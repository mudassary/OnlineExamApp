//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineExamApp.Model.EFSQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuestionAnswer
    {
        public int ID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public Nullable<int> AnswerID { get; set; }
    
        public virtual ProposedAnswer ProposedAnswer { get; set; }
        public virtual Question Question { get; set; }
    }
}
