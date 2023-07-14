namespace BusinessLib.Order;

public class DeliverWorks
{
    public decimal CalculateCost(int productId, Weight weight, TargetRegion targetRegion)
    {
        decimal cost = 1M;
        switch (weight)
        {
            case Weight.Small:
                cost = 1M;
                break;
            case Weight.Medium:
                cost = 1.3M;
                break;
            case Weight.Large:
                cost = 1.5M;
                break;
            case Weight.Xlarge:
                cost = 1.8M;
                break;
            case Weight.XXlarge:
                cost = 2.1M;
                break;
            case Weight.Heavy:
                if (targetRegion == TargetRegion.International)
                {
                    cost = 7.4M;
                }
                else
                {
                    cost = 3.1M;
                }
                break;
        }
        return cost;
    }
}

public enum Weight
{
    Small,
    Medium,
    Large,
    Xlarge,
    XXlarge,
    Heavy
}
public enum TargetRegion
{
    International,
    InCity,
    InCountry
}
