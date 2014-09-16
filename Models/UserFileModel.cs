using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RockstarProj.Models
{
    public class UserFileModel
    {
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
    }
}
