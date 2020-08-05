using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using Selenium.Test.group5.DEVICES_DTO;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace Selenium.Test.group5
{
    [TestClass]
    public class TestDEVICES : Load
    {
        public TestDEVICES()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";
        }

        [TestMethod]
        [DataRow("11","","BR01")]
        public void Search_By_DVCode(String code, String name, String branch)
        {
            //Login();

            //System.Threading.Thread.Sleep(3000);

            //driver.Navigate().GoToUrl(homeURL + "/app/admin/thiet-bi");
            //System.Threading.Thread.Sleep(5000);

            //WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            //wait2.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[2]/div/div[1]/div[2]/input")));


            //driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[2]/div/div[1]/div[2]/input")).SendKeys(code);
            //System.Threading.Thread.Sleep(3000);
            //driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[1]/div")).Click();

            //System.Threading.Thread.Sleep(5000);

            //List<CM_DEV_DTO> listRow = DataProvider.Instance.GetData<CM_DEV_DTO>("DEVICE_Search", new 
            //{
            //    INDEX = 1,
            //    DEVICE_CODE = code,
            //    BRANCH_ID = branch,
            //    DEVICE_NAME = name
            //}).ToList();


            //IList<IWebElement> pages = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/p-table/div/p-paginator/div/span")).FindElements(By.TagName("a"));
            ////Retrieve from DOM

            //IList<CM_DEV_DTO> rowFromWeb = new List<CM_DEV_DTO>();
            //foreach (var item in pages)
            //{
            //    try
            //    {
            //        item.Click();
            //    }
            //    catch (Exception e)
            //    {
            //    }
            //    finally
            //    {
            //        IWebElement table = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/p-table/div/div/table")).FindElement(By.TagName("tbody"));
            //        foreach (var rowData in table.FindElements(By.TagName("tr")))
            //        {
            //            IList<IWebElement> fields = rowData.FindElements(By.TagName("td"));
            //            if (fields.ElementAt(0).Text != "Không có dữ liệu")
            //            {
            //                if (fields.ElementAt(1) != null)
            //                {
            //                    CM_DEV_DTO row = new CM_DEV_DTO();
            //                    row.DEVICE_CODE = fields.ElementAt(1).Text;
            //                    row.DEVICE_NAME = fields.ElementAt(2).Text;
            //                    rowFromWeb.Add(row);
            //                }
            //            }
            //        }
            //    }
            //}



            ////Test
            //if (rowFromWeb.Count != listRow.Count)
            //{
            //    Assert.Fail("sai so luong Devices:" + rowFromWeb.Count.ToString() + "!=" + listRow.Count.ToString());
            //}
            //foreach (var item in rowFromWeb)
            //{
            //    if (!listRow.Any(i => i.DEVICE_CODE == item.DEVICE_CODE && i.DEVICE_NAME == item.DEVICE_NAME))
            //    {
            //        Assert.Fail("sai Devices");
            //    }
            //}

            //System.Threading.Thread.Sleep(2000);
            //driver.Close();
            driver.Close();

        }

        [TestMethod]
        [DataRow("updated")]
        public void Update(String newName)
        {
            Login();

            System.Threading.Thread.Sleep(3000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/thiet-bi");
            System.Threading.Thread.Sleep(5000);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120.0));

            //Page
            IList<IWebElement> pages = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/p-table/div/p-paginator/div/span")).FindElements(By.TagName("a"));
            //Retrieve from DOM

            IWebElement table = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/p-table/div/div/table")).FindElement(By.TagName("tbody"));

            List<IWebElement> rows = table.FindElements(By.TagName("tr")).ToList();

            //random row
            if (rows[0].Text == "Không có dữ liệu")
            {
                driver.Close();
                return;
            }
            int index = rows.Count / 2 + 1;
            IWebElement rowChoose = rows.ElementAt(index);
            rowChoose.Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/div/button[2]")).Click();
            System.Threading.Thread.Sleep(5000);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div/div/div[1]/div[2]/input")).SendKeys(newName);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/div/button")).Click();
            System.Threading.Thread.Sleep(3000);
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div/div/div[1]")).GetAttribute("class").Contains("swal-icon--success"));
            driver.Close();
        }
        
        [DataRow("DV001")]
        public void DeleteWithCheckData(String code)
        {
            Login();

            System.Threading.Thread.Sleep(3000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/thiet-bi");
            System.Threading.Thread.Sleep(5000);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120.0));

            //Page
            IList<IWebElement> pages = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/p-table/div/p-paginator/div/span")).FindElements(By.TagName("a"));
            //Retrieve from DOM

            IWebElement table = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/p-table/div/div/table")).FindElement(By.TagName("tbody"));
            wait.Until(driver => table.FindElements(By.TagName("tr")));

            List<IWebElement> rows = table.FindElements(By.TagName("tr")).ToList();

            //random row
            int index = rows.Count / 2 + 1;
            IWebElement rowChoose = rows.ElementAt(index);

            //Get data row Web
            IList<IWebElement> fields = rowChoose.FindElements(By.TagName("td"));
            string id = fields.ElementAt(0).Text;
            string status = fields.ElementAt(4).Text;
            string Dep_name = fields.ElementAt(2).Text;

            //Get data from db
            CM_DEV_DTO fromDb = DataProvider.Instance.GetData<CM_DEV_DTO>("DEPARTMENT_Search", new
            {
                DEP_CODE = id,
                DEP_NAME = "",
                BRANCH_ID = "",
                RECORD_STATUS = ""
            }).FirstOrDefault();

            //check Pre-execute
            Assert.AreEqual(status, fromDb.RECORD_STATUS);

            IWebElement deleteButton = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/div/button[3]"));
            //deleteButton.Click();

            string dialog = "/html/body/div";
            //IWebElement confirmDiaLog = driver.FindElement(By.XPath(dialog));
            //if (!confirmDiaLog.Displayed)
            //{
            //    Assert.Fail("Loi UI hoac Khong Chon Duoc");
            //}
            //string title = confirmDiaLog.FindElement(By.ClassName("swal-title")).Text;
            //Assert.AreEqual("Không có dữ liệu để xóa", title);
            //confirmDiaLog.FindElement(By.XPath("/html/body/div/div/div[3]/div/button")).Click();

            //Execute
            rowChoose.Click();
            string classes = rowChoose.GetAttribute("class");
            if (!classes.Contains("ui-state-highlight"))
            {
                Assert.Fail("Loi UI hoac Khong Chon Duoc");
            }

            deleteButton = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/div/button[3]"));
            deleteButton.Click();

            IWebElement confirmDiaLog = driver.FindElement(By.XPath(dialog));
            if (!confirmDiaLog.Displayed)
            {
                Assert.Fail("Loi UI hoac Khong Chon Duoc");
            }

            IWebElement confirmButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div[4]/div[2]/button")));

            confirmButton.Click();

            //result
            System.Threading.Thread.Sleep(6000);
            IWebElement resultDialog = driver.FindElement(By.XPath(dialog));
            string title = resultDialog.FindElement(By.ClassName("swal-title")).Text;
            //Check
            if (status == "0")
            {
                Assert.AreEqual(Dep_name + " đã bị xóa", title);
                fromDb = DataProvider.Instance.GetData<CM_DEV_DTO>("DEPARTMENT_Search", new
                {
                    DEP_CODE = id,
                    DEP_NAME = "",
                    BRANCH_ID = "",
                    RECORD_STATUS = ""
                }).FirstOrDefault();
                Assert.AreEqual(fromDb.RECORD_STATUS, "0");
            }
            else
            {
                Assert.AreEqual("Xóa thành công", title);
                fromDb = DataProvider.Instance.GetData<CM_DEV_DTO>("DEPARTMENT_Search", new
                {
                    DEP_CODE = id,
                    DEP_NAME = "",
                    BRANCH_ID = "",
                    RECORD_STATUS = ""
                }).FirstOrDefault();
                Assert.AreEqual(fromDb.RECORD_STATUS, "0");
            }
            driver.Close();
        }


        [TestMethod]
        public void Delete()
        {
            Login();

            System.Threading.Thread.Sleep(3000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/thiet-bi");
            System.Threading.Thread.Sleep(5000);
            

            IWebElement table = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/p-table/div/div/table")).FindElement(By.TagName("tbody"));

            List<IWebElement> rows = table.FindElements(By.TagName("tr")).ToList();

            //random row
            if (rows[0].Text == "Không có dữ liệu")
            {
                driver.Close();
                return;
            }
            int index = rows.Count / 2 + 1;
            IWebElement rowChoose = rows.ElementAt(index);
            rowChoose.Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/div/button[3]")).Click();
            System.Threading.Thread.Sleep(5000);

            driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[2]/button")).Click();
            System.Threading.Thread.Sleep(3000);
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div/div/div[1]")).GetAttribute("class").Contains("swal-icon--success"));
            driver.Close();
        }
        [TestMethod]
        [DataRow("điện thoại","001123")]
        public void Insert(String name, String serial)
        {
            Login();

            System.Threading.Thread.Sleep(3000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/them-thiet-bi");
            System.Threading.Thread.Sleep(5000);
            

            //Act
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[1]/div[2]/input")).SendKeys(name);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[2]/div[4]/input")).SendKeys(serial);
            //ngay mua
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[3]/div/div[1]/div[2]/p-calendar/span/input")).Click();
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[3]/div/div[1]/div[2]/p-calendar/span/div/table/tbody")).FindElements(By.TagName("tr"))[3].FindElements(By.TagName("td"))[0].Click();
            System.Threading.Thread.Sleep(1000);
            //ngay bao hanh
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[3]/div/div[2]/div[2]/p-calendar/span/input")).Click();
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[3]/div/div[2]/div[2]/p-calendar/span/div/table/tbody")).FindElements(By.TagName("tr"))[3].FindElements(By.TagName("td"))[0].Click();
            System.Threading.Thread.Sleep(1000);
            //ngay het bao hanh
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[3]/div/div[3]/div[2]/p-calendar/span/input")).Click();
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[3]/div/div[3]/div[2]/p-calendar/span/div/table/tbody")).FindElements(By.TagName("tr"))[3].FindElements(By.TagName("td"))[0].Click();
            System.Threading.Thread.Sleep(1000);
            //nam san xuat
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[3]/div/div[4]/div[2]/select")).FindElements(By.TagName("option"))[1].Click();

            //Chu Ki Bao Duong
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[1]/div[6]/select")).FindElements(By.TagName("option"))[1].Click();

            //Tinh Trang
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[1]/div[8]/select")).FindElements(By.TagName("option"))[1].Click();

            //Nha san xuat
            string id = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[2]/div[2]/datalist")).FindElements(By.TagName("option"))[0].GetAttribute("value");
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[2]/div[2]/input")).SendKeys(id);
            //Don vi quan li
            string idDonVi = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[1]/div[4]/datalist")).FindElements(By.TagName("option"))[0].GetAttribute("value");
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div[1]/div[4]/input")).SendKeys(idDonVi);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[1]/button")).Click();
            System.Threading.Thread.Sleep(2000);
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div/div/div[1]")).GetAttribute("class").Contains("swal-icon--success"));
            driver.Close();

        }
    }
}
