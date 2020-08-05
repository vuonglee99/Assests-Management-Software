using System;
using System.Collections.Generic;
using System.Text;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using Selenium.Test.group2.G2LOAI_XE_DTO;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Threading;

namespace Selenium.Test.group2.TEST_LOAIXE
{
    [TestClass]
    public class AddLoaiXe : Load
    {
        private string xpathMa = "/html/body/app-root/ng-component/div/div/div[2]/ng-component/html/body/div/div/div/div/form/div[2]/div/div/div[1]/div[1]/input";
        private string xpathTen = "/html/body/app-root/ng-component/div/div/div[2]/ng-component/html/body/div/div/div/div/form/div[2]/div/div/div[1]/div[2]/input";
        private string xpathHang = "/html/body/app-root/ng-component/div/div/div[2]/ng-component/html/body/div/div/div/div/form/div[2]/div/div/div[1]/div[3]/input";
        private string xpathMoTa = "/html/body/app-root/ng-component/div/div/div[2]/ng-component/html/body/div/div/div/div/form/div[2]/div/div/div[1]/div[4]/textarea";
        private string xpathNamSx = "/html/body/app-root/ng-component/div/div/div[2]/ng-component/html/body/div/div/div/div/form/div[2]/div/div/div[2]/div[1]/input";
        private string xpathNguyenLieu = "/html/body/app-root/ng-component/div/div/div[2]/ng-component/html/body/div/div/div/div/form/div[2]/div/div/div[2]/div[2]/input";
        private string xpathDinhMucNL = "/html/body/app-root/ng-component/div/div/div[2]/ng-component/html/body/div/div/div/div/form/div[2]/div/div/div[2]/div[3]/input";

        private string xpathBtnLuu = "/html/body/app-root/ng-component/div/div/div[2]/ng-component/html/body/div/div/div/div/form/ul/li[3]";



        public AddLoaiXe()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        [DataRow("SLTest001", "Xe tải Thaco Kia K250", "KIA", "Thùng Lửng 3.5m 2,4 tấn", 2020, "Dầu", 6)]
        public void Add_LoaiXe_CheckInputNormal(String ma, String ten, String hang, String mota, int namsx, String nguyenlieu, int dinhmucNL)
        {
            Login();
            driver.Navigate().GoToUrl(homeURL + "/app/admin/loai-xe-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);

            //DateTime dateTime = DateTime.Now;
            //string date = dateTime.Year + "" + dateTime.Month + "" + dateTime.Day + "" + dateTime.Hour + "" + dateTime.Minute + "" + dateTime.Second;
            //ten += " " + date;
            //mota += " " + date;

            var tbxMa = driver.FindElement(By.XPath(xpathMa));
            tbxMa.Clear();
            tbxMa.SendKeys(ma);

            var tbxName = driver.FindElement(By.XPath(xpathTen));
            tbxName.Clear();
            tbxName.SendKeys(ten);

            var tbxHang = driver.FindElement(By.XPath(xpathHang));
            tbxHang.Clear();
            tbxHang.SendKeys(hang);

            var tbxDescription = driver.FindElement(By.XPath(xpathMoTa));
            tbxDescription.Clear();
            tbxDescription.SendKeys(mota);

            var tbxNamSx = driver.FindElement(By.XPath(xpathNamSx));
            tbxNamSx.Clear();
            tbxNamSx.SendKeys(namsx.ToString());

            var tbxLoaiNL = driver.FindElement(By.XPath(xpathNguyenLieu));
            tbxLoaiNL.Clear();
            tbxLoaiNL.SendKeys(nguyenlieu);

            var tbxDinhMucNL = driver.FindElement(By.XPath(xpathDinhMucNL));
            tbxDinhMucNL.Clear();
            tbxDinhMucNL.SendKeys(dinhmucNL.ToString());


            Thread.Sleep(200);
            var btnLuu = driver.FindElement(By.XPath(xpathBtnLuu));
            btnLuu.Click();
            Thread.Sleep(500);
            //Check DB
            //LOAI_XE_DTO finalRow = DataProvider.Instance.GetData<LOAI_XE_DTO>("LoaiXe_GetAll", new {}).ToList().LastOrDefault();

            //Assert.AreEqual(ten, finalRow.LX_TEN);
            //Assert.AreEqual(mota, finalRow.LX_MO_TA);
        }



        [TestMethod]
        [DataRow("SLTest001", "Xe tải Thaco Kia K250", "KIA", "Thùng Lửng 3.5m 2,4 tấn", 2020, "Dầu", 6)]
        public void Add_LoaiXe_CheckInputSpecial(String ma, String ten, String hang, String mota, int namsx, String nguyenlieu, int dinhmucNL)
        {
            Login();
            driver.Navigate().GoToUrl(homeURL + "/app/admin/loai-xe-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);

            string specialChar = "&<>\"'`";
            var tbxName = driver.FindElement(By.XPath(xpathTen));
            var tbxDescription = driver.FindElement(By.XPath(xpathMoTa));
            var tbxHang = driver.FindElement(By.XPath(xpathHang));
            var tbxNamSx = driver.FindElement(By.XPath(xpathNamSx));
            var tbxLoaiNL = driver.FindElement(By.XPath(xpathNguyenLieu));
            var tbxDinhMucNL = driver.FindElement(By.XPath(xpathDinhMucNL));
            var tbxMa = driver.FindElement(By.XPath(xpathMa));

            foreach (char c in specialChar.ToCharArray())
            {
                string tenTmp = ten + c.ToString();
                tbxName.Clear();
                tbxName.SendKeys(tenTmp);
                Thread.Sleep(200);
                if (driver.FindElement(By.XPath(xpathBtnLuu)).GetAttribute("class") == "tool-bar-icon-normal")
                {
                    //Log("***Faild : Luu duoc ten chua ky tu dac biet " + tenTmp);
                    throw new Exception();
                }
                else
                {
                    //Log("PASS : Khong luu duoc ten chua ky tu dac biet " + tenTmp);
                }
                tbxName.Clear();
                tbxName.SendKeys(ten);

                //Mo ta
                Thread.Sleep(200);
                string moTaTmp = mota + c.ToString();
                tbxDescription.Clear();
                tbxDescription.SendKeys(moTaTmp);
                Thread.Sleep(200);
                if (driver.FindElement(By.XPath(xpathBtnLuu)).GetAttribute("class") == "tool-bar-icon-normal")
                {
                    //Log("***Faild : Luu duoc mo ta chua ky tu dac biet " + moTaTmp);
                    throw new Exception();
                }
                else
                {
                    //Log("PASS : Khong luu duoc mo ta chua ky tu dac biet " + moTaTmp);
                }
                tbxDescription.Clear();
                tbxDescription.SendKeys(mota);

                //Ma
                Thread.Sleep(200);
                string maTmp = ma + c.ToString();
                tbxMa.Clear();
                tbxMa.SendKeys(maTmp);
                Thread.Sleep(200);
                if (driver.FindElement(By.XPath(xpathBtnLuu)).GetAttribute("class") == "tool-bar-icon-normal")
                {
                    //Log("***Faild : Luu duoc mo ta chua ky tu dac biet " + moTaTmp);
                    throw new Exception();
                }
                else
                {
                    //Log("PASS : Khong luu duoc mo ta chua ky tu dac biet " + moTaTmp);
                }
                tbxMa.Clear();
                tbxMa.SendKeys(ma);

                //hang
                Thread.Sleep(200);
                string hangTmp = hang + c.ToString();
                tbxHang.Clear();
                tbxHang.SendKeys(hangTmp);
                Thread.Sleep(200);
                if (driver.FindElement(By.XPath(xpathBtnLuu)).GetAttribute("class") == "tool-bar-icon-normal")
                {
                    //Log("***Faild : Luu duoc mo ta chua ky tu dac biet " + moTaTmp);
                    throw new Exception();
                }
                else
                {
                    //Log("PASS : Khong luu duoc mo ta chua ky tu dac biet " + moTaTmp);
                }
                tbxHang.Clear();
                tbxHang.SendKeys(hang);

                //loai nl
                Thread.Sleep(200);
                string loaiNL = hang + c.ToString();
                tbxLoaiNL.Clear();
                tbxLoaiNL.SendKeys(hangTmp);
                Thread.Sleep(200);
                if (driver.FindElement(By.XPath(xpathBtnLuu)).GetAttribute("class") == "tool-bar-icon-normal")
                {
                    //Log("***Faild : Luu duoc mo ta chua ky tu dac biet " + moTaTmp);
                    throw new Exception();
                }
                else
                {
                    //Log("PASS : Khong luu duoc mo ta chua ky tu dac biet " + moTaTmp);
                }
                tbxLoaiNL.Clear();
                tbxLoaiNL.SendKeys(hang);

            }




            //RONG---------------
            driver.FindElement(By.XPath(xpathTen)).Clear();
            driver.FindElement(By.XPath(xpathTen)).SendKeys("a");
            driver.FindElement(By.XPath(xpathTen)).SendKeys(Keys.Backspace);
            Thread.Sleep(100);
            if (driver.FindElement(By.XPath(xpathBtnLuu)).GetAttribute("class") == "tool-bar-icon-normal")
            {
                //Log("***Faild : Luu duoc ten rong");
                throw new Exception();
            }
            else
            {
                //Log("PASS : Khong luu duoc ten rong");
            }
            driver.FindElement(By.XPath(xpathTen)).SendKeys(ten);

            tbxMa.Clear();
            tbxMa.SendKeys("a");
            tbxMa.SendKeys(Keys.Backspace);
            Thread.Sleep(100);
            if (driver.FindElement(By.XPath(xpathBtnLuu)).GetAttribute("class") == "tool-bar-icon-normal")
            {
                throw new Exception();
                //Log("*** G2_002_4: Luu duoc mo ta rong");
            }
            else
            {
                //Log("PASS : Khong luu duoc mo ta rong");
            }
            tbxMa.SendKeys(ma);


            driver.FindElement(By.XPath(xpathTen)).Clear();
            string ten101KyTu = "101 ký tự 101 ký tự 101 ký tự 101 ký tự 101 ký tự 101 ký tự 101 ký tự 101 ký tự 101 ký tự 101 ký tự 1";
            driver.FindElement(By.XPath(xpathTen)).SendKeys(ten101KyTu);
            Thread.Sleep(100);
            if (driver.FindElement(By.XPath(xpathBtnLuu)).GetAttribute("class") == "tool-bar-icon-normal")
            {
                //Log("***Faild : Luu duoc ten qua 100 ky tu");
                throw new Exception();
            }
            else
            {
                //Log("PASS : Khong luu duoc ten qua 100 ky tu");
            }
            driver.FindElement(By.XPath(xpathTen)).Clear();
            driver.FindElement(By.XPath(xpathTen)).SendKeys(ten);


            tbxDescription.Clear();
            string mota1001KyTu = "1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự 1001 ký tự1";
            tbxDescription.SendKeys(mota1001KyTu);
            Thread.Sleep(200);
            if (driver.FindElement(By.XPath(xpathBtnLuu)).GetAttribute("class") == "tool-bar-icon-normal")
            {
                //Log("***Faild : Luu duoc mo ta hon 1000 ky tu");
                throw new Exception();
            }
            else
            {
                //Log("PASS : Khong luu duoc mo ta hon 1000 ky tu");
            }
            tbxDescription.Clear();
            tbxDescription.SendKeys(mota);


        }

    }
}

