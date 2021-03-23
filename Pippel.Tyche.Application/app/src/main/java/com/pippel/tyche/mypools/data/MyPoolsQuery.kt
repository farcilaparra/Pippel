package com.pippel.tyche.mypools

import com.pippel.core.Page
import com.pippel.core.Query
import com.pippel.tyche.Gambler
import com.pippel.tyche.api.PoolApi
import com.pippel.tyche.mypools.data.MyPoolModel
import javax.inject.Inject

class MyPoolsQuery @Inject constructor(
    private val myPoolApi: PoolApi,
    private val gambler: Gambler
) :
    Query<MyPoolModel> {

    override suspend fun invoke(skip: Int, take: Int, filter: String): Page<MyPoolModel> {
        return myPoolApi.getMyPools(gambler.userID.toString(), skip, take, filter)
    }

}
