using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class AppUserDetailMap : BaseMap<AppUserDetail>
    {
        public AppUserDetailMap()
        {
            ToTable("KullaniciDetayilari");

            Property(x => x.FirstName).HasColumnName("Adi");

            Property(x => x.LastName).HasColumnName("SoyAdi");

            Property(x => x.Address).HasColumnName("Adresi");
        }
    }
}
