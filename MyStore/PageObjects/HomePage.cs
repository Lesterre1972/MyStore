using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace MyStore
{
    internal class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public HomePage()
        {
        }

        [FindsBy(How = How.ClassName, Using = "login")]
        public IWebElement SignIn { get; set; }
    }
}