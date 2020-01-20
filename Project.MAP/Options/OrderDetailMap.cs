using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class OrderDetailMap : BaseMap<OrderDetail>
    {
        public OrderDetailMap()
        {
            Ignore(x => x.ID);

            HasKey(x => new { x.OrderID, x.ProductID });

            ToTable("SiparisDetaylari");

            Property(x => x.Amount).HasColumnName("Miktar");

            Property(x => x.TotalPrice).HasColumnName("ToplamUcret").HasColumnType("money");

            Property(x => x.PaymentDate).HasColumnName("OdemeTarihi");

            Property(x => x.OrderStatus).HasColumnName("SiparisDurumu");

            Property(x => x.PackingStatus).HasColumnName("PaketlemeDurumu");

            Property(x => x.PaymentOption).HasColumnName("OdemeSecenekleri");

        }
    }
}
