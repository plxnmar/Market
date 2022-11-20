using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Tests
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        private const string URI = "https://localhost:7138/Account/Login";

        //type="input"
        private IWebElement NameElement => _driver.FindElement(By.Id("name"));

        //span
        private IWebElement NameValidElement => _driver.FindElement(By.Id("nameValid"));

        //type="input"
        private IWebElement PasswordElement => _driver.FindElement(By.Id("password"));

        //span
        private IWebElement PasswordValidElement => _driver.FindElement(By.Id("passwordValid"));

        //div 
        private IWebElement ModelValidElement => _driver.FindElement(By.Id("modelValid"));

        //type="submit"
        private IWebElement LoginElement => _driver.FindElement(By.Id("login"));

        public string Title => _driver.Title;
        public string ValidName => NameValidElement.Text;
        public string ValidPassword => PasswordValidElement.Text;
        public string ValidModel => ModelValidElement.Text;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate() => _driver.Navigate().GoToUrl(URI);
        public void PopulateName(string name) => NameElement.SendKeys(name);
        public void PopulatePassword(string password) => PasswordElement.SendKeys(password);
        public void ClickCreate() => LoginElement.Click();

    }
}
