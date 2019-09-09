namespace TrendyolCaseStudyNetCore.Core.Entity
{
    public interface IShoppingCart
    {
        bool AddItem(Product product, int quantity);
        bool RemoveItem(Product product, int quantity);
        int GetItemCount();
        double GetDeliveryCost();
        void ApplyDiscounts(params Discount[] discounts);
        void ApplyCoupon(Coupon coupon);
        double GetTotalAmountAfterDiscounts();
        double GetCouponDiscount();
        double GetCampaingDiscount();
        string Print();
    }
}