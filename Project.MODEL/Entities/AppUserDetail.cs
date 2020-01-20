using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
   public class AppUserDetail:BaseEntity
    {
        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("İsim"), MaxLength(30, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("Soyisim"), MaxLength(30, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), DataType(DataType.Text)]
        public string LastName { get; set; }

        [DisplayName("Adres"), MaxLength(200, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), DataType(DataType.MultilineText)]
        public string Address { get; set; }

        // Relational Properties

        public virtual AppUser AppUser { get; set; }
    }
}
