using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gig_it.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public double Offer { get; set; } = 6.0;
    public double Miles { get; set; } = 5.0;
    public double MPG { get; set; } = 0.00;

    private double low_offer_upper_bound = .50;
    private double high_offer_lower_bound = 1.00;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> OnGetStuff()
    {
        Console.WriteLine("ping!");
        return Content("hi there!");
    }
}