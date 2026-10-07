using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMS.BusinessLayer;

namespace CMS.BusinessLayerTest
{
    [TestClass]
    public class CustomerTest
    {
      
        [TestMethod]
        public void FullNameReturnsCombinedName()
        {
            //-- Arrange
            var client = new Customer();
            client.FirstName = "Frodo";
            client.LastName = "Brandybuck";

            string expected = "Brandybuck, Frodo";

            //-- Act
            string actual = client.FullName;

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FullNameWithoutFirstName()
        {
            //-- Arrange
            var client = new Customer();
            client.LastName = "Brandybuck";
            string expected = "Brandybuck";

            //-- Act
            string actual = client.FullName;

            //-- Assert
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void FullNameWithoutLastName()
        {
            //-- Arrange
            var client = new Customer();
            client.FirstName = "Frodo";
            string expected = "Frodo";

            //-- Act
            string actual = client.FullName;

            //-- Assert
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void ValidCustomerPassesValidation()
        {
            //-- Arrange
            var client = new Customer();
            client.LastName = "Brandybuck";
            client.EmailAddress = "frodo@shire.me";

            var expected = true;

            //-- Act
            var actual = client.Validate();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CustomerWithoutLastNameFailsValidation()
        {
            //-- Arrange
            var client = new Customer();
            client.EmailAddress = "frodo@shire.me";

            var expected = false;

            //-- Act
            var actual = client.Validate();

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

    }

}
