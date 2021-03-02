namespace Pippel.Core

open System
open System.Net
open System.Text
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Diagnostics
open Pippel.Core.Json

exception NotFoundException of string
exception AlreadyExistException of string

[<CLIMutable>]
type Error =
    { Code: string
      Message: string }

[<CLIMutable>]
type ExceptionResponse = { Error: Error }

type ExceptionCode =
    | Generic = 0
    | NotFound = 1
    | AlreadyExist = 2

module ExceptionResponse =

    [<Literal>]
    let private prefixCode = @"CORE"

    let isSystemException (ex: Exception) =
        ex.GetType().FullName.StartsWith("System.")

    let formatExceptionText (exceptionCode: ExceptionCode) = $"{prefixCode}-{string exceptionCode}"

    let private createCodeFromException (ex: Exception) (funcCreateCustomCode: Exception -> string) =
        match ex with
        | :? NotFoundException as ex -> formatExceptionText ExceptionCode.NotFound
        | :? AlreadyExistException as ex -> formatExceptionText ExceptionCode.AlreadyExist
        | _ ->
            match isSystemException ex with
            | true -> formatExceptionText ExceptionCode.Generic
            | false -> funcCreateCustomCode (ex)

    let createResponseText
        (context: HttpContext)
        (funcCreateCustomCode: Exception -> string)
        (jsonSerializer: IJsonSerializer)
        : string option =
        let feature =
            context.Features.Get<IExceptionHandlerPathFeature>()

        let ex = feature.Error

        match not <| isNull ex with
        | true ->
            jsonSerializer.Serialize(
                { ExceptionResponse.Error =
                      { Error.Code = createCodeFromException ex funcCreateCustomCode
                        Message = ex.Message } }
            )
            |> Some
        | false -> None

    let writeResponse (context: HttpContext) (content: string) =
        context.Response.ContentType <- "application/json"

        context.Response.StatusCode <-
            HttpStatusCode.InternalServerError
            |> LanguagePrimitives.EnumToValue

        let resultBytes = Encoding.UTF8.GetBytes(content)

        context.Response.Body.WriteAsync(resultBytes, 0, resultBytes.Length)
        |> Async.AwaitTask

    let asyncUpdateResponseToDefaultError
        (context: HttpContext)
        (funcCreateCustomCode: Exception -> string)
        (jsonSerializer: IJsonSerializer)
        =
        async {
            match createResponseText context funcCreateCustomCode jsonSerializer with
            | Some x -> do! writeResponse context x
            | None -> ()
        }
