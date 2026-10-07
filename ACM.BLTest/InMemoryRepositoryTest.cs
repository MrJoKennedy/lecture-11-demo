using System;
using CMS.BusinessLayer;
using CMS.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CMS.BusinessLayerTest
{
    [TestClass]
    public class InMemoryRepositoryTest
    {
        [TestMethod]
        public void CustomerRepositoryAssignsIdAndReturnsIndependentCopies()
        {
            var customerRepo = new InMemoryCustomerRepository();
            var customerId = customerRepo.Save(new Customer
            {
                FirstName = "Frodo",
                LastName = "Brandybuck",
                EmailAddress = "frodo@example.com",
                HomeAddress = new Address { City = "Shire" }
            });

            var customer = customerRepo.GetById(customerId);
            Assert.AreEqual(1, customerId);
            Assert.AreEqual("Brandybuck", customer.LastName);
            Assert.AreEqual("Shire", customer.HomeAddress.City);

            customer.LastName = "Modified";
            customer.HomeAddress.City = "Modified";
            var storedCustomer = customerRepo.GetById(customerId);
            Assert.AreEqual("Brandybuck", storedCustomer.LastName);
            Assert.AreEqual("Shire", storedCustomer.HomeAddress.City);
            Assert.AreEqual(1, customerRepo.GetAll().Count);
        }

        [TestMethod]
        public void CustomerRepositoryUpdatesAnExistingId()
        {
            var customerRepo = new InMemoryCustomerRepository();
            customerRepo.Save(new Customer(8) { LastName = "Brandybuck" });
            customerRepo.Save(new Customer(8) { LastName = "Gamgee" });

            Assert.AreEqual("Gamgee", customerRepo.GetById(8).LastName);
            Assert.AreEqual(1, customerRepo.GetAll().Count);
        }

        [TestMethod]
        public void ProductRepositoryStoresAndRetrievesProduct()
        {
            var repository = new InMemoryProductRepository();
            var productId = repository.Save(new Product
            {
                ProductName = "Lembas",
                ProductDescription = "Elven bread",
                CurrentPrice = 5.50m
            });

            var product = repository.GetById(productId);
            Assert.AreEqual(1, product.ProductId);
            Assert.AreEqual("Lembas", product.ProductName);
            Assert.AreEqual(5.50m, product.CurrentPrice);
        }

        [TestMethod]
        public void OrderRepositoryCopiesShippingAddress()
        {
            var repository = new InMemoryOrderRepository();
            var orderId = repository.Save(new Order
            {
                CustomerId = 4,
                OrderDate = new DateTimeOffset(2026, 10, 6, 0, 0, 0, TimeSpan.Zero),
                ShippingAddress = new Address { City = "Rivendell" }
            });

            var order = repository.GetById(orderId);
            Assert.AreEqual(1, order.OrderId);
            Assert.AreEqual(4, order.CustomerId);
            Assert.AreEqual("Rivendell", order.ShippingAddress.City);
            order.ShippingAddress.City = "Modified";
            Assert.AreEqual("Rivendell", repository.GetById(orderId).ShippingAddress.City);
        }

        [TestMethod]
        public void OrderItemRepositoryStoresAndRetrievesOrderItem()
        {
            var repository = new InMemoryOrderItemRepository();
            var orderItemId = repository.Save(new OrderItem
            {
                OrderId = 3,
                OrderQuantity = 2,
                ProductId = 7,
                PurchasePrice = 9.75m
            });

            var orderItem = repository.GetById(orderItemId);
            Assert.AreEqual(1, orderItem.OrderItemId);
            Assert.AreEqual(3, orderItem.OrderId);
            Assert.AreEqual(2, orderItem.OrderQuantity);
            Assert.AreEqual(7, orderItem.ProductId);
            Assert.AreEqual(9.75m, orderItem.PurchasePrice);
        }

        [TestMethod]
        public void RepositoriesReturnNullWhenRecordIsMissing()
        {
            Assert.IsNull(new InMemoryCustomerRepository().GetById(1));
            Assert.IsNull(new InMemoryProductRepository().GetById(1));
            Assert.IsNull(new InMemoryOrderRepository().GetById(1));
            Assert.IsNull(new InMemoryOrderItemRepository().GetById(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CustomerRepositoryRejectsNull()
        {
            new InMemoryCustomerRepository().Save(null);
        }
    }
}
