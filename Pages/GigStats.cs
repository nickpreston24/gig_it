namespace gig_it.Pages;

/// <summary>
/// These are populated by SQL views for efficiency
/// </summary>
public record GigStats
{
    public double offers_total { get; set; }
    public double offer_count { get; set; }
    public double average_offer { get; set; }
    public double average_trip { get; set; }
    public double average_fuel_cost_per_mi { get; set; }
    public double average_mpg { get; set; }

    public double UberEats_Avg_Offer { get; set; }
    public double UberX_Avg_Offer { get; set; }
    public double Instacart_Avg_Offer { get; set; }
    public double DoorDash_Avg_Offer { get; set; }
}