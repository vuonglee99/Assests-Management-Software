using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Selenium.Test.group7
{
    [TestClass]
    public class TestNCU : Load
    {
        public TestNCU()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";
        }

        [TestMethod]
        [DataRow("NCU00001", "nhà cung ứng 1", "42 Hoa Huệ", "0123456789", "0123456789", "0123456789", "Nguyễn Huy", "email@gmail.com", "0123456789")]
        public void AddNCU(String id, String name, String Address, String phone, String tax, String fax, String personName, String email, String contactPhone)
        {
            Login();

            System.Threading.Thread.Sleep(2000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/nha-cung-ung-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Act;
            driver.FindElement(By.Name("ncU_MA_NCU")).SendKeys(id);
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Name("ncU_TEN")).SendKeys(name);
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Name("ncU_DIA_CHI")).SendKeys(Address);
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Name("ncU_SDT")).SendKeys(phone);
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Name("ncU_MA_SO_THUE")).SendKeys(tax);
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Name("ncU_FAX")).SendKeys(fax);
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Name("ncU_TEN_NGUOI_LIEN_HE")).SendKeys(personName);
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Name("ncU_EMAIL_NGUOI_LIEN_HE")).SendKeys(email);
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Name("ncU_SDT_NGUOI_LIEN_HE")).SendKeys(contactPhone);

            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.ClassName("fa-floppy-o")).Click();

            System.Threading.Thread.Sleep(5000);

            //Check with value of DB

            driver.Close();
        }

        [TestMethod]
        [DataRow("NCU-003", "nhà cung ứng 1", "42 Hoa Huệ", "0123456789", "0123456789", "0123456789", "Nguyễn Huy", "email@gmail.com", "0123456789")]
        public void editNCU(String id, String name, String Address, String phone, String tax, String fax, String personName, String email, String contactPhone)
        {
            Login();

            System.Threading.Thread.Sleep(2000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/nha-cung-ung-edit/" + id);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Act;
            // driver.FindElement(By.Name("ncU_MA_NCU")).SendKeys(id);
            // System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Name("ncU_TEN")).Clear();
            driver.FindElement(By.Name("ncU_TEN")).SendKeys(name);
            System.Threading.Thread.Sleep(1000);

            driver.FindElement(By.Name("ncU_DIA_CHI")).Clear();
            driver.FindElement(By.Name("ncU_DIA_CHI")).SendKeys(Address);
            System.Threading.Thread.Sleep(1000);

            driver.FindElement(By.Name("ncU_SDT")).Clear();
            driver.FindElement(By.Name("ncU_SDT")).SendKeys(phone);
            System.Threading.Thread.Sleep(1000);

            driver.FindElement(By.Name("ncU_MA_SO_THUE")).Clear();
            driver.FindElement(By.Name("ncU_MA_SO_THUE")).SendKeys(tax);
            System.Threading.Thread.Sleep(1000);

            driver.FindElement(By.Name("ncU_FAX")).Clear();
            driver.FindElement(By.Name("ncU_FAX")).SendKeys(fax);
            System.Threading.Thread.Sleep(1000);

            driver.FindElement(By.Name("ncU_TEN_NGUOI_LIEN_HE")).Clear();
            driver.FindElement(By.Name("ncU_TEN_NGUOI_LIEN_HE")).SendKeys(personName);
            System.Threading.Thread.Sleep(1000);

            driver.FindElement(By.Name("ncU_EMAIL_NGUOI_LIEN_HE")).Clear();
            driver.FindElement(By.Name("ncU_EMAIL_NGUOI_LIEN_HE")).SendKeys(email);
            System.Threading.Thread.Sleep(1000);

            driver.FindElement(By.Name("ncU_SDT_NGUOI_LIEN_HE")).Clear();
            driver.FindElement(By.Name("ncU_SDT_NGUOI_LIEN_HE")).SendKeys(contactPhone);

            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.ClassName("fa-floppy-o")).Click();

            System.Threading.Thread.Sleep(2000);

            
            // Act:
            // go back to view NCU, check field has updated
            driver.Navigate().GoToUrl(homeURL + "/app/admin/nha-cung-ung-view/" + id);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Act;
            // driver.FindElement(By.Name("ncU_MA_NCU")).SendKeys(id);
            // System.Threading.Thread.Sleep(1000);
            var nameE = driver.FindElement(By.Name("ncU_TEN"));
            System.Threading.Thread.Sleep(1000);
            var addressE = driver.FindElement(By.Name("ncU_DIA_CHI"));
            System.Threading.Thread.Sleep(1000);
            var phoneE = driver.FindElement(By.Name("ncU_SDT"));
            System.Threading.Thread.Sleep(1000);
            var taxE = driver.FindElement(By.Name("ncU_MA_SO_THUE"));
            System.Threading.Thread.Sleep(1000);
            var faxE = driver.FindElement(By.Name("ncU_FAX"));
            System.Threading.Thread.Sleep(1000);
            var nameCE = driver.FindElement(By.Name("ncU_TEN_NGUOI_LIEN_HE"));
            System.Threading.Thread.Sleep(1000);
            var emailE = driver.FindElement(By.Name("ncU_EMAIL_NGUOI_LIEN_HE"));
            System.Threading.Thread.Sleep(1000);
            var phoneCE = driver.FindElement(By.Name("ncU_SDT_NGUOI_LIEN_HE"));

            Assert.AreEqual(nameE.Text.ToString(), name);
            Assert.AreEqual(addressE.Text.ToString(), Address);
            Assert.AreEqual(phoneE.Text.ToString(), phone);
            Assert.AreEqual(taxE.Text.ToString(), tax);
            Assert.AreEqual(faxE.Text.ToString(), fax);
            Assert.AreEqual(nameCE.Text.ToString(), personName);
            Assert.AreEqual(emailE.Text.ToString(), email);
            Assert.AreEqual(phoneCE.Text.ToString(), contactPhone);


            //Check with value of DB
            System.Threading.Thread.Sleep(2000);

            driver.Close();
        }
    }
}
