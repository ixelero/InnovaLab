using InnovaLab.CountryExplorer.Application;

WebApplication app = WebApplication
    .CreateBuilder(args)
    .ConfigureBuilder()
    .Build();

app.ConfigureApp();

await app.RunAsync();
