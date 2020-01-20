using Project.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            SiparisDurumu = OrderStatus.Pending;
        }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("TC Kimlik Numarası"), MaxLength(11, ErrorMessage = "TC Kimlik Numaranızı Hatalı Girdiniz."), MinLength(11, ErrorMessage = "TC Kimlik Numaranızı Hatalı Girdiniz."), DataType(DataType.Text)]
        public string TC { get; set; }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("Adres"), MaxLength(200, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("İl"), DataType(DataType.Text)]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("İlçe"), DataType(DataType.Text)]
        public string Town { get; set; }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("Mahalle"), DataType(DataType.Text)]
        public string District { get; set; }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("Telefon"), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public OrderStatus SiparisDurumu { get; set; }

        public int AppUserID { get; set; }

        // Relational Properties

        public virtual AppUser AppUser { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
