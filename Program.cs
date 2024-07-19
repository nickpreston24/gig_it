using System.Reflection;
using CodeMechanic.Embeds;
using CodeMechanic.FileSystem;
using CodeMechanic.Types;
using CodeMechanic.RazorHAT.Services;

// Load and inject .env files & values
DotEnv.Load();

bool dev_mode = Environment.GetEnvironmentVariable("DEVMODE").ToBoolean();

var builder = WebApplication.CreateBuilder(args);


var props_service = new PropertyCache();
builder.Services.AddScoped<IJsonConfigService, JsonConfigService>();
builder.Services.AddSingleton<IMarkdownService, MarkdownService>();
builder.Services.AddSingleton<IImageService, ImageService>();
builder.Services.AddSingleton<IRazorRoutesService, RazorRoutesService>();
builder.Services.AddSingleton<IPropertyCache>(props_service);
builder.Services.AddSingleton<ICsvService>(new CsvService(props_service, dev_mode));

var main_assembly = Assembly.GetExecutingAssembly();
builder.Services.AddSingleton<IEmbeddedResourceQuery>(
    new EmbeddedResourceService(
            new Assembly[]
            {
                main_assembly
            },
            debugMode: false
        )
        .CacheAllEmbeddedFileContents());


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();