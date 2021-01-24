namespace Pippel.Tax

open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Pippel.Data
open Pippel.Data.EntityFrameworkCore
open Pippel.Tax.Api
open Pippel.Tax.Data.Repositories
open Pippel.Core
open Pippel.Core.Json
open Pippel.Tax.Domain.Mappers
open Pippel.Tax.View.Mappers

type Startup private () =
    new(configuration: IConfiguration) as this =
        Startup()
        then this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddControllers()
        |> ignore

        services.AddDbContext<TaxContext>(fun options ->
            options.UseNpgsql(this.Configuration.GetConnectionString("Default"))
            |> ignore)
        |> ignore

        services.AddScoped<DbContext>(fun provider -> provider.GetService<TaxContext>() :> DbContext)
        |> ignore

        services.AddTransient<IVatRepository, VatRepositoryInDB>()
        |> ignore

        services.AddTransient<IUnitOfWork, UnitOfWork>()
        |> ignore

        services.AddTransient<VatDomainMapper>() |> ignore

        services.AddTransient<VatViewMapper>() |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment())
        then app.UseDeveloperExceptionPage() |> ignore

        app.UseExceptionHandler(fun builder ->
            builder.Run(fun context ->
                ExceptionResponse.asyncUpdateResponseToDefaultError
                    context
                    (ResponseCreator())
                    (DefaultJsonSerializer())
                |> Async.StartAsTask :> Task))
        |> ignore

        app.UseHttpsRedirection() |> ignore
        app.UseRouting() |> ignore

        app.UseAuthorization() |> ignore

        app.UseEndpoints(fun endpoints -> endpoints.MapControllers() |> ignore)
        |> ignore

    member val Configuration: IConfiguration = null with get, set
