using System;
using System;
using System.Collections.Generic;
using System.Text;
using static TrendyolCaseStudyNetCore.Core.Enum.Enum;

namespace TrendyolCaseStudyNetCore.Core.Entity
{
    public class Coupon
    {
        public double MinimumPurchaseAmount { get; set; }
        public double DiscountAmount { get; set; }
        public DiscountType DiscountType { get; set; }

        public Coupon(double minimumPurchaseAmount,
                      double discountAmount,
                      DiscountType discountType)
        {
            MinimumPurchaseAmount = minimumPurchaseAmount;
            DiscountAmount = discountAmount;
            DiscountType = DiscountType;
        }

        /// <summary>
        /// return discount amount for appropite DiscountType
        /// </summary>
        /// <param name="productDict"></param>
        /// <returns></returns>
        public double ApplyDiscount(double totalAmount)
        {
            if (totalAmount <= 0)
            {
                throw new Exception("Total Amount cannot be lower than 0");
            }
            switch (DiscountType)
            {
                case DiscountType.Rate:
                    return totalAmount * (DiscountAmount / 100);
                case DiscountType.Amount:
                    return totalAmount - DiscountAmount;
                default:
                    throw new Exception("You should give a valid discount type.");
            }
        }
    }
}
