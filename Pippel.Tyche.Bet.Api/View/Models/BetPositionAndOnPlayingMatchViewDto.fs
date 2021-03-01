namespace Pippel.Tyche.Bet.Data.Models.Queries

type BetPositionAndOnPlayingMatchViewDto =
    { BetsPositions: BetPositionViewDto seq
      OnPlayingMatches: OnPlayingMatchViewDto seq }
