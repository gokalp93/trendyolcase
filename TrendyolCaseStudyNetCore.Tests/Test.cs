using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyolCaseStudyNetCore.Core.Entity;
using static TrendyolCaseStudyNetCore.Core.Enum.Enum;

namespace TrendyolCase.Tests
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void AddItemToShoppingChart()
        {
            var sut = new ShoppingCart(new DeliveryCostCalculator(1, 5));
            var product = new Product("Nike Sneaker", 299.559, new Category("Sneaker"));
            sut.AddItem(product, 100);
        }

        [Test]
        public void ReturnNumberOfDeliveriesIsEqual1()
        {
            var sut = new ShoppingCart(new DeliveryCostCalculator(1, 5));
            var product = new Product("Nike Sneaker", 299.559, new Category("Sneaker"));
            sut.AddItem(product, 1);

            Assert.AreEqual(sut.NumberOfDeliveries, 1);
        }

        [Test]
        public void ReturnNumberOfDistinctCategoriesIsEqual2()
        {
            var sut = new ShoppingCart(new DeliveryCostCalculator(1, 5));
            var product = new Product("Nike Sneaker", 299.559, new Category("Sneaker"));
            var product2 = new Product("Nike Tshirt", 299.559, new Category("Tshirt"));

            sut.AddItem(product, 1);
            sut.AddItem(product2, 2);


            Assert.AreEqual(sut.NumberOfProducts, 2);
        }

        [Test]
        public void ReturnItemCountIsEqual3()
        {
            var sut = new ShoppingCart(new DeliveryCostCalculator(1, 5));
            var product = new Product("Nike Sneaker", 299.559, new Category("Sneaker"));
            var product2 = new Product("Nike Tshirt", 299.559, new Category("Tshirt"));
            var product3 = new Product("Nike Shorts", 299.559, new Category("Shorts"));


            sut.AddItem(product, 1);
            sut.AddItem(product2, 2);
            sut.AddItem(product3, 3);


            Assert.AreEqual(sut.GetItemCount(), 3);
        }

        [Test]
        public void GetCampaingDiscountIsEqual5999()
        {
            Category trendyolMan = new Category("Trendyol Man");
            Category trendyolMilla = new Category("Trendyol Milla");

            Product bomberJacket = new Product("Lacivert Erkek Yanı Şeritli Çizgili Triko Bantlı Bomber Mont", 69.99, trendyolMan);
            Product sweatShirt = new Product("Siyah Erkek Kapüşonlu Kanguru Cepli Kadife Uzun Kollu Yeni Sweatshirt ", 59.99, trendyolMan);
            Product pants = new Product("Siyah Kemer Detaylı Pantolon", 79.99, trendyolMilla);

            IShoppingCart sut = new ShoppingCart(new DeliveryCostCalculator(1, 2));
            sut.AddItem(bomberJacket, 1);
            sut.AddItem(sweatShirt, 3);
            sut.AddItem(pants, 2);

            Campaign campaignTrendyolMan = new Campaign(category: trendyolMan, discountAmount: 20.0, minimumItemCount: 3, discountType: DiscountType.Rate);
            Campaign campaignTrendyolMilla = new Campaign(trendyolMilla, 10, 1, DiscountType.Amount);

            sut.ApplyDiscounts(campaignTrendyolMan, campaignTrendyolMilla);


            Assert.AreEqual(sut.GetCampaingDiscount(), 59.99, 0.01);
        }

        [Test]
        public void GetCouponDiscount_IsEqual4099()
        {
            Category trendyolMan = new Category("Trendyol Man");
            Category trendyolMilla = new Category("Trendyol Milla");

            Product bomberJacket = new Product("Lacivert Erkek Yanı Şeritli Çizgili Triko Bantlı Bomber Mont", 69.99, trendyolMan);
            Product sweatShirt = new Product("Siyah Erkek Kapüşonlu Kanguru Cepli Kadife Uzun Kollu Yeni Sweatshirt ", 59.99, trendyolMan);
            Product pants = new Product("Siyah Kemer Detaylı Pantolon", 79.99, trendyolMilla);

            IShoppingCart sut = new ShoppingCart(new DeliveryCostCalculator(1, 2));
            sut.AddItem(bomberJacket, 1);
            sut.AddItem(sweatShirt, 3);
            sut.AddItem(pants, 2);

            Coupon coupon = new Coupon(100, 10, DiscountType.Rate);
            sut.ApplyCoupon(coupon);

            Assert.AreEqual(sut.GetCouponDiscount(), 40.99, 0.01);
        }

    }
}
