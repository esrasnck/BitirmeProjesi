using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Feature : BaseEntity
    {
        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("Özellik Adı"), MaxLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), DataType(DataType.Text)]
        public string FeatureName { get; set; }

        [DisplayName("Özellik Açıklaması"), MaxLength(200, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        // Relational properties

        public virtual List<ProductFeature> ProductFeatures { get; set; }
    }
}
