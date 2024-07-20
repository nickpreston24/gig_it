using CodeMechanic.Diagnostics;
using CodeMechanic.Types;
using gig_it.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Dapper;

namespace gig_it.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty] public string Query { get; set; } = string.Empty;

    public List<GigOffer> Offers => offers;
    private static List<GigOffer> offers = new();


    public GigOffer Entry = new GigOffer()
    {
        app_name = "Doordash",
        offer = 6.00,
        distance_mi = 5.0,
        MPG = 23.00
    };

    private double low_offer_upper_bound = .50;
    private double high_offer_lower_bound = 1.00;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> OnPostAddOffer()
    {
        Console.WriteLine(nameof(OnPostAddOffer));
        return Content("Thanks!");
    }

    public async Task<IActionResult> OnGetSearch(int start = 0, int end = 10, string query = "", bool debug = false)
    {
        if (offers.Count > 1)
            return Partial("_Leaderboard", this); // only update if the search or something else changes.
        Console.WriteLine(nameof(OnGetSearch));
        using var connection = SQLConnections.GetMySQLConnectionString().AsConnection();
        offers = (await connection
                .QueryAsync<GigOffer>($@"select * from offers # LIMIT {end} OFFSET {start};"))
            .If(query.NotEmpty()
                , items => items
                    .Where(i => i.ToString().Contains(query)))
            .ToList();
        if (debug) Console.WriteLine($"limit: {end}, offset: {start}, query: {query}");
        // if (debug) offers.Dump(nameof(offers));
        return Partial("_Leaderboard", this);
    }
}