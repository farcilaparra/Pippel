package com.pippel.tyche.poolcreation.data

import kotlinx.coroutines.flow.Flow

interface AddPoolsAction {

    suspend fun execute(poolsModels: List<PoolModel>): Flow<List<PoolModel>>

}