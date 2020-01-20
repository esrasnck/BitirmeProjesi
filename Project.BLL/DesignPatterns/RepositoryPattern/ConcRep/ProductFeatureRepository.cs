using Project.BLL.DesignPatterns.RepositoryPattern.BaseRep;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.RepositoryPattern.ConcRep
{
    public class ProductFeatureRepository : BaseRepository<ProductFeature>
    {
        public override void Update(ProductFeature item)
        {
            item.Status = DataStatus.Updated;
            item.ModifiedDate = DateTime.Now;
            ProductFeature toBeUpdated = FirstOrDefault(x=>x.ProductID==item.ProductID && x.FeatureID==item.FeatureID);
            db.Entry(toBeUpdated).CurrentValues.SetValues(item);
            Save();
        }
    }
}
