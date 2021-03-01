namespace Pippel.Tax.Api

open System
open Pippel.Core

type ResponseCreator() =
    interface IResponseCreator with

        member this.FuncCreateCustomCode(ex: Exception) : int = 0
