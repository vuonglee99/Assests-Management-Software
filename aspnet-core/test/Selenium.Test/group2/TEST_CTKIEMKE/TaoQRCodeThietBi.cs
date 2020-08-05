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
    public class TaoQRCodeThietBi : Load
    {
        public TaoQRCodeThietBi()
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
            driver.Navigate().GoToUrl(homeURL + "/app/admin/tao-qrcode");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(5000);

            var lbTieuDe = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-tao-qrcode/div[1]/h1"));
            if (lbTieuDe.Text != "List of equipment" && lbTieuDe.Text != "Danh sách thiết bị")
            {
                listErr += "Sai tieu de!" + "/n";
                numErr++;
            }

            //label tim kiem
            for (int i = 1; i <= 4; i++)
            {
                var lb = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-tao-qrcode/div[2]/div/div/form/div/div/div/div/div/b[" + i + "]"));
                if (lb.GetCssValue("font-family") != "Roboto" || lb.GetCssValue("color") != "rgba(87, 93, 98, 1)")
                {
                    listErr += "Label hanh tim kiem co mau va font chu khong phu hop: " + lb.GetCssValue("font-family") + " , " + lb.GetCssValue("color") + "\n";
                    numErr++;
                    break;
                }
            }

            //button tim kiem
            //var btnTimKiem = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-tao-qrcode/html/body/div/div[1]/div[1]/button[2]"));
            //if(/*!btnTimKiem.GetAttribute("font-family").Contains("Roboto") || */btnTimKiem.GetCssValue("color") != "rgba(255, 255, 255, 1)")
            //{
            //    listErr+= "Button tim kiem co mau va font chu khong phu hop: " + btnTimKiem.GetCssValue("font-family") + " , " + btnTimKiem.GetCssValue("color") + "\n";
            //    numErr++;
            //}

            //button taoqr code
            var btnTaoQR = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-tao-qrcode/div[2]/div/div/div/div/div/div[2]/button[1]"));
            if (/*btnTaoQR.GetAttribute("font-family") != "Roboto" ||*/ btnTaoQR.GetCssValue("color") != "rgba(255, 255, 255, 1)")
            {
                listErr += "Button taoQR co mau va font chu khong phu hop: " + btnTaoQR.GetCssValue("font-family") + " , " + btnTaoQR.GetCssValue("color") + "\n";
                numErr++;
            }

            ////button in
            //var btnIn = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-tao-qrcode/html/body/div/div[3]/button"));
            //if (btnIn.GetAttribute("font-family") != "Roboto" || btnIn.GetCssValue("color") != "rgba(255, 255, 255, 1)")
            //{
            //    listErr += "Button In co mau va font chu khong phu hop: " + btnIn.GetCssValue("font-family") + " , " + btnIn.GetCssValue("color") + "\n";
            //    numErr++;
            //}

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
            driver.Navigate().GoToUrl(homeURL + "/app/admin/tao-qrcode");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(5000);



            var inputTen = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-tao-qrcode/div[2]/div/div/form/div/div/div/div/div/input[2]"));
            inputTen.SendKeys("LG");

            var btnTimKiem = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-tao-qrcode/div[2]/div/div/form/div/div/div/div/div/span[5]/button"));
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



        [TestMethod]
        public void TestTaoQR()
        {
            String listErr = "";
            int numErr = 0;
            Login();
            driver.Navigate().GoToUrl(homeURL + "/app/admin/tao-qrcode");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(5000);


            var maTB = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-tao-qrcode/div[2]/div/div/div/div/div/p-table/div/div/table/tbody/tr[1]/td[2]"));
            var tenTB = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-tao-qrcode/div[2]/div/div/div/div/div/p-table/div/div/table/tbody/tr[1]/td[3]"));
            var donViQL = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-tao-qrcode/div[2]/div/div/div/div/div/p-table/div/div/table/tbody/tr[1]/td[5]"));

            var btnTaoQR = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/app-tao-qrcode/div[2]/div/div/div/div/div/p-table/div/div/table/tbody/tr[1]/td[6]/div/input"));
            btnTaoQR.Click();
            Thread.Sleep(1000);

            var qrTenTB = driver.FindElement(By.XPath("//*[@id=\"printableAreaAll\"]/div/div/div/div/div[1]/li/div/div/p[1]"));
            var qrMaTB = driver.FindElement(By.XPath("//*[@id=\"printableAreaAll\"]/div/div/div/div/div[1]/li/div/div/p[2]"));
            var qrTenDV = driver.FindElement(By.XPath("//*[@id=\"printableAreaAll\"]/div/div/div/div/div[1]/li/div/div/p[3]"));
            if (!qrTenTB.Text.Contains(tenTB.Text) || !qrMaTB.Text.Contains(maTB.Text) || !qrTenDV.Text.Contains(donViQL.Text))
            {
                listErr += "Tao QR Code thiet bi that bai! " + "\n";
                numErr++;
            }

            if (numErr > 0)
            {
                throw new Exception("Co " + numErr + "ERR: " + listErr);
            }
        }
    }
}
