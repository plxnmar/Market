using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Tests
{
    public class HomePage
    {
        private readonly IWebDriver _driver;

        private const string URI = "https://localhost:7138/";

        public string Title => _driver.Title;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate() => _driver.Navigate().GoToUrl(URI);      

    }
}
