package com.pippel.tyche.api

import com.pippel.core.Page
import com.pippel.tyche.MasterPool
import com.pippel.tyche.mypools.data.MyPoolModel
import com.pippel.tyche.poolcreation.data.PoolModel
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Query

interface PoolApi {

    @GET("bet/opened")
    suspend fun getMyPools(
        @Query("gambler-id") gamblerID: String,
        @Query("skip") skip: Int,
        @Query("take") take: Int,
        @Query("filter") filter: String = ""
    ): Page<MyPoolModel>

    @GET("pool/master")
    suspend fun getMasterPool(@Query("master-pool-id") masterPoolID: String): MasterPool

    @POST("pool")
    suspend fun addPools(@Body poolsModels: List<PoolModel>): List<PoolModel>

}
