using Shops.Entities;
using Shops.Models;
using Xunit;

namespace Shops.Test;

public class Shopmanager
{
    [Fact]
    public void CreateShopAndMakeShipment_ShopGetsProducts_CustomerCanBuyIt()
    {
        var shopmanager = new Services.ShopManager();
        Shop shop = shopmanager.RegisterShop("Avtomoika", "Улица Герцена 94 Тюмень за ТЦ Вояж");
        var shipmentBuilder = new ShipmentBuilder();
        var moloko = new Product("Moloko+", Guid.NewGuid());
        var ogurec = new Product("Ogurec", Guid.NewGuid());

        shipmentBuilder
            .AddShopProduct(new ShopProduct(moloko, new Money(10), 100));
        shipmentBuilder
            .AddShopProduct(new ShopProduct(ogurec, new Money(40), 10));
        Shipment shipment = shipmentBuilder.Build();
        shop.ShipmentDelivery(shipment);

        var orderBuilder = new OrderBuilder();
        orderBuilder.AddCustomerProduct(new CustomerProduct(moloko, 99));
        orderBuilder.AddCustomerProduct(new CustomerProduct(ogurec, 10));
        Order order = orderBuilder.Build();

        var customer = new Customer("Stas Baretsky", new Money(1400));
        shop.Buy(customer, order);
        Assert.Equal(10, customer.Money.Value);
        Assert.Equal(1U, shop.GetProduct(moloko.Id).Quantity);
        Assert.Equal(0U, shop.GetProduct(ogurec.Id).Quantity);
    }

    [Fact]
    public void SetAndChangePrice()
    {
        var shopmanager = new Services.ShopManager();
        Shop shop = shopmanager.RegisterShop("Jopochki mladencev", "Улица Герцена 94 Тюмень за ТЦ Вояж");
        var shipmentBuilder = new ShipmentBuilder();
        var product = new Product("Podguznik", Guid.NewGuid());
        shipmentBuilder.AddShopProduct(new ShopProduct(product, new Money(100)));
        shop.ShipmentDelivery(shipmentBuilder.Build());
        Assert.Equal(100, shop.GetProduct(product.Id).Price.Value);
        shop.ChangePrice(product.Id, new Money(200));
        Assert.Equal(200, shop.GetProduct(product.Id).Price.Value);
    }

    [Fact]
    public void CheapestOrder_NoSuchListOfProductsInAnyShop_ThrowException()
    {
        
    }
}