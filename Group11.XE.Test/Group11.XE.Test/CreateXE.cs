using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using Group11.XE.Test.XEDTO;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using Group11.XE.Test.Helper;


namespace Group11.XE.Test
{
    [TestClass]
    public class CreateXE : Load
    {

        public CreateXE()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        [DataRow("Airblade", "Den", 2, "Air 150", "51C4-12377", 37000000, 2, "Xe moi", 135,"Honda", 2019)]
        [DataRow("Winner", "Cam", 2, "Win 150", "51C4-12377", 40000000, 2, "Xe moi", 135, "Honda", 2019)]
        [DataRow("Exciter", "Xanh", 2, "Ex 150", "51C4-12377", 42000000, 2, "Xe moi", 135, "Yamaha", 2019)]
        public void Create_XE_With_OK_Status(String Name, String Color, int Seats, String Model, String License, int Price, int Consumption, String Notes, int Mspeed, String Manufacturer, int Manufacture_year)
        {
            String Code = (new Random().Next(1, 99999999)).ToString();


            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/xe-group11-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Name("xE_CODE")));
            Thread.Sleep(8000);

            //Act;
            driver.FindElement(By.Name("xE_CODE")).SendKeys(Code);
            driver.FindElement(By.Name("xE_NAME")).SendKeys(Name);
            driver.FindElement(By.Name("xE_COLOR")).SendKeys(Color);
            driver.FindElement(By.Name("xE_SEATS")).SendKeys(Seats.ToString());
            driver.FindElement(By.Name("xE_MODEL")).SendKeys(Model);
            driver.FindElement(By.Name("xE_LICENSE_PLATE")).SendKeys(License);
            driver.FindElement(By.Name("xE_PRICE")).SendKeys(Price.ToString());
            driver.FindElement(By.Name("xE_CONSUMPTION")).SendKeys(Consumption.ToString());
            driver.FindElement(By.Name("xE_NOTES")).SendKeys(Notes);
            driver.FindElement(By.Name("xE_MAX_SPEED")).SendKeys(Mspeed.ToString());
            driver.FindElement(By.Name("xE_MANUFACTURER")).SendKeys(Manufacturer);
            driver.FindElement(By.Name("xE_MANUFACTURE_YEAR")).SendKeys(Manufacture_year.ToString());
            //driver.FindElement(By.Name("xE_STATUS")).SendKeys(Status);

            Random rnd = new Random();
            int number = rnd.Next(0, 3);

            IWebElement comboBox = driver.FindElement(By.Name("xE_STATUS"));
            SelectElement selectedValue = new SelectElement(comboBox);
            selectedValue.SelectByIndex(number);
          
            String Status = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text;", selectedValue);
           
            //String Status = selectedValue.SelectByIndex(0).value;
            
            //get value of select element



            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            

            //Check with value of DB
            //CM_XE_DTO input = new CM_XE_DTO(Code, Name, Origin);
            XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH",new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.AreEqual(Code, finalRow.XE_CODE);
            Assert.AreEqual(Name, finalRow.XE_NAME);
            Assert.AreEqual(Color, finalRow.XE_COLOR);
            Assert.AreEqual(Seats, finalRow.XE_SEATS);
            Assert.AreEqual(Model, finalRow.XE_MODEL);
            Assert.AreEqual(License, finalRow.XE_LICENSE_PLATE);
            Assert.AreEqual(Price, finalRow.XE_PRICE);
            Assert.AreEqual(Consumption, finalRow.XE_CONSUMPTION);
            Assert.AreEqual(Notes, finalRow.XE_NOTES);
            Assert.AreEqual(Mspeed, finalRow.XE_MAX_SPEED);
            Assert.AreEqual(Manufacturer, finalRow.XE_MANUFACTURER);
            Assert.AreEqual(Manufacture_year, finalRow.XE_MANUFACTURE_YEAR);
            Assert.AreEqual(Status, finalRow.XE_STATUS);
            
            driver.Close();

        }

        [TestMethod]

        /*[DataRow("", "Winner", "Cam", 2, "Ex 150", "71C4-38677", 39000000, 2, "Xe vua mua", 135, "Yamaha", 2019)]*/
        [DataRow("1000", "", "Cam", 2, "Ex 150", "71C4-39677", 39000000, 2, "Xe vua mua", 135, "Yamaha", 2019)]
        [DataRow("1010", "Winner", "Cam", 2, "Ex 150", "", 39000000, 2, "Xe vua mua", 135, "Yamaha", 2019)]

        public void Create_XE_With_Empty(String Code, String Name, String Color, int Seats, string Model, string License, int Price, int Consumption, string Notes, int Mspeed, string Manufacturer, int Manufacture_year)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/xe-group11-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Name("xE_CODE")));
            Thread.Sleep(8000);

            //Act

            driver.FindElement(By.Name("xE_CODE")).SendKeys(Code);
            driver.FindElement(By.Name("xE_NAME")).SendKeys(Name);
            driver.FindElement(By.Name("xE_COLOR")).SendKeys(Color);
            driver.FindElement(By.Name("xE_SEATS")).SendKeys(Seats.ToString());
            driver.FindElement(By.Name("xE_MODEL")).SendKeys(Model);
            driver.FindElement(By.Name("xE_LICENSE_PLATE")).SendKeys(License);
            driver.FindElement(By.Name("xE_PRICE")).SendKeys(Price.ToString());
            driver.FindElement(By.Name("xE_CONSUMPTION")).SendKeys(Consumption.ToString());
            driver.FindElement(By.Name("xE_NOTES")).SendKeys(Notes);
            driver.FindElement(By.Name("xE_MAX_SPEED")).SendKeys(Mspeed.ToString());
            driver.FindElement(By.Name("xE_MANUFACTURER")).SendKeys(Manufacturer);
            driver.FindElement(By.Name("xE_MANUFACTURE_YEAR")).SendKeys(Manufacture_year.ToString());

            IWebElement comboBox = driver.FindElement(By.Name("xE_STATUS"));
            SelectElement selectedValue = new SelectElement(comboBox);
            selectedValue.SelectByIndex(0);

            String Status = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text;", selectedValue);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            //Assert.IsTrue(driver.FindElement(By.Name("empty_field")).Displayed);

            XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert

            Assert.IsNull(finalRow);

            driver.Close();
        }

        [TestMethod]
        
        [DataRow("1111", "Excitersssssssssssssssssssssssssssssssssssssssssssss", "Đen", 2, "Ex 150", "71C4-212121", 42000000, 2, "Xe vua mua", 135, "Yamaha", 2019)]
        /*[DataRow("112", "Exciter", "Đennnnnnnnnnnnnnnnnnnnnnnn", "2", "Ex 150", "71C4-45677", "42000000", "2,00", "Xe vừa mua", "135", "Yamaha", "2019", "tốt")]
        [DataRow("113", "Exciter", "Đen", "2", "Ex 150", "71C4-45677", "42000000", "2,00", "135", "Xe vừa mua", "Yamaha", "2019", "tốttttttttttttttttttttttttttttttttttttttttttttttttttt")]*/
        public void Create_XE_With_MaxSize(String Code, String Name, String Color, int Seats, string Model, string License, int Price, int Consumption, string Notes, int Mspeed, string Manufacturer, int Manufacture_year)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/xe-group11-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Name("xE_CODE")));
            Thread.Sleep(8000);

            //Act
            driver.FindElement(By.Name("xE_CODE")).SendKeys(Code);
            driver.FindElement(By.Name("xE_NAME")).SendKeys(Name);
            driver.FindElement(By.Name("xE_COLOR")).SendKeys(Color);
            driver.FindElement(By.Name("xE_SEATS")).SendKeys(Seats.ToString());
            driver.FindElement(By.Name("xE_MODEL")).SendKeys(Model);
            driver.FindElement(By.Name("xE_LICENSE_PLATE")).SendKeys(License);
            driver.FindElement(By.Name("xE_PRICE")).SendKeys(Price.ToString());
            driver.FindElement(By.Name("xE_CONSUMPTION")).SendKeys(Consumption.ToString());
            driver.FindElement(By.Name("xE_NOTES")).SendKeys(Notes);
            driver.FindElement(By.Name("xE_MAX_SPEED")).SendKeys(Mspeed.ToString());
            driver.FindElement(By.Name("xE_MANUFACTURER")).SendKeys(Manufacturer);
            driver.FindElement(By.Name("xE_MANUFACTURE_YEAR")).SendKeys(Manufacture_year.ToString());

            IWebElement comboBox = driver.FindElement(By.Name("xE_STATUS"));
            SelectElement selectedValue = new SelectElement(comboBox);
            selectedValue.SelectByIndex(1);

            String Status = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text;", selectedValue);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();
            //Assert

            Assert.IsNull(finalRow);
            driver.Close();
        }
        

        [TestMethod]
        [DataRow("003", "SH", "Trang", 2 , "SH 150", "71C4-331122", 62000000, 2, "Xe sh moi", 135, "Honda", 2019)]

        public void Create_XE_With_DuplicateCode(String Code, String Name, String Color, int Seats, string Model, string License, int Price, int Consumption, string Notes, int Mspeed, string Manufacturer, int Manufacture_year)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/xe-group11-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Name("xE_CODE")));
            Thread.Sleep(8000);

            //Act
            driver.FindElement(By.Name("xE_CODE")).SendKeys(Code);
            driver.FindElement(By.Name("xE_NAME")).SendKeys(Name);
            driver.FindElement(By.Name("xE_COLOR")).SendKeys(Color);
            driver.FindElement(By.Name("xE_SEATS")).SendKeys(Seats.ToString());
            driver.FindElement(By.Name("xE_MODEL")).SendKeys(Model);
            driver.FindElement(By.Name("xE_LICENSE_PLATE")).SendKeys(License);
            driver.FindElement(By.Name("xE_PRICE")).SendKeys(Price.ToString());
            driver.FindElement(By.Name("xE_CONSUMPTION")).SendKeys(Consumption.ToString());
            driver.FindElement(By.Name("xE_NOTES")).SendKeys(Notes);
            driver.FindElement(By.Name("xE_MAX_SPEED")).SendKeys(Mspeed.ToString());
            driver.FindElement(By.Name("xE_MANUFACTURER")).SendKeys(Manufacturer);
            driver.FindElement(By.Name("xE_MANUFACTURE_YEAR")).SendKeys(Manufacture_year.ToString());

            IWebElement comboBox = driver.FindElement(By.Name("xE_STATUS"));
            SelectElement selectedValue = new SelectElement(comboBox);
            selectedValue.SelectByIndex(1);

            String Status = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text;", selectedValue);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert.IsTrue(driver.FindElement(By.XPath("/html/body")).GetAttribute("class").Contains("swal-icon swal-icon--error"));
            XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();
            //Assert
            Assert.AreNotEqual(Name, finalRow.XE_NAME);
            /*Assert.AreEqual(Color, finalRow.XE_COLOR);
            Assert.AreEqual(Seats, finalRow.XE_SEATS);
            Assert.AreEqual(Model, finalRow.XE_MODEL);
            Assert.AreEqual(License, finalRow.XE_LICENSE_PLATE);
            Assert.AreEqual(Price, finalRow.XE_PRICE);
            Assert.AreEqual(Consumption, finalRow.XE_CONSUMPTION);
            Assert.AreEqual(Notes, finalRow.XE_NOTES);
            Assert.AreEqual(Mspeed, finalRow.XE_MAX_SPEED);
            Assert.AreEqual(Manufacturer, finalRow.XE_MANUFACTURER);
            Assert.AreEqual(Manufacture_year, finalRow.XE_MANUFACTURE_YEAR);
            Assert.AreEqual(Status, finalRow.XE_STATUS);*/

            driver.Close();
        }
            
        }
}
