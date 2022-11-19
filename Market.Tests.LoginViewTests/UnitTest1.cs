using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Market.Tests.LoginViewTests
{
    public class UnitTest1 : IDisposable
    {
        private readonly IWebDriver _driver;
       

        public UnitTest1()
        {
            _driver = new ChromeDriver();
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Fact]
        public void TitleShouldStartTest()
        {
            _driver.Navigate().GoToUrl(URI);
            Assert.Equal("¬ход - Market", _driver.Title);
        }

        [Fact]
        public void CreateDataTest()
        {
            _driver.Navigate().GoToUrl(URI);
            Assert.Equal("¬ход - Market", _driver.Title);
        }


    }
}