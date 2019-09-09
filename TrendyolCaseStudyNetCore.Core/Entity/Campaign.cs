using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static TrendyolCaseStudyNetCore.Core.Enum.Enum;

namespace TrendyolCaseStudyNetCore.Core.Entity
{
    public class Campaign : Discount
    {
        public Campaign(Category category,
                        double discountAmount,
                        int minimumItemCount,
                        DiscountType discountType
                        ) : base(category,
                                 discountAmount,
                                 minimumItemCount,
                                 discountType)
        {

        }
        /// <summary>
        /// return discount amount for appropite DiscountType
        /// </summary>
        /// <param name="productDict"></param>
        /// <returns></returns>
        public override double ApplyDiscount(Dictionary<Product, int> productDict)
        {
            double totalDiscount = 0;

            if (productDict.Count == 0)
            {
                throw new Exception("Product list cannot be empty");
            }

            var toBeDiscountedItems = productDict.Where(p => p.Key.Category == this.Category);

            if (toBeDiscountedItems.Sum(i => i.Value) >= MinimumItemCount)
            {
                switch (this.DiscountType)
                {
                    case DiscountType.Rate:
                        totalDiscount = toBeDiscountedItems.Sum(i => i.Key.Price * i.Value) * (DiscountAmount / 100);
                        break;
                    case DiscountType.Amount:
                        totalDiscount = this.DiscountAmount;
                        break;
                    default:
                        throw new Exception("You should give a valid discount type.");
                }
            }

            return totalDiscount;
        }

    }
}
