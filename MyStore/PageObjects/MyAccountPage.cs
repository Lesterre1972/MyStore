using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace MyStore
{
    internal class MyAccountPage
    {
        private readonly IWebDriver driver;

        public MyAccountPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public MyAccountPage()
        {
        }

        [FindsBy(How = How.ClassName, Using = "logout")]
        public IWebElement LogOut { get; set; }

        public object Assert { get; internal set; }

        internal void IsTrue(bool v)
        {
            throw new NotImplementedException();
        }
    }
}