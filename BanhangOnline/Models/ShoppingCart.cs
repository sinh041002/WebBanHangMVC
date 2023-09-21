using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanhangOnline.Models
{

    public class ShoppingCart
    {
        public List<ShoppingCartItems> Items { get; set; }
        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItems>();
        }
        public void AddToCart(ShoppingCartItems item, int quantity)
        {
            var checkExit = Items.FirstOrDefault(x=>x.ProductId==item.ProductId);
            if (checkExit != null)
            {
                checkExit.Quantity += quantity;
                checkExit.TotalPrice=checkExit.Quantity*checkExit.Price;
            }else
            {
                Items.Add(item);
            }
        }
        public bool Remove(int id)
        {
            var checkExit=Items.SingleOrDefault(x=>x.ProductId == id);
            if (checkExit != null)
            {
                Items.Remove(checkExit);
                return true;
            }
            return false;
        }
        public void UpdateQuantity(int id,int quantity)
        {
            var checkExit = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkExit != null)
            {
                checkExit.Quantity = quantity;
                checkExit.TotalPrice=checkExit.Quantity*checkExit.Price;
            }
        }
        public decimal GetTotal()
        {
            return Items.Sum(x => x.TotalPrice);
        }
        public decimal GetTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }
        public void ClearCard()
        {
            Items.Clear();
        }
    }
        public class ShoppingCartItems
        {
            
            public int ProductId { get; set; }
            public string ProductName { get; set; }
      
        public string CategoryName { get; set; }
            public string ProductImg { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        public string Alias { get; set; }
        public decimal TotalPrice { get; set; }

        
    }
    }

 