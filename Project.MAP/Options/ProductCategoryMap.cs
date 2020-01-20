using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class ProductCategoryMap : BaseMap<ProductCategory>
    {
        public ProductCategoryMap()
        {
            
            Ignore(x => x.ID);

            HasKey(x => new { x.ProductID, x.CategoryID });

            ToTable("UrunlerinKategorileri");

        }
    }
}
