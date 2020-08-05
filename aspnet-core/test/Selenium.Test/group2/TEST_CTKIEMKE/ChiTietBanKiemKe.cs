using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Selenium.Test.group2.TEST_CTKIEMKE
{
    [TestClass]
    public class ChiTietBanKiemKe : Load
    {

        public ChiTietBanKiemKe()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";
        }

        [TestMethod]
        public void CheckGiaoDienChung()
        {
            String listErr = "";
            int numErr = 0;
            Login();
            driver.Navigate().GoToUrl(homeURL + "/app/admin/kiemke-mobile/kiemke1");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(5000);

            var lbTieuDe = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-kiemke-mobile/div[1]/div[1]/h1"));
            if (lbTieuDe.Text != "Chi tiết kiểm kê" && lbTieuDe.Text != "Inventory detail")
            {
                listErr += "Sai tieu de!" + "/n";
                numErr++;
            }

            if (lbTieuDe.GetCssValue("color") != "rgba(255, 255, 255, 1)")
            {
                listErr += "Mau cua tieu de khong phai #FFFFFF" + "/n";
                numErr++;
            }

            //Lay font chu label
            for (int i = 1; i <= 4; i++)
            {
                ///html/body/app-root/ng-component/div/div/div[2]/app-kiemke-mobile/div[1]/div[2]/div/div/div[2]/div[3]
                ///html/body/app-root/ng-component/div/div/div[2]/app-kiemke-mobile/div[1]/div[2]/div/div/div[1]/div[1]
                ///html/body/app-root/ng-component/div/div/div[2]/app-kiemke-mobile/div[1]/div[2]/div/div/div[1]/div[2]
                var lb = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-kiemke-mobile/div[1]/div[2]/div/div/div[1]/div[" + i + "]"));
                if (lb.GetCssValue("font-family") != "Roboto")
                {
                    var value = lb.GetCssValue("font-family");
                    listErr += "Font chu cac label thong tin kiem ke khong dung: " + i + " : " + value + "/n";
                    numErr++;
                    break;
                }
            }
            for (int i = 1; i <= 3; i++)
            {

                var lb = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-kiemke-mobile/div[1]/div[2]/div/div/div[2]/div[" + i + "]"));
                if (lb.GetCssValue("font-family") != "Roboto")
                {
                    var value = lb.GetCssValue("font-family");
                    listErr += "Font chu cac label thong tin kiem ke khong dung: " + i + " : " + value + "/n";
                    numErr++;
                    break;
                }
            }

            //Font chu va mau chu cho thanh tim kiem
            for (int i = 1; i <= 4; i++)
            {

                var lb = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-kiemke-mobile/div[3]/div/div/form/div/div/div/div/div/b[" + i + "]"));
                if (lb.GetCssValue("font-family") != "Roboto")
                {
                    var value = lb.GetCssValue("font-family");
                    listErr += "Font chu cac label loc kiem ke khong dung: " + i + " : " + value + "/n";
                    numErr++;
                    break;
                }
                if (lb.GetCssValue("color") != "rgba(87, 93, 98, 1)")
                {
                    var value = lb.GetCssValue("color");
                    listErr += "Mau chu cac label loc kiem ke khong dung: " + i + " : " + value + "/n";
                    numErr++;
                    break;
                }
            }

            ////Font va mau chu cho button
            //var btnTimKiem = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-kiemke-mobile/html/body/div/div[2]/div[1]/button"));
            //if (btnTimKiem.GetCssValue("font-family") != "Roboto" || btnTimKiem.GetCssValue("color") != "rgba(255, 255, 255, 1)")
            //{
            //    listErr += "Button tim kiem co mau va font chu khong phu hop: " + btnTimKiem.GetCssValue("font-family") + " , " + btnTimKiem.GetCssValue("color") + "/n";
            //    numErr++;
            //}

            //Kiem tra bang Danh sách thiết bị kiểm kê
            for (int i = 1; i <= 7; i++)
            {
                var lb = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-kiemke-mobile/div[3]/div/div/div/div/div/p-table/div/div/table/thead/tr/th[" + i + "]"));
                if (lb.GetCssValue("font-family") != "Roboto" && lb.GetCssValue("font-family") != "Poppins")
                {
                    listErr += "Font chu bang DS thiet bi kiem ke khong dung: " + lb.GetCssValue("font-family") + "/n";
                    numErr++;
                    break;
                }
            }

            if (numErr > 0)
            {
                throw new Exception("Co " + numErr + "ERR: " + listErr);
            }
        }


        [TestMethod]
        public void TestFilter()
        {
            String listErr = "";
            int numErr = 0;
            Login();
            driver.Navigate().GoToUrl(homeURL + "/app/admin/kiemke-mobile/kiemke1");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(5000);


            var inputTen = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-kiemke-mobile/div[3]/div/div/form/div/div/div/div/div/input[2]"));
            inputTen.SendKeys("LG");

            var btnTimKiem = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-kiemke-mobile/div[3]/div/div/form/div/div/div/div/div/span[5]/button"));
            btnTimKiem.Click();
            Thread.Sleep(200);

            //var elementTenTB = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-kiemke-mobile/div[3]/div/div/div/div/div/p-table/div/div/table/tbody/tr[1]/td[3]"));
            //if (!elementTenTB.Text.Contains("LG"))
            //{
            //    listErr += "Tim kiem ten thiet bi LG that bai! "+"\n";
            //    numErr++;

            //}

            if (numErr > 0)
            {
                throw new Exception("Co " + numErr + "ERR: " + listErr);
            }
        }
    }
}
