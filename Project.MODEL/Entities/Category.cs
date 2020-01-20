using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Category : BaseEntity
    {

        public Category()
        {
            Products = new List<ProductCategory>();
        }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("Kategori Adı"), MaxLength(30, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), DataType(DataType.Text)]
        public string CategoryName { get; set; }

        [DisplayName("Kategori Açıklaması"), MaxLength(1000, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        // Relational Properties

        public virtual List<ProductCategory> Products { get; set; }
    }
}
