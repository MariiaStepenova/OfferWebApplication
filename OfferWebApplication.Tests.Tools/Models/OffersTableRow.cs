namespace OfferWebApplication.Tests.Tools.Models;

public class OffersTableRow
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public int RowNumber { get; set; }

    protected bool Equals(OffersTableRow other)
    {
        return Name == other.Name && Key == other.Key;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((OffersTableRow)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Key);
    }
}