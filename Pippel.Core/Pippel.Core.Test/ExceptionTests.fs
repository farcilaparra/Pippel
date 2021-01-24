module ExceptionTests

open System
open System.Net
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Diagnostics
open Moq
open Xunit
open Pippel.Core
open Pippel.Core.Json

[<Fact>]
let ``given an exception when a text that represents the exception is created then an option with the text is returned`` () =
    let context = DefaultHttpContext() :> HttpContext

    let exceptionHandlerPathFeatureMock = Mock<IExceptionHandlerPathFeature>()
    exceptionHandlerPathFeatureMock.SetupGet(fun x -> x.Error).Returns(Exception("Occurs an error"))
    |> ignore

    let responseCreatorMock = Mock<IResponseCreator>()
    responseCreatorMock.Setup(fun x -> x.FuncCreateCustomCode(It.IsAny<Exception>())).Returns(0)
    |> ignore

    context.Features.Set<IExceptionHandlerPathFeature>(exceptionHandlerPathFeatureMock.Object)

    let jsonOpt =
        ExceptionResponse.createResponseText context responseCreatorMock.Object (DefaultJsonSerializer())

    Assert.True(jsonOpt.IsSome)

[<Fact>]
let ``given an exception when the response is updated then an the body of the response is updated`` () =
    let context = DefaultHttpContext() :> HttpContext

    let exceptionHandlerPathFeatureMock = Mock<IExceptionHandlerPathFeature>()
    exceptionHandlerPathFeatureMock.SetupGet(fun x -> x.Error).Returns(Exception("Occurs an error"))
    |> ignore

    let responseCreatorMock = Mock<IResponseCreator>()
    responseCreatorMock.Setup(fun x -> x.FuncCreateCustomCode(It.IsAny<Exception>())).Returns(0)
    |> ignore

    context.Features.Set<IExceptionHandlerPathFeature>(exceptionHandlerPathFeatureMock.Object)

    ExceptionResponse.asyncUpdateResponseToDefaultError context responseCreatorMock.Object (DefaultJsonSerializer())
    |> Async.RunSynchronously

    let buffer: byte array = Array.zeroCreate 4096

    let readSize =
        context.Response.Body.Read(buffer, 0, buffer.Length)

    Assert.Equal
        (context.Response.StatusCode,
         HttpStatusCode.InternalServerError
         |> LanguagePrimitives.EnumToValue)
