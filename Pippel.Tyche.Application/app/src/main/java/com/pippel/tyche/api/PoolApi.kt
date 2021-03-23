package com.pippel.tyche.api

import com.pippel.core.Page
import com.pippel.tyche.mypools.MyPoolModel
import retrofit2.http.GET
import retrofit2.http.Query

interface PoolApi {

    @GET("bet/opened")
    suspend fun getMyPools(
        @Query("gambler-id") gamblerID: String,
        @Query("skip") skip: Int,
        @Query("take") take: Int,
        @Query("filter") filter: String = ""
    ): Page<MyPoolModel>

}
