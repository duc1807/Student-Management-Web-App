using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AssignmentApp.Models
{
    public class ListTrainee
    {
        public List<SelectListItem> Trainees { get; set; }
        public int[] ID { get; set; }
    }
}