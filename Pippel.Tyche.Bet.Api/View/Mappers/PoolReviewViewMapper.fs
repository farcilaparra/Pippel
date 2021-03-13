namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Data.Models.Queries

module PoolReviewViewMapper =

    let mapToView (poolReviewViewDao: PoolReviewViewDao) : PoolReviewViewDto =
        { PoolReviewViewDto.PoolID = poolReviewViewDao.PoolID
          GamblerID = poolReviewViewDao.GamblerID
          MasterPoolName = poolReviewViewDao.MasterPoolName
          CurrentPosition = poolReviewViewDao.CurrentPosition
          BeforePosition = poolReviewViewDao.BeforePosition }
