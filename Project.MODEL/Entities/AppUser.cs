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
    public class AppUser : BaseEntity
    {
        public AppUser()
        {
            Role = UserRole.Member;
            ActivationCode = Guid.NewGuid();
        }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("Kullanıcı Adı"), MaxLength(30, ErrorMessage ="{0} alanı en fazla {1} karakter olmalıdır."), DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("Şifre"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("E-Posta"), MaxLength(60, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public Guid? ActivationCode { get; set; }

        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), DisplayName("Rol")]
        public UserRole Role { get; set; }

        // Relational properties

        public virtual AppUserDetail Profile { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
