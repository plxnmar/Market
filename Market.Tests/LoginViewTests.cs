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
        public void TitleTest()
        {
            Assert.That("Вход - Market", Is.EqualTo(loginPage.Title));
        }


        [Test]
        public void CorrectDataTest()
        {
            loginPage.PopulateName("user123");
            loginPage.PopulatePassword("111");
            loginPage.ClickCreate();

            Assert.That("Home - Market", Is.EqualTo(homePage.Title));
        }

        [Test]
        public void EmptyDataTest()
        {
            loginPage.ClickCreate();

            if (loginPage.ValidName.Length == 0)
                throw new Exception("Сообщение о неверном имени не показалось");

            if (loginPage.ValidPassword.Length == 0)
                throw new Exception("Сообщение о неверном пароле не показалось");
        }

        [Test]
        public void InvalidDataTest()
        {
            loginPage.PopulateName("111");
            loginPage.PopulatePassword("111");

            loginPage.ClickCreate();

            if (loginPage.ValidModel.Length == 0)
                throw new Exception("Сообщение о неверном пользователе не показалось");

        }


        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}