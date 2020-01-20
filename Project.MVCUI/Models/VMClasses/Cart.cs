using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.VMClasses
{
    public class Cart
    {
        Dictionary<int, CartItem> _sepetim;

        public Cart()
        {
            _sepetim = new Dictionary<int, CartItem>();
        }

        public List<CartItem> Sepetim
        {
            get
            {
                return _sepetim.Values.ToList();
            }
        }


        public void SepeteEkle(CartItem item)
        {
            if (_sepetim.ContainsKey(item.ID))
            {
                _sepetim[item.ID].Amount += 1;
                return;
            }

            _sepetim.Add(item.ID, item);
        }


        public void Sepettensil(int id)
        {
            if (_sepetim[id].Amount > 1)
            {
                _sepetim[id].Amount -= 1;
                return;
            }

            _sepetim.Remove(id);
        }

   
        public decimal? TotalPrice
        {
            get
            {
                return _sepetim.Sum(x => x.Value.SubTotal);
            }
        }
    }
}