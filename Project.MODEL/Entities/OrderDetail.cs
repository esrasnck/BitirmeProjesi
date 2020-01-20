using Project.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class OrderDetail : BaseEntity
    {
    
        public short Amount { get; set; }

        public decimal? TotalPrice { get; set; }

        public DateTime? PaymentDate { get; set; }

        public OrderStatus OrderStatus{ get; set; }

        public PackingStatus PackingStatus { get; set; }

        public PaymentOption PaymentOption { get; set; }
        
        public int ProductID { get; set; }

        public int OrderID { get; set; }

        // Relational Properties

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
