namespace Pippel.Tyche.Bet.Api

open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Pippel.Data
open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Core
open Pippel.Core.Json
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Api.Domain.Mappers
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Data.Repositories.Queries
open Pippel.Tyche.Bet.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Models

type Startup private () =

    let addRepositoriesToInjection (services: IServiceCollection) =

        services.AddScoped<IMatchRepository, MatchRepositoryInDB>()
        |> ignore

        services.AddScoped<IGroupBetGamblerRepository, GroupBetGamblerRepositoryInDB>()
        |> ignore

        services.AddScoped<IBetRepository, BetRepositoryInDB>()
        |> ignore

        services.AddScoped<IHistoryBetRepository, HistoryBetRepositoryInDB>()
        |> ignore
        
        services.AddScoped<IFindMatchesByGroupBetAction, FindMatchesByGroupBetAction>()
        |> ignore

    let addQueryRepositoriesToInjection (services: IServiceCollection) =

        services.AddTransient<IQueryRepository<MatchGamblerViewDao>, QueryRepositoryInDB<MatchGamblerViewDao>>()
        |> ignore
        
        services.AddTransient<IQueryRepository<MatchViewDao>, QueryRepositoryInDB<MatchViewDao>>()
        |> ignore

    let addDomainMappersToInjection (services: IServiceCollection) =

        services.AddScoped<IMapper<Bet, BetDao>, BetDomainMapper>()
        |> ignore

        services.AddScoped<IMapper<HistoryBet, HistoryBetDao>, HistoryBetDomainMapper>()
        |> ignore

        services.AddScoped<IMapper<GroupBetGambler, GroupBetGamblerDao>, GroupBetGamblerDomainMapper>()
        |> ignore

        services.AddScoped<IMapper<Match, MatchDao>, MatchDomainMapper>()
        |> ignore

    let addViewMappersToInjection (services: IServiceCollection) =

        services.AddTransient<MatchGamblerViewMapper>()
        |> ignore

        services.AddTransient<MatchViewMapper>() |> ignore

        services.AddTransient<EditingBetMapper>()
        |> ignore

    let addActionsToInjection (services: IServiceCollection) =

        services.AddScoped<IFindGroupBetGamblerByKeyAction, FindGroupBetGamblerByKeyAction>()
        |> ignore

        services.AddScoped<IFindMatchByKeyAction, FindMatchByKeyAction>()
        |> ignore

        services.AddScoped<IFindBetByKeyAction, FindBetByKeyAction>()
        |> ignore

        services.AddScoped<IUpdateBetsAction, UpdateBetsAction>()
        |> ignore

        services.AddScoped<IAddHistoryBetsAction, AddHistoryBetsAction>()
        |> ignore

        services.AddScoped<IAddBetsAction, AddBetsAction>()
        |> ignore

        services.AddScoped<IEditBetAction, EditBetAction>()
        |> ignore

        services.AddScoped<IFindOpenedGroupsMatchesByGamblerAction, FindOpenedGroupsMatchesByGamblerAction>()
        |> ignore

    new(configuration: IConfiguration) as this =
        Startup()
        then this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services
            .AddControllers()
            .AddJsonOptions(fun options -> options.JsonSerializerOptions.Converters.Add(DateTimeJsonConverter()))
        |> ignore

        services.AddDbContext<Context>(fun options ->
            options.UseOracle(this.Configuration.GetConnectionString("Default"))
            |> ignore)
        |> ignore

        services.AddDbContext<QueryContext>(fun options ->
            options.UseOracle(this.Configuration.GetConnectionString("Default"))
            |> ignore)
        |> ignore

        services.AddScoped<DbContext>(fun provider -> provider.GetService<Context>() :> DbContext)
        |> ignore

        services.AddTransient<IUnitOfWork, UnitOfWork>()
        |> ignore

        services.AddTransient<MatchGamblerViewMapper>()
        |> ignore
        
        services.AddTransient<MatchViewMapper>()
        |> ignore
        
        services.AddTransient<MatchGroupViewMapper>()
        |> ignore

        services.AddTransient<IQueryRepositoryFactory, QueryRepositoryFactory>()
        |> ignore

        addRepositoriesToInjection services
        addQueryRepositoriesToInjection services
        addDomainMappersToInjection services
        addViewMappersToInjection services
        addActionsToInjection services

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

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
