using OfferWebApplication.Tests.Tools;
using OpenQA.Selenium;

namespace OfferWebApplication.Tests.Test.Scopes;

public class WebDriverTestScope : IDisposable
{
    public readonly IWebDriver Driver;
    public readonly string BaseUrl;

    public WebDriverTestScope() // MainPageScope
    {
        Driver = WebDriverHelper.Create();
        BaseUrl = WebDriverHelper.Config.Url;
        Driver.Navigate().GoToUrl(BaseUrl);
    }

    public void Dispose()
    {
        try
        {
            Driver.Quit();
        }
        catch (Exception) { }
    }
}