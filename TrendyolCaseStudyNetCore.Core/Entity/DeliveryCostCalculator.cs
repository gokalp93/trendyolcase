using System;
using System.Collections.Generic;
using System.Text;
using TrendyolCaseStudyNetCore.Core.Interface;

namespace TrendyolCaseStudyNetCore.Core.Entity
{
    public class DeliveryCostCalculator : IDeliveryCostCalculator
    {
        private const double fixedPrice = 2.99;

        public DeliveryCostCalculator(double costPerDelivery, double costPerProduct)
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
        }

        private double CostPerDelivery { get; set; }
        private double CostPerProduct { get; set; }


        public double CalculateDeliveryCost(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                throw new Exception("Shopping cart can not be null");
            }

            return ((CostPerDelivery * shoppingCart.NumberOfDeliveries) + (CostPerProduct * shoppingCart.NumberOfProducts) + fixedPrice);
        }
    }
}
