namespace Pippel.Core

type Page<'T> =
    { CurrentPage: int
      PageCount: int
      PageSize: int
      ItemsCount: int64
      Items: 'T seq }

module Page =

    let map func page =
        { Page.Items = page.Items |> Seq.map func
          CurrentPage = page.CurrentPage
          PageCount = page.PageCount
          PageSize = page.PageSize
          ItemsCount = page.ItemsCount }

    let fromSeq seq =
        let count = seq |> Seq.length

        { Page.Items = seq
          CurrentPage = 0
          PageCount = count
          PageSize = count
          ItemsCount = count |> int64 }
