using OpenQA.Selenium;

namespace OfferWebApplication.Tests.Tools.PageObjects;

public class AddPage : BasePage
{
    public AddPage(IWebDriver driver) : base(driver)
    {
    }

    private IWebElement NameTextField => ByXPath("//input[@name='name']");
    private IWebElement KeyTextField => ByXPath("//input[@name='key']");

    private const string SelectCategoryXPath = "//select[@name='category']";

    private IWebElement AddCategoryButton =>
        ByXPath($"{SelectCategoryXPath}/parent::div/following-sibling::div/child::button");

    private IWebElement AddInput => ByXPath("//input[@id='mat-input-3']");
    private IWebElement CreateButton => ByXPath("//span[contains(text(),'Create')]/parent::button");

    private const string SelectNetworksXPath = "//mat-select[@name='networks']";
    private IWebElement SelectNetWorks => ByXPath(SelectNetworksXPath);

    private const string AddNetworkButtonXPath =
        $"{SelectNetworksXPath}/parent::div/following-sibling::div/child::button";

    private IWebElement AddNetworkButton => ByXPath(AddNetworkButtonXPath);

    private const string SelectGroupXPath = "//mat-select[@name='group']";
    private IWebElement SelectGroup => ByXPath(SelectGroupXPath);

    private const string AddGroupButtonsXPath = "//span[contains(text(),'Add group')]/parent::button";

    private const string AddSegmentButtonsXPath = "//span[contains(text(),'Add segment')]/parent::button";
    private IWebElement SaveButton => ByXPath("//span[contains(text(),'Save')]/parent::button");

    private const string SelectPanelXPath = "//div[@class='cdk-overlay-pane']";

    private const string SelectSegmentXPath = "//mat-select[@name='val']";
    private IWebElement SelectSegment => ByXPath(SelectSegmentXPath);

    private const string SegmentValueXPath = $"{SelectSegmentXPath}/child::div[@class='mat-select-trigger']";
    private IWebElement AddNewSegmentButton => ByXPath("//mat-card-title/child::button");

    private IWebElement AddGroupButton =>
        ByXPath($"{SelectGroupXPath}/parent::div/following-sibling::div/child::button");

    private const string OrRadioButtonXPath = "//mat-radio-button[@value='or']";

    /// <summary>
    /// Fill in all mandatory fields with valid values
    /// </summary>
    public AddPage FillInValid(string name, string key)
    {
        SetNameField(name)
            .SetKeyField(key)
            .SelectExistingCategory(0)
            .SelectExistingGroup(0)
            .SelectExistingNetworks(0)
            .AddGroup(0);
        return this;
    }

    public AddPage SetNameField(string name)
    {
        NameTextField.SendKeys(name);
        return this;
    }

    public AddPage SetKeyField(string key)
    {
        KeyTextField.SendKeys(key);
        return this;
    }

    public AddPage SelectExistingCategory(int index)
    {
        GetOptionByIndex(SelectCategoryXPath, index).Click();
        return this;
    }

    /// <summary>
    /// Return true if category==name shows in dropDown list
    /// </summary>
    public bool CategoryExistsByName(string name)
    {
        return GetOptionByText(SelectCategoryXPath, name) != null;
    }

    /// <summary>
    /// Return true if network==name shows in dropDown list
    /// </summary>
    public bool NetworkExistsByName(string name)
    {
        SelectNetWorks.Click();
        var res = GetCheckBoxByText(name) != null;
        SelectNetWorks.SendKeys(Keys.Escape);
        return res;
    }

    /// <summary>
    /// Return true if group==name shows in dropDown list
    /// </summary>
    public bool GroupExistsByName(string name)
    {
        SelectGroup.Click();
        var res = GetMatOptionByText(name) != null;
        SelectGroup.SendKeys(Keys.Escape);
        return res;
    }

    /// <summary>
    /// Return true if segment==name shows in dropDown list
    /// </summary>
    public bool SegmentExistsByName(string name)
    {
        SelectSegment.Click();
        var res = GetMatOptionByText(name) != null;
        SelectSegment.SendKeys(Keys.Escape);
        return res;
    }

    public AddPage AddNewCategory(string value)
    {
        AddCategoryButton.Click();
        AddInput.SendKeys(value);
        CreateButton.Click();
        return this;
    }

    public AddPage AddNewNetwork(string value)
    {
        AddNetworkButton.Click();
        AddInput.SendKeys(value);
        CreateButton.Click();
        return this;
    }

    public AddPage AddNewGroup(string value)
    {
        AddGroupButton.Click();
        AddInput.SendKeys(value);
        CreateButton.Click();
        return this;
    }

    public AddPage AddNewSegment(string value)
    {
        AddNewSegmentButton.Click();
        AddInput.SendKeys(value);
        CreateButton.Click();
        return this;
    }

    public AddPage SelectExistingNetworks(int index)
    {
        SelectNetWorks.Click();
        var checkBox = GetCheckBoxByIndex(index);
        checkBox.Click();
        SelectNetWorks.SendKeys(Keys.Escape);
        return this;
    }

    public AddPage SelectExistingGroup(int index)
    {
        SelectGroup.Click();
        var element = GetMatOptionByIndex(index);
        element.Click();
        SelectGroup.SendKeys(Keys.Escape);
        return this;
    }

    public AddPage SelectExistingSegment(int selectIndex, string segmentValue)
    {
        var segments = _driver.FindElements(By.XPath(SelectSegmentXPath));
        segments[selectIndex].Click();
        var element = GetMatOptionByText(segmentValue);
        element.Click();
        SelectSegment.SendKeys(Keys.Escape);
        return this;
    }

    public string GetExistingSegmentValue(int segmentIndex)
    {
        var segmentsValues = _driver.FindElements(By.XPath(SegmentValueXPath));
        return segmentsValues[segmentIndex].Text;
    }

    public AddPage AddGroup(int index)
    {
        var buttons = _driver.FindElements(By.XPath(AddGroupButtonsXPath));
        buttons[index].Click();
        return this;
    }

    public AddPage ClickOrRadioButton(int index)
    {
        var buttons = _driver.FindElements(By.XPath(OrRadioButtonXPath));
        buttons[index].Click();
        return this;
    }

    public AddPage AddSegment(int buttonIndex, int? segmentSetdex = null, string segmentValue = null)
    {
        var buttons = _driver.FindElements(By.XPath(AddSegmentButtonsXPath));
        buttons[buttonIndex].Click();
        if (segmentSetdex != null && segmentValue != null)
        {
            SelectExistingSegment(segmentSetdex.Value, segmentValue);
        }

        return this;
    }

    public bool IsSaveEnabled()
    {
        return SaveButton.Enabled;
    }

    public OffersPage ClickSaveButtonReturnToOffersPage()
    {
        SaveButton.Click();
        return new OffersPage(_driver);
    }

    private IWebElement GetOptionByText(string xPath, string text)
    {
        var elements = _driver.FindElements(By.XPath($"{xPath}/option[contains(text(),'{text}')]"));
        return elements.Count != 0 ? elements.First() : null;
    }
    private IWebElement GetOptionByIndex(string xPath, int index)
    {
        return ByXPath($"{xPath}/option[@value='{index}']");
    }

    private IWebElement GetMatOptionByIndex(int index)
    {
        var xPath = $"{SelectPanelXPath}//child::mat-option";
        var elements = _driver.FindElements(By.XPath(xPath));
        return elements[index];
    }

    private IWebElement GetMatOptionByText(string text)
    {
        var xpath = $"{SelectPanelXPath}//child::span[contains(text(),'{text}')]//parent::mat-option";
        var elements = _driver.FindElements(
            By.XPath(xpath));
        return elements.Count != 0 ? elements.First() : null;
    }

    private IWebElement GetCheckBoxByIndex(int index)
    {
        var elements = _driver.FindElements(By.XPath($"{SelectPanelXPath}//child::mat-pseudo-checkbox"));
        return elements[index];
    }

    private IWebElement GetCheckBoxByText(string text)
    {
        var elements = _driver.FindElements(By.XPath(
            $"//span[@class='mat-option-text' and contains(text(),'{text}')]//preceding-sibling::mat-pseudo-checkbox"));
        return elements.Count != 0 ? elements.First() : null;
    }
}
