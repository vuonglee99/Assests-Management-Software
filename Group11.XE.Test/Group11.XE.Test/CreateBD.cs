using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using Group11.XE.Test.BAODUONGDTO;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using Group11.XE.Test.Helper;

namespace Group11.XE.Test
{
    [TestClass]
    public class CreateBD : Load
    {

        public CreateBD()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        [DataRow("Hung","Quan 5,HCM")]
        public void Create_BD_With_OK_Status(String Garage,String Address)
        {


            Login();

            Random rnd = new Random();

            DateTime Bd_from_dt = new DateTime(2020, 9, 7);           
            DateTime Bd_to_dt = new DateTime(2020, 9, 15);

            String Code = (new Random().Next(1, 99999999)).ToString();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Name("bD_CODE")));
            Thread.Sleep(8000);



            //Act;



            driver.FindElement(By.Name("bD_CODE")).SendKeys(Code);
            driver.FindElement(By.Name("bD_FROM_DT")).SendKeys((Bd_from_dt).ToString());
            driver.FindElement(By.Name("bD_TO_DT")).SendKeys((Bd_to_dt).ToString());
            driver.FindElement(By.Name("bD_GARAGE")).SendKeys(Garage);
            driver.FindElement(By.Name("bD_ADDRESS")).SendKeys(Address);
           

            //driver.FindElement(By.Name("xE_STATUS")).SendKeys(Status);
            //Chon ngau nhien 1 Ma Xe
            
            int number = rnd.Next(0, 3);

            IWebElement comboBox = driver.FindElement(By.Name("xE_ID"));
            SelectElement selectedValue = new SelectElement(comboBox);
            selectedValue.SelectByIndex(number);

            
            String Xe_Id = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text;", selectedValue);



            //get value of select element



            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);


            //Check with value of DB
            //CM_XE_DTO input = new CM_XE_DTO(Code, Name, Origin);
            //PTX_DTO finalRow = DataProvider.Instance.GetData<PTX_DTO>("PTX_GetByCode", new { PTX_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--success__ring")).Displayed);




            driver.Close();

        }

        [TestMethod]
        [DataRow("","")]
        public void Create_BD_With_Empty(String Garage, String Address)
        {


            Login();

            Random rnd = new Random();


            String Code = (new Random().Next(1, 99999999)).ToString();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Name("bD_CODE")));
            Thread.Sleep(8000);



            //Act;



            driver.FindElement(By.Name("bD_CODE")).SendKeys(Code);
            driver.FindElement(By.Name("bD_GARAGE")).SendKeys(Garage);
            driver.FindElement(By.Name("bD_ADDRESS")).SendKeys(Address);


            //driver.FindElement(By.Name("xE_STATUS")).SendKeys(Status);
            //Chon ngau nhien 1 Ma Xe

            int number = rnd.Next(0, 3);

            IWebElement comboBox = driver.FindElement(By.Name("xE_ID"));
            SelectElement selectedValue = new SelectElement(comboBox);
            selectedValue.SelectByIndex(number);


            String Xe_Id = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text;", selectedValue);



            //get value of select element



            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);


            //Check with value of DB
            //CM_XE_DTO input = new CM_XE_DTO(Code, Name, Origin);
            //PTX_DTO finalRow = DataProvider.Instance.GetData<PTX_DTO>("PTX_GetByCode", new { PTX_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--error")).Displayed);




            driver.Close();

        }

        /*[TestMethod]

        [DataRow("333333333333333333333","Hung","Quan 5")]
        [DataRow("3214", "Tien Phattttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt", "Quan5")]
        [DataRow("3215", "Hung", "Quan 555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555")]
        public void Create_BD_With_Maxsize(String Code, String Garage, String Address)
        {


            Login();

            Random rnd = new Random();


            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Name("bD_CODE")));
            Thread.Sleep(8000);



            //Act;



            driver.FindElement(By.Name("bD_CODE")).SendKeys(Code);
            driver.FindElement(By.Name("bD_GARAGE")).SendKeys(Garage);
            driver.FindElement(By.Name("bD_ADDRESS")).SendKeys(Address);


            //driver.FindElement(By.Name("xE_STATUS")).SendKeys(Status);
            //Chon ngau nhien 1 Ma Xe

            int number = rnd.Next(0, 3);

            IWebElement comboBox = driver.FindElement(By.Name("xE_ID"));
            SelectElement selectedValue = new SelectElement(comboBox);
            selectedValue.SelectByIndex(number);


            String Xe_Id = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text;", selectedValue);



            //get value of select element



            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);


            //Check with value of DB
            //CM_XE_DTO input = new CM_XE_DTO(Code, Name, Origin);
            //PTX_DTO finalRow = DataProvider.Instance.GetData<PTX_DTO>("PTX_GetByCode", new { PTX_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--error")).Displayed);




            driver.Close();

        }

        */
        [TestMethod]
        [DataRow("Thanh Phat", "Quan 11")]
        public void Create_BD_With_InvalidDate(String Garage, String Address)
        {


            Login();

            Random rnd = new Random();

            DateTime Bd_from_dt = new DateTime(2020, 9, 15);
            DateTime Bd_to_dt = new DateTime(2020, 9, 7);

            String Code = (new Random().Next(1, 99999999)).ToString();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Name("bD_CODE")));
            Thread.Sleep(8000);



            //Act;



            driver.FindElement(By.Name("bD_CODE")).SendKeys(Code);
            driver.FindElement(By.Name("bD_FROM_DT")).SendKeys((Bd_from_dt).ToString());
            driver.FindElement(By.Name("bD_TO_DT")).SendKeys((Bd_to_dt).ToString());
            driver.FindElement(By.Name("bD_GARAGE")).SendKeys(Garage);
            driver.FindElement(By.Name("bD_ADDRESS")).SendKeys(Address);


            //driver.FindElement(By.Name("xE_STATUS")).SendKeys(Status);
            //Select random XE

            int number = rnd.Next(0, 3);

            IWebElement comboBox = driver.FindElement(By.Name("xE_ID"));
            SelectElement selectedValue = new SelectElement(comboBox);
            selectedValue.SelectByIndex(number);


            String Xe_Id = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text;", selectedValue);


            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);


            //Check with value of DB
            //CM_XE_DTO input = new CM_XE_DTO(Code, Name, Origin);
            //PTX_DTO finalRow = DataProvider.Instance.GetData<PTX_DTO>("PTX_GetByCode", new { PTX_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--error")).Displayed);




            driver.Close();

        }
    }
}

