using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.Identity.Web;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web.Resource;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace PagedCached.oData;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

        //IEdmModel model0 = EdmModelBuilder.GetEdmModel();

        builder.Services.AddControllers().AddOData(opt => opt.Count().Filter().Expand().Select().OrderBy().SkipToken().SetMaxTop(5)
            .AddRouteComponents("odata", GetEdmModel()).SkipToken()
            //.AddRouteComponents(model0)
            //.AddRouteComponents("v1", model1)
            //.AddRouteComponents("v2{data}", model2, services => services.AddSingleton<ODataBatchHandler, DefaultODataBatchHandler>())
            //.AddRouteComponents("v3", model3)
            //.Conventions.Add(new MyConvention())
        );

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Use odata route debug, /$odata
        app.UseODataRouteDebug();

        // If you want to use /$openapi, enable the middleware.
        //app.UseODataOpenApi();

        // Add OData /$query middleware
        app.UseODataQueryRequest();

        // Add the OData Batch middleware to support OData $Batch
        app.UseODataBatching();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

    static IEdmModel GetEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();

        odataBuilder.EntitySet<WeatherForecast>(nameof(WeatherForecast));
        return odataBuilder.GetEdmModel();
    }
}
