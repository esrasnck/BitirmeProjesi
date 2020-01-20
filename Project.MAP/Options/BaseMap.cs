using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public abstract class BaseMap<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseMap()
        {
            Property(x => x.CreatedDate).HasColumnName("OlusturulmaTarihi").HasColumnType("datetime2");

            Property(x => x.ModifiedDate).HasColumnName("GuncellenmeTarihi").HasColumnType("datetime2");

            Property(x => x.DeletedDate).HasColumnName("SilinmeTarihi").HasColumnType("datetime2");

            Property(x => x.Status).HasColumnName("VeriDurumu");

        }
    }
}
