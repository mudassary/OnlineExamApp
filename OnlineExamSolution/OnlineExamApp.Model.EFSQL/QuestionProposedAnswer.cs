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
    
    public partial class QuestionProposedAnswer
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public Nullable<int> ProposedAnswerID { get; set; }
    
        public virtual ProposedAnswer ProposedAnswer { get; set; }
        public virtual Question Question { get; set; }
    }
}