using OpenQA.Selenium;

namespace MyStore
{
    internal enum PropertyType

    {
        ClassName,
        CssSelector,
        Id,
        Name
    }

    internal class PropertiesCollection
    {
        //Auto implement properties that we will use to locate elements
        public static IWebDriver driver { get; set; }
    }
}