using System.Text;

namespace gig_it.Models;

public record GigOffer
{
    public string id { get; set; } = string.Empty;
    public string app_name { get; set; } = string.Empty;
    public string route_url { get; set; } = string.Empty;
    public double offer { get; set; }
    public double distance_mi { get; set; }
    public double fuel_cost_per_mi { get; set; }
    public double tank_size_gal { get; set; }
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
            .ToString();
    }
}