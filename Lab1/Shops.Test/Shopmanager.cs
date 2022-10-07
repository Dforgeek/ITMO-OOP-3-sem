using Shops.Entities;
using Shops.Exceptions;
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
        var shopmanager = new Services.ShopManager();
        Shop diksi = shopmanager.RegisterShop("Diksi", "Kronverksky, 69");
        Shop pyaterochka = shopmanager.RegisterShop("Pyaterochka", "Kronverksky, 90");
        var cookies = new Product("Oreo", Guid.NewGuid());
        var tomato = new Product("Tomato", Guid.NewGuid());

        var shipmentBuilder = new ShipmentBuilder();
        shipmentBuilder
            .AddShopProduct(new ShopProduct(cookies, new Money(10), 100));
        shipmentBuilder
            .AddShopProduct(new ShopProduct(tomato, new Money(40), 10));
        Shipment shipment = shipmentBuilder.Build();
        diksi.ShipmentDelivery(shipment);

        shipmentBuilder
            .AddShopProduct(new ShopProduct(cookies, new Money(5), 100));
        shipmentBuilder
            .AddShopProduct(new ShopProduct(tomato, new Money(25), 10));
        shipment = shipmentBuilder.Build();
        pyaterochka.ShipmentDelivery(shipment);

        var orderBuilder = new OrderBuilder();
        orderBuilder.AddCustomerProduct(new CustomerProduct(cookies, 99));
        orderBuilder.AddCustomerProduct(new CustomerProduct(tomato, 10));
        Order order = orderBuilder.Build();
        Shop cheapestShop = shopmanager.CheapestShopToOrder(order);
        Assert.Equal(cheapestShop.Id.ToString(), pyaterochka.Id.ToString());

        orderBuilder.AddCustomerProduct(new CustomerProduct(new Product("Vodka", Guid.NewGuid()), 10));
        Order exceptionOrder = orderBuilder.Build();
        Assert.Throws<ShopException>(() => shopmanager.CheapestShopToOrder(exceptionOrder));
    }

    [Fact]
    public void CreateShopAndMakeShipment_ShopGetsProducts_ThrowException_CustomerNotEnoughMoney()
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

        var customer = new Customer("Stas Baretsky", new Money(1000));
        Assert.Throws<ShopException>(() => shop.Buy(customer, order));
        customer.EarnMoney(new Money(500));
        shop.Buy(customer, order);
        Assert.Equal(110, customer.Money.Value);
        Assert.Equal(1U, shop.GetProduct(moloko.Id).Quantity);
        Assert.Equal(0U, shop.GetProduct(ogurec.Id).Quantity);
    }
}