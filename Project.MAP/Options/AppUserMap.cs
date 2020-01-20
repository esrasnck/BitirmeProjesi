using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class AppUserMap : BaseMap<AppUser>
    {
        public AppUserMap()
        {
            HasOptional(x => x.Profile).WithRequired(x => x.AppUser);

            ToTable("Kullanicilar");

            Property(x => x.UserName).HasColumnName("KullaniciAdi");

            Property(x => x.Password).HasColumnName("Sifre");

            Property(x => x.Email).HasColumnName("e-Posta");

            Property(x => x.IsActive).HasColumnName("AktifKullanici");

            Property(x => x.ActivationCode).HasColumnName("AktivasyonKodu");

            Property(x => x.Role).HasColumnName("KullaniciRolu");

        }
    }
}
