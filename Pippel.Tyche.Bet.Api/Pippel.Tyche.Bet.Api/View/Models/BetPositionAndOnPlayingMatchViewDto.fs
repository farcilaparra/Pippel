namespace Pippel.Tyche.Bet.Data.Models.Queries

[<CLIMutable>]
type BetPositionAndOnPlayingMatchViewDto =
    { BetsPositions: BetPositionViewDto seq
      OnPlayingMatches: OnPlayingMatchViewDto seq }
