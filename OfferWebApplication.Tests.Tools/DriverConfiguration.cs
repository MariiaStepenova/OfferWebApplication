namespace OfferWebApplication.Tests.Tools;
public enum DriverToUse
{
    InternetExplorer,
    Chrome,
    Firefox
}

public class DriverConfiguration
{
    public DriverToUse DriverToUse { get; set; }
    public string Url { get; set; }
    public int ImplicitlyWait { get; set; }
    public int PageLoadTimeout { get; set; }
}