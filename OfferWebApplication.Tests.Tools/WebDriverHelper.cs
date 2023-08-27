using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OfferWebApplication.Tests.Tools;

public static class WebDriverHelper
{
    public static DriverConfiguration Config;

    public static IWebDriver Create()
    {
        IWebDriver driver;
        var driverToUse = Config.DriverToUse;

        switch (driverToUse)
        {
            case DriverToUse.InternetExplorer:
                throw new NotImplementedException("Internet Explorer not yet supported");
            case DriverToUse.Firefox:
                throw new NotImplementedException("Firefox not yet supported");
            case DriverToUse.Chrome:
                driver = new ChromeDriver();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        driver.Manage().Window.Maximize();
        var timeouts = driver.Manage().Timeouts();

        timeouts.ImplicitWait = TimeSpan.FromSeconds(Config.ImplicitlyWait);
        timeouts.PageLoad = TimeSpan.FromSeconds(Config.PageLoadTimeout);

        return driver;
    }
}