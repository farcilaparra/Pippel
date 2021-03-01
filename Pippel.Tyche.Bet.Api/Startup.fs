namespace Pippel.Tyche.Bet.Api

open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Mvc
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Pippel.Core.Json
open Pippel.Data
open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Core
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Data.Repositories.Queries
open Pippel.Tyche.Bet.Domain.Actions.Queries
open Pippel.Type
open Pippel.Type.DateTime
open Pippel.Type.NotEmptyString
open Pippel.Type.PositiveInt
open Pippel.Type.Uuid

type Startup private () =

    let addRepositoriesToInjection (services: IServiceCollection) =

        services.AddScoped<IMatchRepository, MatchRepositoryInDB>()
        |> ignore

        services.AddScoped<IPoolEnrollmentRepository, PoolEnrollmentRepositoryInDB>()
        |> ignore

        services.AddScoped<IBetRepository, BetRepositoryInDB>()
        |> ignore

        services.AddScoped<IPoolRepository, PoolRepositoryInDB>()
        |> ignore

    let addQueryRepositoriesToInjection (services: IServiceCollection) =

        services.AddTransient<IQueryRepository<PoolReviewViewDao>, QueryRepositoryInDB<PoolReviewViewDao>>()
        |> ignore

        services.AddTransient<IQueryRepository<MatchViewDao>, QueryRepositoryInDB<MatchViewDao>>()
        |> ignore

        services.AddTransient<IQueryRepository<MasterPoolMatchViewDao>, QueryRepositoryInDB<MasterPoolMatchViewDao>>()
        |> ignore

        services.AddTransient<IQueryRepository<BetPositionViewDao>, QueryRepositoryInDB<BetPositionViewDao>>()
        |> ignore

        services.AddTransient<IQueryRepository<OnPlayingMatchViewDao>, QueryRepositoryInDB<OnPlayingMatchViewDao>>()
        |> ignore

    let addActionsToInjection (services: IServiceCollection) =

        services.AddScoped<IFindPoolEnrollmentByKeyAction, FindPoolEnrollmentByKeyAction>()
        |> ignore

        services.AddScoped<IFindMatchByKeyAction, FindMatchByKeyAction>()
        |> ignore

        services.AddScoped<IFindBetByKeyAction, FindBetByKeyAction>()
        |> ignore

        services.AddScoped<IUpdateBetsAction, UpdateBetsAction>()
        |> ignore

        services.AddScoped<IAddBetsAction, AddBetsAction>()
        |> ignore

        services.AddScoped<IEditBetAction, EditBetAction>()
        |> ignore

        services.AddScoped<IFindOpenedMasterPoolsByGamblerAction, FindOpenedMasterPoolsByGamblerAction>()
        |> ignore

        services.AddScoped<IFindBetsByPoolAction, FindBetsByPoolAction>()
        |> ignore

        services.AddScoped<IFindMatchesByPoolAction, FindMatchesByPoolAction>()
        |> ignore

        services.AddScoped<IFindPoolByKeyAction, FindPoolByKeyAction>()
        |> ignore

        services.AddScoped<IFindMatchesByMasterPoolAction, FindMatchesByMasterPoolAction>()
        |> ignore

        services.AddScoped<IFindOnPlayingMatchesByMasterPoolAction, FindOnPlayingMatchesByMasterPoolAction>()
        |> ignore

    let initTypeConverters () = Uuid.initTypeConverter ()

    let initJsonOptions (options: JsonOptions) =
        options.JsonSerializerOptions.Converters.Add(UuidJsonConverter())
        options.JsonSerializerOptions.Converters.Add(NotEmptyStringJsonConverter())
        options.JsonSerializerOptions.Converters.Add(DateTimeJsonConverter())
        options.JsonSerializerOptions.Converters.Add(PositiveIntJsonConverter())

    new(configuration: IConfiguration) as this =
        Startup()
        then this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services
            .AddControllers()
            .AddJsonOptions(fun options -> initJsonOptions options)
        |> ignore

        services.AddDbContext<Context>
            (fun options ->
                options.UseOracle(this.Configuration.GetConnectionString("Default"))
                |> ignore)
        |> ignore

        services.AddDbContext<QueryContext>
            (fun options ->
                options.UseOracle(this.Configuration.GetConnectionString("Default"))
                |> ignore)
        |> ignore

        services.AddScoped<DbContext>(fun provider -> provider.GetService<Context>() :> DbContext)
        |> ignore

        services.AddTransient<IUnitOfWork, UnitOfWork>()
        |> ignore

        addRepositoriesToInjection services
        addQueryRepositoriesToInjection services
        addActionsToInjection services

        initTypeConverters ()

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseExceptionHandler
            (fun builder ->
                builder.Run
                    (fun context ->
                        ExceptionResponse.asyncUpdateResponseToDefaultError
                            context
                            (Exception.funcCreateCustomCode)
                            (DefaultJsonSerializer())
                        |> Async.StartAsTask
                        :> Task))
        |> ignore

        app.UseHttpsRedirection() |> ignore
        app.UseRouting() |> ignore

        app.UseAuthorization() |> ignore

        app.UseEndpoints(fun endpoints -> endpoints.MapControllers() |> ignore)
        |> ignore

    member val Configuration: IConfiguration = null with get, set
