using Project.MVCUI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.VMClasses
{
    public class LogVM
    {
        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string Information { get; set; }

        public Keyword Description { get; set; }

        public string UserName { get; set; }
       

    }
}