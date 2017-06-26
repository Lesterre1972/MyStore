using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

using System.Threading;
using System.Configuration;
using System.IO;
using System.Linq;

namespace WhatcanidoSample
{
    [TestFixture]
    [Parallelizable]
    public class FirefoxTesting : Hooks
    {
        public FirefoxTesting() : base(BrowserType.Firefox)
        {
        }

        [Test]
        public void FirefoxLoginAndUpdateProfileTest()
        {
            Driver.Navigate().GoToUrl("google.com");
            // Login link
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(1)>a"))
                .Click();
            WaitUntilEmailElementVisible();
            Driver.FindElement(By.Id("email")).SendKeys("nora2.smith2@troyweb.com");
            Driver.FindElement(By.Id("password")).SendKeys("TrackerTest1");
            // Click Login button 
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
            WaitForDropdownToggleElement();
            // Click on user name in upper right of page
            Driver.FindElement(By.ClassName("dropdown-toggle")).Click();
            // Click on update profile toggle
            Driver.FindElement(
                By.CssSelector("#app-navbar-collapse > ul.nav.navbar-nav.navbar-right > li > ul > li:nth-child(1) > a"))
                .Click();
            WaitUntilStatusElementVisible();
            // Change status to contractor
            Driver.FindElement(By.CssSelector("#edit-profile > div:nth-child(7) > div > select > option:nth-child(3)"))
                .Click();
            Thread.Sleep(1000);
            // Add Skill SASS
            Driver.FindElement(
                By.CssSelector("#skill-list > div:nth-child(1) > div:nth-child(2) > select > option:nth-child(25)"))
                .Click();
            // Mark Skill as Novice
            Driver.FindElement(
                By.CssSelector("# skill-list > div:nth-child(1) > div:nth-child(3) > select > option:nth-child(4)"))
                .Click();
            Thread.Sleep(1000);
            // Add Skill Git
            Driver.FindElement(
                By.CssSelector("#skill-list > div:nth-child(2) > div:nth-child(2) > select > option:nth-child(13)"))
                .Click();
            Thread.Sleep(1000);
            // Mark skill as expert
            Driver.FindElement(
                By.CssSelector("# skill-list > div:nth-child(2) > div:nth-child(3) > select > option:nth-child(2)"))
                .Click();
            WaitForElement();
            // Click the update button to save skills to the database
            Driver.FindElement(By.XPath("//*[@id='edit-profile']/button[3]")).Click();
            Thread.Sleep(1000);
            // Log user out
            Driver.FindElement(
                By.CssSelector("#app-navbar-collapse > ul.nav.navbar-nav.navbar-right > li > ul > li:nth-child(2) > a"))
                .Click();
        }

        private void WaitUntilStatusElementVisible()
        {
            throw new NotImplementedException();
        }

        private static void WaitForElement()
        {
            Thread.Sleep(1000);
        }

        private void WaitForDropdownToggleElement()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void FirefoxLoginWithWrongCredentials()
        {
            Driver.Navigate().GoToUrl("google.com");
            // Login link
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(1)>a"))
                .Click();
            WaitUntilEmailElementVisible();
            Driver.FindElement(By.Id("email")).SendKeys("nora1.smith1@troyweb.com");
            Driver.FindElement(By.Id("password")).SendKeys("Tracker3Test");
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
            Thread.Sleep(2000);
            // Verify statement appears after an existing user tries to login with the wrong credentials, message is same for wrong username or password
            Assert.That(Driver.PageSource.Contains("These credentials do not match our records."), Is.EqualTo(true),
                "this string of text does not exists");
        }

        private void WaitUntilEmailElementVisible()
        {
        }

        [Test]
        public void FirefoxWhatcanidoRegisterTest()
        {
            Driver.Navigate().GoToUrl("google.com");
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(2)>a"))
                .Click();
            WaitUntilNameElementVisible();
        }

        private List<IWebElement> GetTextFields(IWebDriver driver)
        {
            var textFields = new List<IWebElement>();

            try
            {
                var elements = driver.FindElements(By.CssSelector("form-control"));
                var tempList = new List<IWebElement>(elements);
                textFields.AddRange(tempList);
            }
            catch
            {
                // throw exception or log exception
            }

            try
            {
                textFields.AddRange(driver.FindElements(By.TagName("textarea")).ToList());
            }
            catch
            {
                // throw exception or log exception
            }

            textFields = textFields.Where(i => !i.Displayed).ToList(); // removes all hidden fields

            return textFields;


            Driver.FindElement(By.Id("first_name")).SendKeys("Johnny");
            Driver.FindElement(By.Id("last_name")).SendKeys("Test");
            Driver.FindElement(By.Id("middle")).SendKeys("D");
            Driver.FindElement(By.Id("email")).SendKeys("nora2.smith2@troyweb.com");
            Driver.FindElement(By.Id("password")).SendKeys("TrackerTest1");
            Driver.FindElement(By.Id("password-confirm")).SendKeys("TrackerTest1");
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
        }

        [Test]
        public void FirefoxUserViaAdmin()
        {
            Driver.Navigate().GoToUrl("google.com");
            // Login link
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(1)>a"))
                .Click();
            Thread.Sleep(2000);
            Driver.FindElement(By.CssSelector("#email")).SendKeys("theadmin@example.com");
            Driver.FindElement(By.CssSelector("#password")).SendKeys("password1");
            // Click the Login button
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();


            WaitForAdminToggleElement();
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li>a"));

            Thread.Sleep(2000);
            //Logs the user out
            Driver.FindElement(By.CssSelector(".dropdown-menu > li:nth-child(2) > a:nth-child(1)"));
        }

        private void WaitForAdminToggleElement()
        {
        }

        //// Click update profile toggle
        //Driver.FindElement(By.XPath("//*[@id='app-navbar-collapse']/ul[2]/li/ul/li[2]/a")).Click();
        private void WaitUntilNameElementVisible()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            wait.Until<IWebElement>((d) =>
            {
                IWebElement element = d.FindElement(By.Id("first_name"));
                if (element.Displayed &&
                    element.Enabled)
                {
                    return element;
                }

                return null;
            });
        }

        [Test]
        public void FirefoxRegisterExistingUserTest()
        {
            Driver.Navigate().GoToUrl("google.com");
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(2)>a"))
                .Click();
            WaitUntilNameElementVisible();
            Driver.FindElement(By.Id("first_name")).SendKeys("Johnny");
            Driver.FindElement(By.Id("last_name")).SendKeys("Test");
            Driver.FindElement(By.Id("middle")).SendKeys("D");
            Driver.FindElement(By.Id("email")).SendKeys("nora2.smith2@troyweb.com");
            Driver.FindElement(By.Id("password")).SendKeys("TrackerTest1");
            Driver.FindElement(By.Id("password-confirm")).SendKeys("TrackerTest1");
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
            Thread.Sleep(1000);

            // Verify statement appears after an existing user tries to register again
            Assert.That(Driver.PageSource.Contains(" that email already exists"), Is.EqualTo(true),
                "A user with that email already exists");
        }
    }

    [TestFixture]
    //  [Parallelizable]
    public class ChromeTesting : Hooks
    {
        public ChromeTesting() : base(BrowserType.Chrome)
        {
        }

        private object DriverWait(IWebDriver driver, int v)
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ChromeLoginAndUpdateProfileTest()
        {
            Driver.Navigate().GoToUrl("google.com");
            // Login link
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(1)>a"))
                .Click();
            WaitUntilNameElementVisible();
            Driver.FindElement(By.Id("email")).SendKeys("nora1.smith1@troyweb.com");
            Driver.FindElement(By.Id("password")).SendKeys("TrackerTest1");
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
            WaitForDropdownToggleElement();
            // Click on user name in upper right of page
            Driver.FindElement(By.ClassName("dropdown-toggle")).Click();
            // Click on update profile toggle
            Driver.FindElement(
                By.CssSelector("#app-navbar-collapse > ul.nav.navbar-nav.navbar-right > li > ul > li:nth-child(1) > a"))
                .Click();
            WaitUntilStatusElementVisible();
            // Change status to part time
            Driver.FindElement(By.CssSelector("#edit-profile > div:nth-child(7) > div > select > option:nth-child(4)"))
                .Click();
            // Mark skill JavaScript
            Driver.FindElement(By.CssSelector("#skill-list > div > div:nth-child(2) > select > option:nth-child(18)"))
                .Click();
            // Add Intermediate skill
            Driver.FindElement(By.CssSelector("#skill-list > div > div:nth-child(3) > select > option:nth-child(3)"))
                .Click();
            Thread.Sleep(1000);
            // Mark Intermediate skill
            Driver.FindElement(By.CssSelector("#add")).Click();
            //Mark another skill JQuery
            Driver.FindElement(
                By.CssSelector("#skill-list > div:nth-child(2) > div:nth-child(2) > select > option:nth-child(20)"))
                .Click();
            // Mark skill level as expert
            Driver.FindElement(
                By.CssSelector("#skill-list > div:nth-child(2) > div:nth-child(3) > select > option:nth-child(2)"))
                .Click();
            //Click on Add Skill button
            Driver.FindElement(By.CssSelector("#add")).Click();
            Thread.Sleep(1000);
            // Mark another skill Titanium
            Driver.FindElement(
                By.CssSelector("#skill-list > div:nth-child(3) > div:nth-child(2) > select > option:nth-child(36)"))
                .Click();
            // Mark skill level as want to learn
            Driver.FindElement(
                By.CssSelector("#skill-list > div:nth-child(3) > div:nth-child(3) > select > option:nth-child(5)"))
                .Click();
            Thread.Sleep(1000);
            // Click the update button to save skills to the database
            Driver.FindElement(By.XPath("//*[@id='edit-profile']/button[3]")).Click();
            Thread.Sleep(1000);
            // Log user out
            Driver.FindElement(
                By.CssSelector("#app-navbar-collapse > ul.nav.navbar-nav.navbar-right > li > ul > li:nth-child(2) > a"))
                .Click();
        }

        private void WaitUntilStatusElementVisible()
        {
            throw new NotImplementedException();
        }

        private void WaitForDropdownToggleElement()
        {
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
                wait.Until<IWebElement>((d) =>
                {
                    IWebElement element = d.FindElement(By.ClassName("dropdown-toggle"));
                    if (element.Displayed &&
                        element.Enabled)
                    {
                        return element;
                    }

                    return null;
                });
            }
        }

        private void WaitUntilNameElementVisible()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until((d) =>
            {
                IWebElement element = d.FindElement(By.Id("email"));
                if (element.Displayed &&
                    element.Enabled)
                {
                    return element;
                }

                return null;
            });
        }

        [Test]
        public void ChromeLoginWithWrongCredentials()
        {
            Driver.Navigate().GoToUrl("google.com");
            // Login link
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(1)>a"))
                .Click();
            Driver.FindElement(By.Id("email")).SendKeys("nora1.smith1@troyweb.com");
            Driver.FindElement(By.Id("password")).SendKeys("Tracker1Test");
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
            Thread.Sleep(2000);
            // Verify statement appears after an existing user tries to login with the wrong credentials, message is same for wrong username or password
            Assert.That(Driver.PageSource.Contains("These credentials do not match our records."), Is.EqualTo(true),
                "this string of text does not exists");
        }

        [Test]
        public void ChromeWhatcanidoRegisterTest()
        {
            Driver.Navigate().GoToUrl("google.com");
            Driver.FindElement(
                By.CssSelector("#app-navbar-collapse > ul.nav.navbar-nav.navbar-right > li:nth-child(2) > a")).Click();
            Driver.FindElement(By.Id("first_name")).SendKeys("John");
            Driver.FindElement(By.Id("last_name")).SendKeys("Test");
            Driver.FindElement(By.Id("middle")).SendKeys("D");
            Driver.FindElement(By.Id("email")).SendKeys("nora1.smith1@troyweb.com");
            Driver.FindElement(By.Id("password")).SendKeys("TrackerTest1");
            Driver.FindElement(By.Id("password-confirm")).SendKeys("TrackerTest1");
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
        }

        [Test]
        public void ChromeActivateUserViaAdmin()
        {
            Driver.Navigate().GoToUrl("google.com");
            // Login link
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(1)>a"))
                .Click();
            WaitUntilEmailElementVisible();
            Driver.FindElement(By.Id("email")).SendKeys("theadmin@example.com");
            Driver.FindElement(By.Id("password")).SendKeys("password1");
            // Click the Login button
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();


            WaitForAdminToggleElement();
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li>a"));
            Thread.Sleep(2000);
            //// Click update profile toggle
            //Driver.FindElement(By.XPath("//*[@id='app-navbar-collapse']/ul[2]/li/ul/li[2]/a")).Click();



            {

            }

        }

        private void WaitForAdminToggleElement()
        {
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
                wait.Until<IWebElement>((d) =>
                {
                    IWebElement element =
                        d.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li>a"));
                    if (element.Displayed &&
                        element.Enabled)
                    {
                        return element;
                    }

                    return null;
                });
            }
        }

        private void WaitUntilEmailElementVisible()
        {
        }

        [Test]
        public void ChromeRegisterExistingUserTest()
        {
            Driver.Navigate().GoToUrl("google.com");
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(2)>a"))
                .Click();
            WaitUntilNameElementVisible();
            Driver.FindElement(By.Id("first_name")).SendKeys("Johnny");
            Driver.FindElement(By.Id("last_name")).SendKeys("Test");
            Driver.FindElement(By.Id("middle")).SendKeys("D");
            Driver.FindElement(By.Id("email")).SendKeys("nora2.smith2@troyweb.com");
            Driver.FindElement(By.Id("password")).SendKeys("TrackerTest1");
            Driver.FindElement(By.Id("password-confirm")).SendKeys("TrackerTest1");
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
            Thread.Sleep(1000);

            // Verify statement appears after an existing user tries to register again
            Assert.That(Driver.PageSource.Contains(" that email already exists"), Is.EqualTo(true),
                "A user with that email already exists");
        }
    }

    [TestFixture]
    //  [Parallelizable]
    public class IETesting : Hooks
    {
        public IETesting() : base(BrowserType.IE)
        {
        }

        [Test]
        public void IELoginAndUpdateProfileTest()
        {
            Driver.Navigate().GoToUrl("google.com");
            // Login link
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(1)>a"))
                .Click();
            WaitUntilEmailElementVisible();
            Driver.FindElement(By.Id("email")).SendKeys("nora3.smith3@troyweb.com");
            Driver.FindElement(By.Id("password")).SendKeys("TrackerTest1");
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
            WaitForDropdownToggleElement();
            // Click update profile toggle
            Driver.FindElement(By.ClassName("dropdown-toggle")).Click();
            // Click on update profile toggle
            Thread.Sleep(1000);
            Driver.FindElement(
                By.CssSelector("#app-navbar-collapse > ul.nav.navbar-nav.navbar-right > li > ul > li:nth-child(1) > a"))
                .Click();
            // Change status to part time
            WaitUntilStatusElementVisible();
            Driver.FindElement(By.CssSelector("#edit-profile > div:nth-child(7) > div > select > option:nth-child(2)"))
                .Click();
            Thread.Sleep(1000);
            // add Backbone.js skill
            Driver.FindElement(By.CssSelector("#skill-list > div > div:nth-child(2) > select > option:nth-child(8)"))
                .Click();
            // add want to learn for skill level
            Driver.FindElement(By.CssSelector("#skill-list > div > div:nth-child(3) > select > option:nth-child(5)"))
                .Click();
            //Click on Add Skill button
            Driver.FindElement(By.CssSelector("#add")).Click();
            Thread.Sleep(1000);
            //Add another skill C++
            Driver.FindElement(
                By.CssSelector("#skill-list > div:nth-child(2) > div:nth-child(2) > select > option:nth-child(11)"))
                .Click();
            //Mark skill level as expert
            Driver.FindElement(
                By.CssSelector("#skill-list > div:nth-child(2) > div:nth-child(3) > select > option:nth-child(2)"))
                .Click();
            Thread.Sleep(1000);
            // Click the update button to save skills to the database
            Driver.FindElement(By.XPath("//*[@id='edit-profile']/button[3]")).Click();
            Thread.Sleep(2000);
            // Log user out
            Driver.FindElement(
                By.CssSelector("#app-navbar-collapse > ul.nav.navbar-nav.navbar-right > li > ul > li:nth-child(2) > a"))
                .Click();
        }

        private static void WaitUntilStatusElementVisible()
        {
            Thread.Sleep(5000);
        }

        private void WaitForDropdownToggleElement()
        {
        }

        private void WaitUntilEmailElementVisible()
        {
        }

        [Test]
        public void IELoginWithWrongCredentials()
        {
            Driver.Navigate().GoToUrl("google.com");
            // Login link
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(1)>a"))
                .Click();
            Driver.FindElement(By.Id("email")).SendKeys("nora1.smith1@troyweb.com");
            Driver.FindElement(By.Id("password")).SendKeys("Test4Tracker");
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
            Thread.Sleep(3000);
            // Verify statement appears after an existing user tries to login with the wrong credentials, message is same for wrong username or password
            Assert.That(Driver.PageSource.Contains("These credentials do not match our records."), Is.EqualTo(true),
                "this string of text does not exists");
        }

        //private void WaitUntilEmailElementVisible()
        //{
        //    throw new NotImplementedException();
        //}

        [Test]
        public void IEWhatcanidoRegisterTest()
        {
            Driver.Navigate().GoToUrl("google.com");
            // Register user
            Driver.FindElement(
                By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(2)>a")).Click();
            WaitUntilNameElementVisible();
            Driver.FindElement(By.Id("first_name")).SendKeys("Jon");
            Driver.FindElement(By.Id("last_name")).SendKeys("Test");
            Driver.FindElement(By.Id("middle")).SendKeys("D");
            WaitUntilEmailElementVisible();
            Driver.FindElement(By.Id("email")).SendKeys("nora3.smith3@troyweb.com");
            Driver.FindElement(By.Id("password")).SendKeys("TrackerTest1");
            Driver.FindElement(By.Id("password-confirm")).SendKeys("TrackerTest1");
            // Click submit button
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
        }

        private void WaitUntilNameElementVisible()
        {
        }

        [Test]
        public void IERegisterExistingUserTest()
        {
            Driver.Navigate().GoToUrl("google.com");
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(2)>a"))
                .Click();
            WaitUntilNameElementVisible();
            Driver.FindElement(By.Id("first_name")).SendKeys("Johnny");
            Driver.FindElement(By.Id("last_name")).SendKeys("Test");
            Driver.FindElement(By.Id("middle")).SendKeys("D");
            Driver.FindElement(By.Id("email")).SendKeys("nora2.smith2@troyweb.com");
            Driver.FindElement(By.Id("password")).SendKeys("TrackerTest1");
            Driver.FindElement(By.Id("password-confirm")).SendKeys("TrackerTest1");
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
            Thread.Sleep(1000);

            // Verify statement appears after an existing user tries to register again
            Assert.That(Driver.PageSource.Contains(" that email already exists"), Is.EqualTo(true),
                "A user with that email already exists");
        }

        [Test]
        public void IEActivateUserViaAdmin()
        {
            Driver.Navigate().GoToUrl("google.com");
            // Login link
            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li:nth-child(1)>a"))
                .Click();
            WaitUntilEmailElementVisible();
            Driver.FindElement(By.CssSelector("#email")).SendKeys("theadmin@example.com");
            Driver.FindElement(By.CssSelector("#password")).SendKeys("password1");
            // Click the Login button
            Driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();


            //WaitForAdminToggleElement();

            Driver.SwitchTo().Window(Driver.WindowHandles.Last());

            Driver.FindElement(By.CssSelector("#app-navbar-collapse>ul.nav.navbar-nav.navbar-right>li>a"));
            Thread.Sleep(2000);
            //// Click update profile toggle
            Driver.FindElement(By.XPath("//*[@id='app-navbar-collapse']/ul[2]/li/ul/li[2]/a")).Click();
        }

        private void WaitForAdminToggleElement()
        {
        }
    }
}