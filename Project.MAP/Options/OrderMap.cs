using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class OrderMap : BaseMap<Order>
    {
        public OrderMap()
        {
            ToTable("Siparisler");

            Property(x => x.Address).HasColumnName("Adres");

            Property(x => x.City).HasColumnName("Sehir");

            Property(x => x.Town).HasColumnName("Ilce");

            Property(x => x.District).HasColumnName("Mahalle");

            Property(x => x.Phone).HasColumnName("Telefon");
        }
    }
}
