using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class ProductMap : BaseMap<Product>
    {
        public ProductMap()
        {
            ToTable("Urunler");

            Property(x => x.ProductName).HasColumnName("UrunAdi");

            Property(x => x.ImagePath).HasColumnName("UrunResmi");

            Property(x => x.UnitsInStock).HasColumnName("StokMiktari");

            Property(x => x.UnitPrice).HasColumnName("BirimFiyati").HasColumnType("money");
        }
    }
}
