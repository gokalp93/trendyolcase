using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrendyolCaseStudyNetCore.Core.Interface;

namespace TrendyolCaseStudyNetCore.Core.Entity
{
    public class ShoppingCart : IShoppingCart
    {
        private Dictionary<Product, int> ProductDict;
        public int NumberOfProducts
        {
            get
            {
                return ProductDict.Count;
            }
        }
        public int NumberOfDeliveries
        {
            get
            {
                return ProductDict.Keys.Select(p => p.Category).Distinct().Count();
            }
        }

        private double TotalCampaingDiscount { get; set; } = 0;
        private double TotalCouponDiscount { get; set; } = 0;
        private double TotalPrice
        {
            get
            {
                return ProductDict.Sum(p => p.Value * p.Key.Price);
            }
        }
        private double TotalPriceAfterDiscounts
        {
            get
            {
                return TotalPrice - TotalCampaingDiscount - TotalCouponDiscount;
            }
        }


        private readonly IDeliveryCostCalculator _deliveryCostCalculator;
        public List<Discount> discountList;

        public ShoppingCart(IDeliveryCostCalculator deliveryCostCalculator)
        {
            ProductDict = new Dictionary<Product, int>();
            _deliveryCostCalculator = deliveryCostCalculator;
            discountList = new List<Discount>();
        }

        #region Cart Operations
        /// <summary>
        /// This function takes product and quatity as parameters, 
        /// if product is not null and quantity is more than 0 tries to add quatity whether the product exist
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool AddItem(Product product, int quantity)
        {
            bool result = false;

            if (product != null && quantity > 0)
            {
                if (ProductDict.ContainsKey(product))
                {
                    int productQuantity;
                    ProductDict.TryGetValue(product, out productQuantity);
                    ProductDict[product] = productQuantity + quantity;
                }
                else
                {
                    ProductDict.Add(product, quantity);
                }
                result = true;
            }

            return result;
        }

        /// <summary>
        /// This function takes product and quatity as parameters, 
        /// If product is exist in dictionary lower its quantity
        /// Also new quantity lower than 0, removes it from dictionary
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool RemoveItem(Product product, int quantity)
        {
            bool result = false;

            var isProductExist = ProductDict.ContainsKey(product);
            if (isProductExist && quantity > 0)
            {
                int productQuantity;
                ProductDict.TryGetValue(product, out productQuantity);

                var newQuatity = productQuantity - quantity;
                if (newQuatity < 0)
                {
                    ProductDict.Remove(product);
                }
                else
                {
                    ProductDict[product] = productQuantity - quantity;
                }
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Returns item count in shopping cart
        /// </summary>
        /// <returns></returns>
        public int GetItemCount()
        {
            return ProductDict.Count;
        }
        #endregion

        #region Discount Operations 
        public void ApplyDiscounts(params Discount[] discounts)
        {
            discountList.AddRange(discounts);

            foreach (var discount in discountList)
            {
                TotalCampaingDiscount += discount.ApplyDiscount(this.ProductDict);
                if (TotalCampaingDiscount > TotalPrice)
                {
                    throw new Exception("Total campaing discount cannot be lower than total price");
                }
            }
        }

        public void ApplyCoupon(Coupon coupon)
        {
            if (coupon == null)
            {
                throw new Exception("Coupon cannot be empty");
            }

            TotalCouponDiscount = coupon.ApplyDiscount(this.TotalPriceAfterDiscounts);
        }

        public double GetTotalAmountAfterDiscounts()
        {
            return TotalPriceAfterDiscounts;
        }

        public double GetCouponDiscount()
        {
            return TotalCouponDiscount;
        }

        public double GetCampaingDiscount()
        {
            return TotalCampaingDiscount;
        }
        #endregion

        #region Delivery Operations
        /// <summary>
        /// Returns total delivery cost
        /// </summary>
        /// <returns></returns>
        public double GetDeliveryCost()
        {
            return _deliveryCostCalculator.CalculateDeliveryCost(this);
        }
        #endregion

        #region Print Operations
        /// <summary>
        /// Print product related properties and total amounts (total price, total price after discounts, delivery cost etc.)
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            var categoryList = ProductDict.GroupBy(p => p.Key.Category)
                                            .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var category in categoryList)
            {
                sb.AppendLine($"Category:{category.Key.Title}");
                foreach (var product in category.Value)
                {
                    sb.AppendLine($"\t Title:{product.Key.Title}: Price:{product.Key.Price} Quantity:{product.Value}");
                }
            }
            sb.AppendLine($"Total Price Before Discounts:{TotalPrice}");
            sb.AppendLine($"Total Price After Discounts:{TotalPriceAfterDiscounts}");
            sb.AppendLine($"Total Discount:{TotalCampaingDiscount + TotalCouponDiscount}");
            sb.AppendLine($"Delivery Cost:{GetDeliveryCost()}");


            return sb.ToString();
        }
        #endregion

    }
}
