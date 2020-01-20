using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.VMClasses
{
    public class PaymentVM
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public decimal PaymentPrice { get; set; }

        public string CardNumber { get; set; }

        public string SecurityNumber { get; set; }
    }
}