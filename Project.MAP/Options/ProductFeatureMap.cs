using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class ProductFeatureMap : BaseMap<ProductFeature>
    {
        public ProductFeatureMap()
        {
            Ignore(x => x.ID);

            HasKey(x => new { x.ProductID, x.FeatureID });

            ToTable("UrunlerinOzellikleri");

            Property(x => x.Value).HasColumnName("Degeri");
        }
    }
}
