using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            Categories = new List<ProductCategory>();
        }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("Ürün Adı"), MaxLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), DataType(DataType.Text)]
        public string ProductName { get; set; }

        [DisplayName("Ürün Resmi")]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("Stok Miktarı"), Range(0, ushort.MaxValue, ErrorMessage ="Geçerli bir değer giriniz.")]
        public int UnitsInStock { get; set; }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("Ürün Fiyatı"), Range(0, int.MaxValue, ErrorMessage = "Geçerli bir değer giriniz."), DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        // Relational Properties

        public virtual List<ProductCategory> Categories { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }

        public virtual List<ProductFeature> ProductFeatures { get; set; }
    }
}
