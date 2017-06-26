using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace TstAPP
{
    public enum BrowserType
    {
        Chrome,
        IE,
        Firefox
    }

    [TestFixture]
    public class Hooks : BaseTestClass
    {
        [SetUp]
        public void InitializeTest()
        {
            ChooseDriverInstance(_browserType);
        }

        [TearDown]
        public void CleanUp()
        {
            //Close open windows after test is completed
            Driver.Close();
        }

        private readonly BrowserType _browserType;

        public Hooks(BrowserType browser)
        {
            _browserType = browser;
        }

        private void ChooseDriverInstance(BrowserType browserType)

        {
            if (browserType == BrowserType.Chrome)
                Driver = new ChromeDriver();
            else if (browserType == BrowserType.Firefox)
            {
                Driver = new FirefoxDriver();
            }

            else if (browserType == BrowserType.IE)
                Driver = new InternetExplorerDriver();
        }

    }

}