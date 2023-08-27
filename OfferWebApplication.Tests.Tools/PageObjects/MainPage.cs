using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OfferWebApplication.Tests.Tools.PageObjects;

public class MainPage : BasePage
{
    public MainPage(IWebDriver driver) : base(driver)
    {

    }

    private IWebElement Label(string name) =>
        ByXPath($"//mat-card-title[contains(text(),'{name}')]/following-sibling::mat-card-content");

    public int GetCategoriesCount(int? value = null)
    {
        return GetCount(Label("categories"), value);
    }

    public int GetNetworksCount(int? value = null)
    {
        return GetCount(Label("networks"), value);
    }

    public int GetGroupsCount(int? value = null)
    {
        return GetCount(Label("groups"), value);
    }

    public int GetOffersCount(int? value = null)
    {
        return GetCount(Label("offers"), value);
    }

    public int GetSegmentsCount(int? value = null)
    {
        return GetCount(Label("segments"), value);
    }

    private int GetCount(IWebElement webElement, int? value)
    {
        // wait until text will appear and count == value;
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2))
        {
            PollingInterval = TimeSpan.FromMilliseconds(200)
        };
        string text = string.Empty;
        wait.Until(_ =>
        {
            text = webElement.Text;
            return text != null && !text.Equals(string.Empty) &&
                   (value == null ||
                    int.Parse(text.Split(' ')[1]) == value);
        });
        return int.Parse(text.Split(' ')[1]);
    }
}