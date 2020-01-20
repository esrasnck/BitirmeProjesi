using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class FeatureMap : BaseMap<Feature>
    {
        public FeatureMap()
        {
            ToTable("Ozellikler");

            Property(x => x.FeatureName).HasColumnName("OzellikAdi");

            Property(x => x.Description).HasColumnName("Aciklama");
        }
    }
}
