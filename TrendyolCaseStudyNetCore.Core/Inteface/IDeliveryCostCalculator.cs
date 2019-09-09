using System;
using System.Collections.Generic;
using System.Text;
using TrendyolCaseStudyNetCore.Core.Entity;

namespace TrendyolCaseStudyNetCore.Core.Interface
{
    public interface IDeliveryCostCalculator
    {
        double CalculateDeliveryCost(ShoppingCart shoppingCart);
    }
}
