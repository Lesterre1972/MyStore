using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MyStore
{
    [TestFixture]
    internal class Program
    {
        [SetUp]
        public void BeforeTest()
        {
        }

        [TearDown]
        public void CleanUp()
        {
            //Close open window
            driver.Close();
        }

        private readonly IWebDriver driver = new ChromeDriver();

        private static void Main(string[] args)
        {
        }

        [OneTimeSetUp]
        public void Initialize()
        {
            driver.Navigate()
                .GoToUrl(
                    "http://automationpractice.com/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        [Test]
        public void MyStore()
        {
            //Initialize the home page by calling its reference
            var homePage = new HomePage(driver);
            homePage.SignIn.Click();

            //Initialize the login page by calling its reference
            var pageLogin = new LoginPage(driver);

            //I created a method in 1the Login Page Object that automatically verified text, enters email, and clicks the log in button
            pageLogin.Login("mystor3@mystore.com");

            //Initialize the register page by calling its reference
            var registerPage = new RegisterPage(driver);

            //Enter text in all required fields
            registerPage.Title.Click();
            registerPage.CustomerFirstName.SendKeys("Jon");
            registerPage.CustomerLastName.SendKeys("Doe");
            registerPage.CustomerEmail.Clear();
            registerPage.CustomerEmail.SendKeys("mystor3@mystore.com");
            registerPage.Password.SendKeys("12345");
            registerPage.Dateofbirth.Click();
            registerPage.Monthofbirth.Click();
            registerPage.Yearofbirth.Click();
            registerPage.FirstName.Clear();
            registerPage.FirstName.SendKeys("Jon");
            registerPage.LastName.Clear();
            registerPage.LastName.SendKeys("Doe");
            registerPage.CompanyName.SendKeys("ABC Company");
            registerPage.Address.SendKeys("1 Main St.");
            registerPage.City.SendKeys("My Town");
            registerPage.State.Click();
            registerPage.ZipCode.SendKeys("10001");
            registerPage.Country.SendKeys("USA");
            registerPage.MobilePhone.SendKeys("3211234567");
            registerPage.EmailAlias.Clear();
            registerPage.EmailAlias.SendKeys("Winter");
            registerPage.RegisterBtn.Click();

            //Initialize the my account page by calling its reference
            var myAccountPage = new MyAccountPage(driver);

            //Verify text on my account page
            Assert.IsTrue(driver.PageSource.Contains("Welcome to your account"));

            //Click the log out
            myAccountPage.LogOut.Click();
        }
    }
}