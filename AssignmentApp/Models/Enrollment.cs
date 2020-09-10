//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssignmentApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class Enrollment
    {
        private SelectListItem selectedItem;

        public Enrollment(int enrollmentID, int courseID, SelectListItem selectedItem)
        {
            EnrollmentID = enrollmentID;
            CourseID = courseID;
            this.selectedItem = selectedItem;
        }

        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public string TraineeID { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Trainee Trainee { get; set; }
    }
}