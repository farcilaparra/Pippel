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
type Error = { Code: int; Message: string }

[<CLIMutable>]
type ExceptionResponse = { Error: Error }

type ExceptionCode =
    | Generic = 0
    | NotFound = 100
    | AlreadyExist = 101


[<Interface>]
type IResponseCreator =

    abstract FuncCreateCustomCode: Exception -> int


module ExceptionResponse =

    /// Creates the code of error from an exception
    let private createCodeFromException (ex: Exception) (funcCreateCustomCode: Exception -> int) =
        match ex with
        | :? NotFoundException as ex -> LanguagePrimitives.EnumToValue ExceptionCode.NotFound
        | :? AlreadyExistException as ex -> LanguagePrimitives.EnumToValue ExceptionCode.AlreadyExist
        | _ -> funcCreateCustomCode (ex)

    /// Creates the response text of error
    let createResponseText (context: HttpContext)
                           (creator: IResponseCreator)
                           (jsonSerializer: IJsonSerializer)
                           : string option =
        let feature =
            context.Features.Get<IExceptionHandlerPathFeature>()

        let ex = feature.Error

        if not (isNull ex) then

            let json =
                jsonSerializer.Serialize
                    ({ ExceptionResponse.Error =
                           { Error.Code = createCodeFromException ex creator.FuncCreateCustomCode
                             Message = ex.Message } })

            Some json

        else
            None

    /// Updates the response of error
    let asyncUpdateResponseToDefaultError (context: HttpContext)
                                          (creator: IResponseCreator)
                                          (jsonSerializer: IJsonSerializer) =
        async {
            let jsonOpt = createResponseText context creator jsonSerializer

            match jsonOpt with
            | Some x ->
                context.Response.ContentType <- "application/json"
                context.Response.StatusCode <-
                    HttpStatusCode.InternalServerError
                    |> LanguagePrimitives.EnumToValue
                let resultBytes = Encoding.UTF8.GetBytes(x)
                do! context.Response.Body.WriteAsync(resultBytes, 0, resultBytes.Length)
                    |> Async.AwaitTask
            | None -> ()
        }
