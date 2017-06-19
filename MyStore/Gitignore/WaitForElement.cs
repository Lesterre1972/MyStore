
private static void WaitUntilFirstNameElementVisible()
{
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
}