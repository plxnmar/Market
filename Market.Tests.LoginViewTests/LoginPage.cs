using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Tests.LoginViewTests
{
	public class LoginPage
	{
        private const string URI = "https://localhost:7138/Account/Login";
        private IWebElement NameElement => _driver.FindElement(By.Id("Name"));
        private IWebElement AgeElement => _driver.FindElement(By.Id("Age"));
    }
}
