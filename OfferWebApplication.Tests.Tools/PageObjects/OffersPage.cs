using OfferWebApplication.Tests.Tools.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OfferWebApplication.Tests.Tools.PageObjects;

public class OffersPage : BasePage
{
    public OffersPage(IWebDriver driver) : base(driver)
    {

    }

    private const string AddButtonXPath = "//button[@routerlink='../add']";
    private IWebElement AddButton => ByXPath(AddButtonXPath);

    private const string YesButtonXPath = "//span[contains(text(),'yes!')]/parent::button";
    private IWebElement YesButton => ByXPath(YesButtonXPath);


    private const string TableRowsXPath = "//table/tbody/tr";
    private const int IdColumnIndex = 1;
    private const int NameColumnIndex = 2;
    private const int KeyColumnIndex = 3;
    private const int ButtonsColumnIndex = 4;


    private const string DeleteButtonXPath = "//span[contains(text(),'Delete')]/parent::button";

    public AddPage OpenAddPage()
    {
        AddButton.Click();
        return new AddPage(_driver);
    }

    /// <summary>
    /// Returns all rows where column name == name
    /// </summary>
    public List<OffersTableRow> FindRowsByColumnName(string name)
    {
        return FindRowsByColumn(NameColumnIndex, name);
    }

    public bool WaitUntilRowDeleted(string name)
    {
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        wait.PollingInterval = TimeSpan.FromMilliseconds(200);
        List<OffersTableRow> list = new List<OffersTableRow>();
        wait.Until(_ =>
        {
            list = FindRowsByColumn(NameColumnIndex, name);
            return list.Count == 0;
        });
        return list.Count == 0;
    }

    public OffersPage DeleteRowById(string id)
    {
        var row = FindRowsByColumn(IdColumnIndex, id);
        var deleteButton = FindDeleteButtonInTable(row.First().RowNumber);
        deleteButton.Click();
        YesButton.Click();
        return this;
    }

    private List<OffersTableRow> FindRowsByColumn(int columnIndex, string value)
    {
        var elementsCount = _driver.FindElements(By.XPath(TableRowsXPath)).Count;
        var offersTableRows = new List<OffersTableRow>();
        for (int i = 0; i < elementsCount; i++)
        {
            var tempName = FindElementInTable(TableRowsXPath, i + 1, columnIndex);
            if (tempName.Equals(value))
            {
                offersTableRows.Add(
                    new OffersTableRow()
                    {
                        Id = FindElementInTable(TableRowsXPath, i + 1, IdColumnIndex),
                        Name = FindElementInTable(TableRowsXPath, i + 1, NameColumnIndex),
                        Key = FindElementInTable(TableRowsXPath, i + 1, KeyColumnIndex),
                        RowNumber = i + 1
                    });
            }
        }

        return offersTableRows;
    }

    private string FindElementInTable(string tableXPath, int rowIndex, int columnIndex)
    {
        var elements = _driver.FindElements(
            By.XPath($"{tableXPath}[{rowIndex}]/td[{columnIndex}]"));
        return elements.Count != 0 ? elements.First().Text : string.Empty;
    }

    private IWebElement FindDeleteButtonInTable(int rowIndex)
    {
        return ByXPath($"{TableRowsXPath}[{rowIndex}]/td[{ButtonsColumnIndex}]{DeleteButtonXPath}");
    }
}