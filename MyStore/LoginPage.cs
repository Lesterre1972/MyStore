using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace MyStore
{
    internal class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public LoginPage()
        {
        }

        [FindsBy(How = How.Id, Using = "email_create")]
        public IWebElement Email { get; set; }

        public object SubmitCreate { get; internal set; }

        [FindsBy(How = How.Id, Using = "SubmitCreate")]
        public IWebElement Submitbtn { get; set; }


        public RegisterPage Login(string Email)
        {
            //Verify text on page
            Assert.IsTrue(driver.PageSource.Contains("create an account"));

            //Enter email
            this.Email.SendKeys(Email);

            //Click button
            Submitbtn.Click();

            //Clicking the buttton will return the Registration Page and initialize its objects
            return new RegisterPage();
        }
    }
}