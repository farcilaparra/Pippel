module VatTests

open System.Net
open System.Net.Http
open System.Text
open System.Text.Json
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.TestHost
open Microsoft.Extensions.DependencyInjection
open Pippel.Tax.Domain.Mappers
open Pippel.Tax.Domain.Models
open Pippel.Tax.View.Mappers
open Pippel.Tax.View.Models
open Xunit
open System
open System.Linq
open Microsoft.EntityFrameworkCore
open Pippel.Tax
open Pippel.Tax.Data.Models
open Pippel.Core

/// Creates a context
let createContext () =
    new TaxContext(DbContextOptionsBuilder<TaxContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options)

/// Creates example data
let createVatsDaos () =
    [| { VatDao.ID = Guid("d9986f7b-7790-412c-81e2-7bdc3b96f767")
         Name = "Vat for 5%"
         Percentage = 0.05 }
       { VatDao.ID = Guid("7c405f6f-355b-4177-a28a-0f14c30d806f")
         Name = "Vat for 10%"
         Percentage = 0.1 }
       { VatDao.ID = Guid("83ef4a5e-c91d-4897-ab55-e4f1890f5ca8")
         Name = "Vat for 15%"
         Percentage = 0.15 } |]

/// Creates a context with example data
let createContextWithData (vatEntities: VatDao []) =
    let context = createContext ()
    context.Vats.AddRange(vatEntities)
    context.SaveChanges() |> ignore
    context

/// Creates a http client
let createHttpClientWithData (vatEntities: VatDao []) =
    let builder =
        WebHostBuilder()
            .ConfigureTestServices(fun services ->
                                  services.AddScoped<TaxContext>(fun provider -> createContextWithData(vatEntities))
                                  |> ignore).UseEnvironment("Development").UseStartup<Startup>()

    let server = new TestServer(builder)
    server.CreateClient()

[<Fact>]
let ``given an id of a vat that exist when a request to get vat's info is raised then a correct answer is gotten`` () =
    async {
        let vatsDaos = createVatsDaos ()

        let vatDomainMapper =
            VatDomainMapper() :> IMapper<Vat, VatDao>

        let vatViewMapper = VatViewMapper() :> IMapper<VatDto, Vat>

        let vatsDtos =
            vatsDaos
            |> Array.map (fun x -> x |> vatDomainMapper.MapToSource |> vatViewMapper.MapToSource)

        let indexToFind = 1
        let httpClient = createHttpClientWithData (vatsDaos)

        let request =
            new HttpRequestMessage(HttpMethod("GET"), String.Format("/vat/{0}", vatsDtos.[indexToFind].Id))

        let! response = httpClient.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore

        Assert.Equal(HttpStatusCode.OK, response.StatusCode)

        let! resultText =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        let jsonOptions =
            JsonSerializerOptions(PropertyNamingPolicy = JsonNamingPolicy.CamelCase)

        Assert.Equal(JsonSerializer.Serialize(vatsDtos.[indexToFind], jsonOptions), resultText)
    }

[<Fact>]
let ``given an id of a vat that doesn't exist when a request to get vat's info is executed then a code of error NotFound is returned`` () =
    async {
        let httpClient =
            createHttpClientWithData (createVatsDaos ())

        let request =
            new HttpRequestMessage(HttpMethod("GET"), String.Format("/vat/{0}", "631264e3-1414-438b-94f8-45f5f7901749"))

        let! response = httpClient.SendAsync(request) |> Async.AwaitTask

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode)

        let! resultText =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        let jsonOptions =
            JsonSerializerOptions(PropertyNamingPolicy = JsonNamingPolicy.CamelCase)

        let result =
            JsonSerializer.Deserialize<ExceptionResponse>(resultText, jsonOptions)

        Assert.Equal(LanguagePrimitives.EnumToValue ExceptionCode.NotFound, result.Error.Code)
    }

[<Fact>]
let ``given a vat when a request to persist it is executed then the vat is persisted`` () =
    async {
        let httpClient =
            createHttpClientWithData (createVatsDaos ())

        let request =
            new HttpRequestMessage(HttpMethod("POST"), "/vat")

        let jsonOptions =
            JsonSerializerOptions(PropertyNamingPolicy = JsonNamingPolicy.CamelCase)

        let json =
            JsonSerializer.Serialize
                ([| { VatDto.Id = null
                      Name = "Vat for 50%"
                      Percentage = 0.5 } |],
                 jsonOptions)

        request.Content <- new StringContent(json, Encoding.UTF8, "application/json")
        let! response = httpClient.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore

        Assert.Equal(HttpStatusCode.OK, response.StatusCode)

        let! resultText =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        let addedVats =
            JsonSerializer.Deserialize<VatDto seq>(resultText, jsonOptions)

        for addedVat in addedVats do
            Assert.NotNull(addedVat.Id)
    }

[<Fact>]
let ``given an vat that exists when a request to persist it is executed then a code of error AlreadyExist is returned`` () =
    async {
        let vatDaos = createVatsDaos ()
        let httpClient = createHttpClientWithData (vatDaos)

        let vatDomainMapper =
            VatDomainMapper() :> IMapper<Vat, VatDao>

        let vatViewMapper = VatViewMapper() :> IMapper<VatDto, Vat>

        let request =
            new HttpRequestMessage(HttpMethod("POST"), "/vat")

        let jsonOptions =
            JsonSerializerOptions(PropertyNamingPolicy = JsonNamingPolicy.CamelCase)

        let json =
            JsonSerializer.Serialize
                ([| vatDaos.ElementAt(0)
                    |> vatDomainMapper.MapToSource
                    |> vatViewMapper.MapToSource |],
                 jsonOptions)

        request.Content <- new StringContent(json, Encoding.UTF8, "application/json")
        let! response = httpClient.SendAsync(request) |> Async.AwaitTask

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode)

        let! resultText =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        let result =
            JsonSerializer.Deserialize<ExceptionResponse>(resultText, jsonOptions)

        Assert.Equal(LanguagePrimitives.EnumToValue ExceptionCode.AlreadyExist, result.Error.Code)
    }

[<Fact>]
let ``given a vat that persist when a request to update it is executed then the vat is updated`` () =
    async {
        let vatsDaos = createVatsDaos ()
        let httpClient = createHttpClientWithData (vatsDaos)

        let vatDomainMapper =
            VatDomainMapper() :> IMapper<Vat, VatDao>

        let vatViewMapper = VatViewMapper() :> IMapper<VatDto, Vat>

        let request =
            new HttpRequestMessage(HttpMethod("PUT"), "/vat")

        let vat =
            ({ vatsDaos.ElementAt(0) with
                   Name = "Vat for 50%"
                   Percentage = 0.5 })
            |> vatDomainMapper.MapToSource
            |> vatViewMapper.MapToSource

        let jsonOptions =
            JsonSerializerOptions(PropertyNamingPolicy = JsonNamingPolicy.CamelCase)

        let json =
            JsonSerializer.Serialize([| vat |], jsonOptions)

        request.Content <- new StringContent(json, Encoding.UTF8, "application/json")
        let! response = httpClient.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore

        Assert.Equal(HttpStatusCode.OK, response.StatusCode)

        let! resultText =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        let updatedVats =
            JsonSerializer.Deserialize<VatDto seq>(resultText, jsonOptions)

        Assert.Equal(vat, updatedVats.ElementAt(0))
    }

[<Fact>]
let ``given an id of a vat that persist when a request to remove is executed then its info is returned`` () =
    async {
        let vatsDaos = createVatsDaos ()
        let httpClient = createHttpClientWithData (vatsDaos)

        let vatDomainMapper =
            VatDomainMapper() :> IMapper<Vat, VatDao>

        let vatViewMapper = VatViewMapper() :> IMapper<VatDto, Vat>

        let request =
            new HttpRequestMessage(HttpMethod("DELETE"), "/vat")

        let vat =
            vatsDaos.ElementAt(0)
            |> vatDomainMapper.MapToSource
            |> vatViewMapper.MapToSource

        let jsonOptions =
            JsonSerializerOptions(PropertyNamingPolicy = JsonNamingPolicy.CamelCase)

        let json =
            JsonSerializer.Serialize([| vat.Id |], jsonOptions)

        request.Content <- new StringContent(json, Encoding.UTF8, "application/json")
        let! response = httpClient.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore

        Assert.Equal(HttpStatusCode.OK, response.StatusCode)

        let! resultText =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        let removedVats =
            JsonSerializer.Deserialize<VatDto seq>(resultText, jsonOptions)

        Assert.Equal(vat, removedVats.ElementAt(0))
    }
