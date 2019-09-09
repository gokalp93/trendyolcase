using System;
using TrendyolCaseStudyNetCore.Core.Entity;
using static TrendyolCaseStudyNetCore.Core.Enum.Enum;

namespace TrendyolCaseStudyNetCore.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Category trendyolMan = new Category("Trendyol Man");
            Category trendyolMilla = new Category("Trendyol Milla");

            Product bomberJacket = new Product("Lacivert Erkek Yanı Şeritli Çizgili Triko Bantlı Bomber Mont", 69.99, trendyolMan);
            Product sweatShirt = new Product("Siyah Erkek Kapüşonlu Kanguru Cepli Kadife Uzun Kollu Yeni Sweatshirt ", 59.99, trendyolMan);
            Product pants = new Product("Siyah Kemer Detaylı Pantolon", 79.99, trendyolMilla);

            IShoppingCart shoppingCart = new ShoppingCart(new DeliveryCostCalculator(1, 2));
            shoppingCart.AddItem(bomberJacket, 1);
            shoppingCart.AddItem(sweatShirt, 3);
            shoppingCart.AddItem(pants, 2);

            Campaign campaignTrendyolMan = new Campaign(category: trendyolMan, discountAmount: 20.0, minimumItemCount: 3, discountType: DiscountType.Rate);
            Campaign campaignTrendyolMilla = new Campaign(trendyolMilla, 10, 1, DiscountType.Amount);

            shoppingCart.ApplyDiscounts(campaignTrendyolMan, campaignTrendyolMilla);

            Coupon coupon = new Coupon(100, 10, DiscountType.Rate);
            shoppingCart.ApplyCoupon(coupon);

            var printResult = shoppingCart.Print();
            System.Console.WriteLine(printResult);
            System.Console.ReadKey();
        }
    }
}
