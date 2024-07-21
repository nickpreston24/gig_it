using System.Diagnostics;
using System.Text.RegularExpressions;
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
    private double low_offer_upper_bound = .50;
    private double high_offer_lower_bound = 1.00;
    public GigStats Stats => stats;
    private static GigStats stats = new();
    public List<GigOffer> Offers => offers;
    private static List<GigOffer> offers = new();

    [BindProperty(SupportsGet = true)] public string Query { get; set; } = string.Empty;

    public Regex QueryAsRegex => new Regex(Query.IsNullOrEmpty() ? ".*" : Query,
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

    [BindProperty(SupportsGet = true)] public string app_name { get; set; } = "Doordash";
    [BindProperty(SupportsGet = true)] public double MPG { get; set; } = 23.00;
    [BindProperty(SupportsGet = true)] public double offer { get; set; } = 10.00;
    [BindProperty(SupportsGet = true)] public double distance_mi { get; set; } = 15.00;


    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> OnGetStats()
    {
        Console.WriteLine(nameof(OnGetStats));
        var timer = Stopwatch.StartNew();
        using var connection1 = SQLConnections.GetMySQLConnectionString().AsConnection();
        using var connection2 = SQLConnections.GetMySQLConnectionString().AsConnection();
        var task1 = connection1.QueryAsync<GigStats>(@"Select * from AverageOffers;");
        var task2 = connection2.QueryAsync<GigStats>(@"select * from OffersOverView");


        await Task.WhenAll(task1, task2);

        var results = (await task1).ToList()
            .SingleOrDefault();
        var results2 = (await task2).ToList()
            .SingleOrDefault();
        // results2.Dump("gig stats");
        var merged_stats = new GigStats().With(gs =>
        {
            gs.Instacart_Avg_Offer = results.Instacart_Avg_Offer;
            gs.UberEats_Avg_Offer = results.UberEats_Avg_Offer;
            gs.UberX_Avg_Offer = results.UberX_Avg_Offer;
            gs.DoorDash_Avg_Offer = results.DoorDash_Avg_Offer;

            gs.average_fuel_cost_per_mi = results2.average_fuel_cost_per_mi;
            gs.average_offer = results2.average_offer;
            gs.average_mpg = results2.average_mpg;
            gs.average_trip = results2.average_trip;
        });

        timer.Stop();
        Console.WriteLine(timer.Elapsed);
        stats = merged_stats.Dump("merged");

        return Partial("_GigStats", this);
    }

    public async Task OnGetRandomize(GigOffer entry, bool debug = false)
    {
    }

    public async Task<IActionResult> OnPostAddOffer(GigOffer entry, bool debug = false)
    {
        if (debug) Console.WriteLine(nameof(OnPostAddOffer));
        if (debug) entry.Dump();

        Console.WriteLine($"dist: {distance_mi}");

        Console.WriteLine($"cost per gal: {MPG}");

        string query = $@"
                insert into offers (app_name, offer, distance_mi, mpg, fuel_cost_per_mi)
                values (@app_name, @offer, @distance_mi, @mpg, @fuel_cost_per_mi);
".Trim();

        using var connection = SQLConnections.GetMySQLConnectionString().AsConnection();
        int rows_affected = await connection
            .ExecuteAsync(query, entry);
        Console.WriteLine("rows :" + rows_affected);

        return Content($"Thanks!!\n {entry.ToString()}");
    }

    public async Task<IActionResult> OnGetSearch(
        int skip = 0
        , int take = 10
        , bool regex_match = true
        , bool debug = false)
    {
        Console.WriteLine($"limit: {take}, offset: {skip}, Query: {Query}");
        // if (offers.Count > 1)
        //     return Partial("_Leaderboard", this); // only update if the search or something else changes.
        Console.WriteLine(nameof(OnGetSearch));
        using var connection = SQLConnections.GetMySQLConnectionString().AsConnection();

        // get paginated results
        offers = (await connection
                .QueryAsync<GigOffer>($@"select * from offers LIMIT @limit OFFSET @offset;", new
                {
                    limit = 100,
                    offset = 0
                }))

            // filter using substring:
            .If(Query.NotEmpty()
                , items => items
                    .Where(offer => offer.ToString().Contains(Query, StringComparison.OrdinalIgnoreCase)))

            // filter using regular expressions:
            .If(regex_match, items => items
                .Where(offer =>
                    QueryAsRegex.IsMatch(
                        offer.ToString())
                ))
            .ToList();
        // if (debug) offers.Dump(nameof(offers));
        return Partial("_Leaderboard", this);
    }

    public async Task<IActionResult> OnDeleteRemove(int id = -1)
    {
        Console.WriteLine(nameof(OnDeleteRemove));
        return Content("It's working, its workiiiing!");

        if (id < 1)
            return Partial("_Leaderboard", this);

        return Partial("_Leaderboard", this);
    }

    // public GigOffer Entry = new GigOffer()
    // {
    //     app_name = "Doordash",
    //     offer = 6.00,
    //     distance_mi = 5.0,
    //     MPG = 23.00
    // };
}