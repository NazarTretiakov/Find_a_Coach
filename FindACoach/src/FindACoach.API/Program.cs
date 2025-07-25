using FindACoach.API.StartupExtensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureServices(builder.Configuration);


        var app = builder.Build();

        app.UseHsts();
        app.UseHttpsRedirection();

        app.UseStaticFiles();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}