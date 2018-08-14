using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace MyStore
{
    internal class RegisterPage
    {
        private IWebDriver driver;

        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        
        public RegisterPage()
        {
        }

        [FindsBy(How = How.Id, Using = "id_gender1")]
        public IWebElement Title { get; set; }

        [FindsBy(How = How.Id, Using = "customer_firstname")]
        public IWebElement CustomerFirstName { get; set; }

        [FindsBy(How = How.Id, Using = "customer_lastname")]
        public IWebElement CustomerLastName { get; set; }

        [FindsBy(How = How.Id, Using = "email")]
        public IWebElement CustomerEmail { get; set; }

        [FindsBy(How = How.Id, Using = "passwd")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#days > option:nth-child(2)")]
        public IWebElement Dateofbirth { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#months > option:nth-child(5)")]
        public IWebElement Monthofbirth { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#years > option:nth-child(47)")]
        public IWebElement Yearofbirth { get; set; }

        [FindsBy(How = How.Id, Using = "firstname")]
        public IWebElement FirstName { get; set; }

        [FindsBy(How = How.Id, Using = "lastname")]
        public IWebElement LastName { get; set; }

        [FindsBy(How = How.Id, Using = "company")]
        public IWebElement CompanyName { get; set; }

        [FindsBy(How = How.Id, Using = "address1")]
        public IWebElement Address { get; set; }

        [FindsBy(How = How.Id, Using = "city")]
        public IWebElement City { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#id_state > option:nth-child(12)")]
        public IWebElement State { get; set; }

        [FindsBy(How = How.Id, Using = "postcode")]
        public IWebElement ZipCode { get; set; }

        [FindsBy(How = How.Id, Using = "id_country")]
        public IWebElement Country { get; set; }

        [FindsBy(How = How.Id, Using = "phone_mobile")]
        public IWebElement MobilePhone { get; set; }

        [FindsBy(How = How.Id, Using = "alias")]
        public IWebElement EmailAlias { get; set; }

        [FindsBy(How = How.Id, Using = "submitAccount")]
        public IWebElement RegisterBtn { get; set; }

    }
}