using OpenQA.Selenium;

namespace OfferWebApplication.Tests.Tools.PageObjects;

public class BasePage
{
    protected readonly IWebDriver _driver;

    public BasePage(IWebDriver driver)
    {
        _driver = driver;
    }

    protected IWebElement OffersTab => ByXPath("//button[@routerlink='/list']");
    protected IWebElement DashBoardTab => ByXPath("//button[@routerlink='/dashboard']");
    protected IWebElement MenuSlider => ByClassName("mat-slide-toggle-thumb");

    protected IWebElement ByClassName(string className) => _driver.FindElement(By.ClassName(className));
    protected IWebElement ByXPath(string xPath) => _driver.FindElement(By.XPath(xPath));

    public OffersPage OpenOffersTab()
    {
        MenuSlider.Click();
        return OpenOffersTabMenuClicked();
    }

    public OffersPage OpenOffersTabMenuClicked()
    {
        OffersTab.Click();
        return new OffersPage(_driver);
    }

    public MainPage OpenDashboardTabMenuClicked()
    {
        DashBoardTab.Click();
        return new MainPage(_driver);
    }

}