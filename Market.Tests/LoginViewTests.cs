using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Market.Tests;
using System;

namespace Market.Tests
{
    public class LoginViewTests
    {
        private IWebDriver _driver;
        private LoginPage loginPage;
        private HomePage homePage;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            loginPage = new LoginPage(_driver);
            homePage = new HomePage(_driver);
            loginPage.Navigate();
        }

        [Test]
        public void CreateTitleTest()
        {
            Assert.That("¬ход - Market", Is.EqualTo(loginPage.Title));
        }


        [Test]
        public void CreateDataTest()
        {
            loginPage.PopulateName("user123");
            loginPage.PopulatePassword("111");
            loginPage.ClickCreate();

            Assert.That("Home - Market", Is.EqualTo(homePage.Title));
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}