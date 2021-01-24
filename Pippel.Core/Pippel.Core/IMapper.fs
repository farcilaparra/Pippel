namespace Pippel.Core

[<Interface>]
type IMapper<'TSource, 'TTarget> =

    /// <summary>Maps from <c>TSource</c> to <c>TTarget</c></summary>
    abstract MapToTarget: 'TSource -> 'TTarget

    /// <summary>Maps from <c>TTarget</c> to <c>TSource</c></summary>
    abstract MapToSource: 'TTarget -> 'TSource
