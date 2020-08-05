using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using Selenium.Test.group4.DTO;
using Selenium.Test.group4.General;

namespace Selenium.Test.group4.Tests.ApartmentTests
{
    [TestClass]
    public class UpdateApartment : Load
    {
        public UpdateApartment()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";
        }

        [TestInitialize]
        public void InitializeTest()
        {
            Login();
        }

        [TestCleanup]
        public void CleanTest()
        {
            driver.Close();
        }
    }
}
