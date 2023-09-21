using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanhangOnline.Models.EF
{
    public class CommonAbstract
    {


        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedrBy { get; set; }
        public DateTime ModifiedrDate { get; set; }
    }
}