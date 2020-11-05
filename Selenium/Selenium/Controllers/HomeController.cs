using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium.Models;
using System.Timers;
using OpenQA.Selenium;
using System.Drawing;
using System.IO;
using OpenQA.Selenium.Support.Events;

namespace Selenium.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GoSelenium(string TaiKhoan,string MatKhau)
        {
            ChromeDriver driver = new ChromeDriver();
            try
            {
                driver.Url = "https://katalon-demo-cura.herokuapp.com";
                driver.Navigate();

                var ButtonGo = driver.FindElementById("btn-make-appointment");
                ButtonGo.Click();

                var Username = driver.FindElementById("txt-username");
                Username.SendKeys(TaiKhoan);

                var Password = driver.FindElementById("txt-password");
                Password.SendKeys(MatKhau);

                var LoginButton = driver.FindElementById("btn-login");
                LoginButton.Click();
                try
                {
                    bool Checker = driver.FindElement(By.Id("btn-book-appointment")).Displayed;
                    string TextReturn = driver.FindElement(By.Id("btn-book-appointment")).Text;
                    if (Checker == false)
                    {
                        ViewBag.Data = "btn-book-appointment is not Displayed";
                    }
                    else
                    {
                        ViewBag.Data = TextReturn;
                    }
  
                }
                catch
                {
                    ViewBag.Data = "btn-book-appointment is not Exist!";
                }
            }
            catch
            {
                ViewBag.Data = "Something went wrong!";
            }
            finally
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                String destinationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Hello.png");
                ss.SaveAsFile(destinationPath, ScreenshotImageFormat.Png);
                driver.Close();
            }
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
