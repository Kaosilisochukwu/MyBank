using MyBankApp;
using MyBankApp.ClassLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBankTest
{
    [TestFixture]
    public class MyBankTests
    {
        [Test]
        public void createCustomer()
        {
            var sut = new Customer("Kaosi", "Nwizu", "email", "pass");
            var suti = new Customer("Kaosi", "Nwizu", "email", "piss");

            Assert.That(sut.CustomerId, Is.EqualTo(1));
            Assert.That(suti.CustomerId, Is.EqualTo(2));
        }
    }
}
