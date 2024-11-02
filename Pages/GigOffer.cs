using System.Text;

namespace gig_it.Models;

public record GigOffer
{
    public string id { get; set; } = string.Empty;
    public string app_name { get; set; } = string.Empty;
    public string route_url { get; set; } = string.Empty;

    // [BindProperty(SupportsGet = true)] 
    public double offer { get; set; }

    // [BindProperty(SupportsGet = true)]
    public double distance_mi { get; set; }
    public double fuel_cost_per_mi { get; set; }

    public double tank_size_gal { get; set; }

    // [BindProperty(SupportsGet = true)] 
    public double MPG { get; set; } = 0.00;


    /*USER */
    public string UserName { get; set; } = "Anon";
    public int user_id { get; set; } = -1; // try: https://www.youtube.com/watch?v=wenGcYOfXiA

    /*Computed */
    public double run_cost => (distance_mi * fuel_cost_per_mi).Round(4);
    public double profit => (offer - Math.Abs(run_cost)).Round();
    public double percent_profit => (profit / offer * 1.00).Round();
    public double dollars_per_mile => (offer / distance_mi).Round(2);
    public double profit_per_mile => (profit / distance_mi).Round();


    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine($"{nameof(id)}:{id}")
            .AppendLine($"{nameof(app_name)}:{app_name}")
            .AppendLine($"{nameof(offer)}:{offer}")
            .AppendLine($"{nameof(distance_mi)}:{distance_mi}")
            .AppendLine($"{nameof(MPG)}:{MPG}")
            .AppendLine($"{nameof(profit)}:{profit}")
            .AppendLine($"{nameof(profit_per_mile)}:{profit_per_mile}")
            .ToString();
    }
}