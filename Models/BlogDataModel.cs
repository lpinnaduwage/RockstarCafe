using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RockstarProj.Models
{
    public class BlogDataModel
    {
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] FileData { get; set; }
        [DisplayName("Select File to Upload")]
        public HttpPostedFileBase FileContent { get; set; }
    }
}