namespace Pippel.Core

type Page<'T> =
    { CurrentPage: int
      PageCount: int
      PageSize: int
      GroupCount: int
      ItemsCount: int64
      Items: 'T seq }
