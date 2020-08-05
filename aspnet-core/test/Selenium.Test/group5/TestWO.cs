using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using Selenium.Test.group5.WO_DTO;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace Selenium.Test.group5
{
    [TestClass]
    public class TestWO : Load
    {
        public TestWO()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";
        }


        [TestMethod]
        [DataRow("26")]
        public void Search_By_WOCode(String code)
        {
            Login();

            System.Threading.Thread.Sleep(3000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-tri-bao-duong");
            System.Threading.Thread.Sleep(5000);

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait2.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[2]/div/div[1]/div[2]/input")));


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[2]/div/div[1]/div[2]/input")).SendKeys(code);
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[1]/div")).Click();

            System.Threading.Thread.Sleep(5000);

            List<CM_WO_DTO> listRow = DataProvider.Instance.GetData<CM_WO_DTO>("WORK_ORDER_Search", new CM_WO_DTO
            {
                WO_CODE = code,
            }).ToList();


            IList<IWebElement> pages = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[3]/p-table/div/p-paginator/div/span")).FindElements(By.TagName("a"));
            //Retrieve from DOM

            IList<CM_WO_DTO> rowFromWeb = new List<CM_WO_DTO>();
            foreach (var item in pages)
            {
                try
                {
                    item.Click();
                }
                catch (Exception e)
                {
                }
                finally
                {
                    IWebElement table = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[3]/p-table/div/div/table")).FindElement(By.TagName("tbody"));
                    foreach (var rowData in table.FindElements(By.TagName("tr")))
                    {
                        IList<IWebElement> fields = rowData.FindElements(By.TagName("td"));
                        if (fields.ElementAt(0).Text != "Không có dữ liệu")
                        {
                            if (fields.ElementAt(1) != null)
                            {
                                CM_WO_DTO row = new CM_WO_DTO();
                                row.WO_CODE = fields.ElementAt(1).Text;
                                row.DESCRIPTIONS = fields.ElementAt(2).Text;
                                rowFromWeb.Add(row);
                            }
                        }
                    }
                }
            }
            //Test
            if (rowFromWeb.Count != listRow.Count)
            {
                Assert.Fail("sai so luong Department:" + rowFromWeb.Count.ToString() + "!=" + listRow.Count.ToString());
            }
            foreach (var item in rowFromWeb)
            {
                if (!listRow.Any(i => i.WO_CODE == item.WO_CODE && i.DESCRIPTIONS == item.DESCRIPTIONS))
                {
                    Assert.Fail("sai Department");
                }
            }

            System.Threading.Thread.Sleep(2000);
            driver.Close();
        }

        [TestMethod]
        [DataRow("ABC")]
        public void Search_By_WOCode2(String code)
        {
            Login();

            System.Threading.Thread.Sleep(3000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-tri-bao-duong");
            System.Threading.Thread.Sleep(5000);

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait2.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[2]/div/div[1]/div[2]/input")));


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[2]/div/div[1]/div[2]/input")).SendKeys(code);
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[1]/div")).Click();

            System.Threading.Thread.Sleep(5000);

            List<CM_WO_DTO> listRow = DataProvider.Instance.GetData<CM_WO_DTO>("WORK_ORDER_Search", new CM_WO_DTO
            {
                WO_CODE = code,
            }).ToList();


            IList<IWebElement> pages = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[3]/p-table/div/p-paginator/div/span")).FindElements(By.TagName("a"));
            //Retrieve from DOM

            IList<CM_WO_DTO> rowFromWeb = new List<CM_WO_DTO>();
            foreach (var item in pages)
            {
                try
                {
                    item.Click();
                }
                catch (Exception e)
                {
                }
                finally
                {
                    IWebElement table = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[3]/p-table/div/div/table")).FindElement(By.TagName("tbody"));
                    foreach (var rowData in table.FindElements(By.TagName("tr")))
                    {
                        IList<IWebElement> fields = rowData.FindElements(By.TagName("td"));
                        if (fields.ElementAt(0).Text != "Không có dữ liệu")
                        {
                            if (fields.ElementAt(1) != null)
                            {
                                CM_WO_DTO row = new CM_WO_DTO();
                                row.WO_CODE = fields.ElementAt(1).Text;
                                row.DESCRIPTIONS = fields.ElementAt(2).Text;
                                rowFromWeb.Add(row);
                            }
                        }
                    }
                }
            }



            //Test
            if (rowFromWeb.Count != listRow.Count)
            {
                Assert.Fail("sai so luong WO:" + rowFromWeb.Count.ToString() + "!=" + listRow.Count.ToString());
            }
            foreach (var item in rowFromWeb)
            {
                if (!listRow.Any(i => i.WO_CODE == item.WO_CODE && i.DESCRIPTIONS == item.DESCRIPTIONS))
                {
                    Assert.Fail("sai WO");
                }
            }
            System.Threading.Thread.Sleep(2000);
            driver.Close();
        }
        [TestMethod]
        public void Insert_WO()
        {
            //    Login();
            //    //Page
            //    IList<IWebElement> pages = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/p-table/div/p-paginator/div/span")).FindElements(By.TagName("a"));
            //    //Retrieve from DOM

            //    IWebElement table = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/p-table/div/div/table")).FindElement(By.TagName("tbody"));
            //    wait.Until(driver => table.FindElements(By.TagName("tr")));

            //    List<IWebElement> rows = table.FindElements(By.TagName("tr")).ToList();

            //    //random row
            //    int index = rows.Count / 2 + 1;
            //    IWebElement rowChoose = rows.ElementAt(index);

            //    //Get data row Web
            //    IList<IWebElement> fields = rowChoose.FindElements(By.TagName("td"));
            //    string id = fields.ElementAt(0).Text;
            //    string status = fields.ElementAt(4).Text;
            //    string Dep_name = fields.ElementAt(2).Text;

            //    //Get data from db
            //    CM_PHONGBAN_DTO fromDb = DataProvider.Instance.GetData<CM_PHONGBAN_DTO>("DEPARTMENT_Search", new
            //    {
            //        DEP_CODE = id,
            //        DEP_NAME = "",
            //        BRANCH_ID = "",
            //        RECORD_STATUS = ""
            //    }).FirstOrDefault();

            //    //check Pre-execute
            //    Assert.AreEqual(status, fromDb.RECORD_STATUS);

            //    IWebElement deleteButton = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/div/button[3]"));
            //    //deleteButton.Click();

            //    string dialog = "/html/body/div";
            //    //IWebElement confirmDiaLog = driver.FindElement(By.XPath(dialog));
            //    //if (!confirmDiaLog.Displayed)
            //    //{
            //    //    Assert.Fail("Loi UI hoac Khong Chon Duoc");
            //    //}
            //    //string title = confirmDiaLog.FindElement(By.ClassName("swal-title")).Text;
            //    //Assert.AreEqual("Không có dữ liệu để xóa", title);
            //    //confirmDiaLog.FindElement(By.XPath("/html/body/div/div/div[3]/div/button")).Click();

            //    //Execute
            //    rowChoose.Click();
            //    string classes = rowChoose.GetAttribute("class");
            //    if (!classes.Contains("ui-state-highlight"))
            //    {
            //        Assert.Fail("Loi UI hoac Khong Chon Duoc");
            //    }

            //    deleteButton = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/div/button[3]"));
            //    deleteButton.Click();

            //    IWebElement confirmDiaLog = driver.FindElement(By.XPath(dialog));
            //    if (!confirmDiaLog.Displayed)
            //    {
            //        Assert.Fail("Loi UI hoac Khong Chon Duoc");
            //    }

            //    IWebElement confirmButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div[4]/div[2]/button")));

            //    confirmButton.Click();

            //    //result
            //    Thread.Sleep(6000);
            //    IWebElement resultDialog = driver.FindElement(By.XPath(dialog));
            //    string title = resultDialog.FindElement(By.ClassName("swal-title")).Text;
            //    //Check
            //    if (status == "0")
            //    {
            //        Assert.AreEqual(Dep_name + " đã bị xóa", title);
            //        fromDb = DataProvider.Instance.GetData<CM_PHONGBAN_DTO>("DEPARTMENT_Search", new
            //        {
            //            DEP_CODE = id,
            //            DEP_NAME = "",
            //            BRANCH_ID = "",
            //            RECORD_STATUS = ""
            //        }).FirstOrDefault();
            //        Assert.AreEqual(fromDb.RECORD_STATUS, "0");
            //    }
            //    else
            //    {
            //        Assert.AreEqual("Xóa thành công", title);
            //        fromDb = DataProvider.Instance.GetData<CM_PHONGBAN_DTO>("DEPARTMENT_Search", new
            //        {
            //            DEP_CODE = id,
            //            DEP_NAME = "",
            //            BRANCH_ID = "",
            //            RECORD_STATUS = ""
            //        }).FirstOrDefault();
            //        Assert.AreEqual(fromDb.RECORD_STATUS, "0");
            //    }
            driver.Close();
        }
    }
}
