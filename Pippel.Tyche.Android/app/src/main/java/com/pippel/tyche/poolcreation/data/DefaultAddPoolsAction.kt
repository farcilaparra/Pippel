package com.pippel.tyche.poolcreation.data

import com.pippel.tyche.api.PoolApi
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.flow
import javax.inject.Inject

class DefaultAddPoolsAction @Inject constructor(private val api: PoolApi) : AddPoolsAction {

    override suspend fun execute(poolsModels: List<PoolModel>): Flow<List<PoolModel>> =
        flow {
            emit(api.addPools(poolsModels))
        }

}