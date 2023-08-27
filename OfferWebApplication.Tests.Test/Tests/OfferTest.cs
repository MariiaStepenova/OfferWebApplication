using NUnit.Framework;
using OfferWebApplication.Tests.Test.Scopes;
using OfferWebApplication.Tests.Tools.Models;
using OfferWebApplication.Tests.Tools.PageObjects;

namespace OfferWebApplication.Tests.Test.Tests;

[TestFixture]
public class OfferTest
{
    private string GetRandomStringValue()
    {
        return "Test_" + new Random().Next(10000, 99999);
    }

    [Test]
    [Description("Save Button is enabled when all fields are filled in")]
    public void TestAllFieldsFilledInSaveButtonEnabled()
    {
        using var scope = new WebDriverTestScope();

        var mainPage = new MainPage(scope.Driver);
        var enabled = mainPage
            .OpenOffersTab()
            .OpenAddPage()
            .FillInValid(GetRandomStringValue(), GetRandomStringValue())
            .IsSaveEnabled();

        Assert.True(enabled, "Expected Save button: Enabled, but it wasn't");
    }

    [Test]
    [Description("New category can be selected, new category is displayed on the dashboard")]
    public void TestAddNewCategory()
    {
        using var scope = new WebDriverTestScope();

        var mainPage = new MainPage(scope.Driver);
        var previousCategoriesCount = mainPage.GetCategoriesCount();
        var addPage = mainPage
            .OpenOffersTab()
            .OpenAddPage();

        var category = GetRandomStringValue();
        var exists = addPage
            .AddNewCategory(category)
            .CategoryExistsByName(category);
        Assert.True(exists, $"Expected new category in list, but it wasn't");

        var newCount = addPage
            .OpenDashboardTabMenuClicked()
            .GetCategoriesCount(previousCategoriesCount + 1);
        Assert.That(previousCategoriesCount + 1, Is.EqualTo(newCount),
            $"Expected Categories count on Dashboard: {previousCategoriesCount + 1}, but was: {newCount}");
    }

    [Test]
    [Description("New network can be selected, new network is displayed on the dashboard")]
    public void TestAddNewNetwork()
    {
        using var scope = new WebDriverTestScope();

        var mainPage = new MainPage(scope.Driver);
        var previousNetworksCount = mainPage.GetNetworksCount();
        var addPage = mainPage
            .OpenOffersTab()
            .OpenAddPage();

        var network = GetRandomStringValue();
        var exists = addPage
            .AddNewNetwork(network)
            .NetworkExistsByName(network);
        Assert.True(exists, $"Expected new network in list, but it wasn't");

        var newCount = mainPage
            .OpenDashboardTabMenuClicked()
            .GetNetworksCount(previousNetworksCount + 1);
        Assert.That(previousNetworksCount + 1, Is.EqualTo(newCount),
            $"Expected Networks count on Dashboard: {previousNetworksCount + 1}, but was: {newCount}");
    }

    [Test]
    [Description("New group can be selected, new group is displayed on the dashboard")]
    public void TestAddNewGroup()
    {
        using var scope = new WebDriverTestScope();

        var mainPage = new MainPage(scope.Driver);
        var previousGroupsCount = mainPage.GetGroupsCount();
        var addPage = mainPage
            .OpenOffersTab()
            .OpenAddPage();

        var network = GetRandomStringValue();
        var exists = addPage
            .AddNewGroup(network)
            .GroupExistsByName(network);

        Assert.True(exists, $"Expected new group in list, but it wasn't");

        var newCount = mainPage
            .OpenDashboardTabMenuClicked()
            .GetGroupsCount(previousGroupsCount + 1);
        Assert.That(previousGroupsCount + 1, Is.EqualTo(newCount),
            $"Expected Groups count on Dashboard: {previousGroupsCount + 1}, but was: {newCount}");
    }

    [Test]
    [Description("New segment can be selected, new group is displayed on the dashboard")]
    public void TestAddNewSegment()
    {
        using var scope = new WebDriverTestScope();

        var mainPage = new MainPage(scope.Driver);
        var previousSegmentsCount = mainPage.GetSegmentsCount();
        var addPage = mainPage
            .OpenOffersTab()
            .OpenAddPage();

        var segment = GetRandomStringValue();

        var exists = addPage
            .AddNewSegment(segment)
            .AddSegment(0)
            .SegmentExistsByName(segment);
        Assert.True(exists, $"Expected new segment in list, but it wasn't");

        var newCount = mainPage
            .OpenDashboardTabMenuClicked()
            .GetSegmentsCount(previousSegmentsCount + 1);

        Assert.That(previousSegmentsCount + 1, Is.EqualTo(newCount),
            $"Expected Segments count on Dashboard: {previousSegmentsCount + 1}, but was: {newCount}");
    }

    [Test]
    [Description("New offer is saved and shown in /list")]
    public void TestAddOfferOfferExistsInOffers()
    {
        using var scope = new WebDriverTestScope();

        var mainPage = new MainPage(scope.Driver);
        var addPage = mainPage
            .OpenOffersTab()
            .OpenAddPage();

        var name = GetRandomStringValue();
        var key = GetRandomStringValue();
        addPage.FillInValid(name, key);

        var rows = addPage
            .ClickSaveButtonReturnToOffersPage()
            .FindRowsByColumnName(name);
        Assert.That(rows.Count, Is.EqualTo(1), $"Expected 1 row with the name: {name}, but was: {rows.Count}");

        var expectedRow = new OffersTableRow()
        {
            Name = name,
            Key = key
        };
        Assert.That(rows.First(), Is.EqualTo(expectedRow));
    }

    [Test]
    [Description("2 segments and 2 groups can be added, filled in value is displayed correctly")]
    public void TestAddOfferWith2SegmentsAnd2Groups()
    {
        using var scope = new WebDriverTestScope();

        var mainPage = new MainPage(scope.Driver);
        var addPage = mainPage
            .OpenOffersTab()
            .OpenAddPage();

        var name = GetRandomStringValue();
        var key = GetRandomStringValue();
        addPage.FillInValid(name, key);


        var segment = GetRandomStringValue();
        addPage
            .AddNewSegment(segment)
            .ClickOrRadioButton(0)
            .AddSegment(0, 0, segment)
            .AddSegment(0, 1, segment)
            .AddGroup(0)
            .AddSegment(1, 2, segment)
            .AddSegment(1, 3, segment);


        for (int i = 0; i < 4; i++)
        {
            var segmentValue = addPage.GetExistingSegmentValue(i);
            Assert.That(segmentValue, Is.EqualTo(segment),
                $"Expected value for segment {i}: {segment}, but was: {segmentValue}");
        }

        var rows =
            addPage
                .ClickSaveButtonReturnToOffersPage()
                .FindRowsByColumnName(name);
        Assert.That(rows.Count, Is.EqualTo(1), $"Expected 1 row with the name: {name}, but was: {rows.Count}");

        var expectedRow = new OffersTableRow()
        {
            Name = name,
            Key = key
        };
        Assert.That(rows.First(), Is.EqualTo(expectedRow));
    }

    [Test]
    [Description("New offer is saved and shown in /dashboard")]
    public void TestAddOfferOfferExistsInDashboard()
    {
        using var scope = new WebDriverTestScope();
        
        var mainPage = new MainPage(scope.Driver);
        var previousOffersCount = mainPage.GetOffersCount();
        var addPage = mainPage
            .OpenOffersTab()
            .OpenAddPage();

        addPage.FillInValid(GetRandomStringValue(), GetRandomStringValue());

        var newCount = addPage
            .ClickSaveButtonReturnToOffersPage()
            .OpenDashboardTabMenuClicked()
            .GetOffersCount(previousOffersCount + 1);

        Assert.That(previousOffersCount + 1, Is.EqualTo(newCount),
            $"Expected Offers count on Dashboard: {previousOffersCount + 1}, but was: {newCount}");

    }

    [Test]
    [Description("Deleted offer is deleted and not shown in /list")]
    public void TestAddOfferDeleteOfferOfferNotExistsInOffersPage()
    {
        using var scope = new WebDriverTestScope();

        var mainPage = new MainPage(scope.Driver);
        var addPage = mainPage
            .OpenOffersTab()
            .OpenAddPage();

        var name = GetRandomStringValue();
        var key = GetRandomStringValue();
        addPage.FillInValid(name, key);

        var offersPage = addPage.ClickSaveButtonReturnToOffersPage();
        var rows = offersPage.FindRowsByColumnName(name);

        Assert.That(rows.Count, Is.EqualTo(1), $"Expected 1 row with the name: {name}, but was: {rows.Count}");
        offersPage.DeleteRowById(rows.First().Id);

        var deleted = offersPage.WaitUntilRowDeleted(name);

        Assert.True(deleted, $"Expected rows with the name: {name}: deleted, but it wasn't");
    }

    [Test]
    [Description("Deleted offer is deleted and not shown in /dashboard")]
    public void TestAddOfferDeleteOfferOfferNotExistsInDashboard()
    {
        using var scope = new WebDriverTestScope();

        var mainPage = new MainPage(scope.Driver);
        var previousOffersCount = mainPage.GetOffersCount();
        var addPage = mainPage
            .OpenOffersTab()
            .OpenAddPage();

        var name = GetRandomStringValue();
        var key = GetRandomStringValue();
        addPage.FillInValid(name, key);

        var offersPage = addPage.ClickSaveButtonReturnToOffersPage();
        var rows = offersPage.FindRowsByColumnName(name);

        Assert.That(rows.Count, Is.EqualTo(1), $"Expected 1 row with the name: {name}, but was: {rows.Count}");

        offersPage
            .DeleteRowById(rows.First().Id)
            .WaitUntilRowDeleted(name);

        var countAfterDeleting = addPage
            .OpenDashboardTabMenuClicked()
            .GetOffersCount(previousOffersCount);

        Assert.That(previousOffersCount, Is.EqualTo(countAfterDeleting),
            $"Expected Offers count on Dashboard: {previousOffersCount + 1}, but was: {countAfterDeleting}");
    }
}