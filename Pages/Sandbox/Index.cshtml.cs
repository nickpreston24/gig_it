using System.Reflection;
using CodeMechanic.Diagnostics;
using CodeMechanic.Fake;
using CodeMechanic.Embeds;
using CodeMechanic.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using gig_it.Models;

namespace fake_razor_regex_fix.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnGetTypes()
    {
        int result = "10".ToInt();
        return Content(result.ToString());
    }

    public async Task<IActionResult> OnGetRunDiagnostics()
    {
        int result = "42".ToInt();
        result.Dump();
        return Content(result.ToString());
    }

    public async Task<IActionResult> OnGetReadEmbeds()
    {
        var lines = "";
        var ass = Assembly.GetExecutingAssembly();
        string filename = "my-json-file.json";
        lines = ass.ReadFile(filename);

        return Content(lines);
    }

    public async Task<IActionResult> OnGetFakeLib()
    {
        var phone = PhoneNumbers.GetPhone("(817) 565-2372");
        return Content(phone.area_code);
    }

    public async Task<IActionResult> OnGetPerson(string view = "")
    {
        Console.WriteLine("view: " + view);
        var person = Resources.Embedded.Person;
        Console.WriteLine(person);
        return Partial(view, person);
    }
}