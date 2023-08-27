using NUnit.Framework;
using OfferWebApplication.Tests.Tools;

namespace OfferWebApplication.Tests.Test.Tests;

[SetUpFixture]
public class Setup
{
    [OneTimeSetUp]
    public void Configure()
    {
        WebDriverHelper.Config = new DriverConfiguration
        {
            DriverToUse = DriverToUse.Chrome,
            ImplicitlyWait = 30,
            PageLoadTimeout = 60,
            Url = "https://test-task.gameteq.com"
        };
    }
}