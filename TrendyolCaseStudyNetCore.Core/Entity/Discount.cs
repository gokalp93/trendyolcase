using System;
using System.Collections.Generic;
using System.Text;
using static TrendyolCaseStudyNetCore.Core.Enum.Enum;

namespace TrendyolCaseStudyNetCore.Core.Entity
{
    public abstract class Discount
    {
        public Category Category { get; set; }
        public double DiscountAmount { get; set; }
        public int MinimumItemCount { get; set; }
        public DiscountType DiscountType { get; set; }

        public Discount(Category category,
                        double discountAmount,
                        int minimumItemCount,
                        DiscountType discountType)
        {
            Category = category;
            DiscountAmount = discountAmount;
            MinimumItemCount = minimumItemCount;
            DiscountType = discountType;
        }

        public abstract double ApplyDiscount(Dictionary<Product, int> productDict);
    }
}
