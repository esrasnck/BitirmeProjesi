using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
   public class ProductFeature:BaseEntity
    {
        public int ProductID { get; set; }

        public int FeatureID { get; set; }

        [DisplayName("Özellik Değeri"), DataType(DataType.Text)]
        public string Value { get; set; }

        // Relational Properties

        public virtual Product  Product { get; set; }

        public virtual Feature Feature { get; set; }
    }
}
