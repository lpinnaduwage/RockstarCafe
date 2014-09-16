using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RockstarProj.Models
{
    public class InvolvedModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
        public string Instagram { get; set; }
        public string Skype { get; set; }

        public string CompanyProjName { get; set; }
        public string CompanyProjDescription { get; set; }
        public string QuantifiableMetrics { get; set; }

        public string ImpactQuestion { get; set; }
        public string ExampleQuestion { get; set; }
        public string LearnQuestion { get; set; }
        public string AdditionalInfo { get; set; }

        

        [DisplayName("Select File to Upload")]
        public HttpPostedFileBase FirstFile { get; set; }


        [DisplayName("Select File to Upload")]
        public HttpPostedFileBase SecondFile { get; set; }

        [DisplayName("Select File to Upload")]
        public HttpPostedFileBase ThirdFile { get; set; }

    }
}