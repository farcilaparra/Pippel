package com.pippel.tyche.poolcreation.data

import com.pippel.tyche.MasterPool
import com.pippel.tyche.api.PoolApi
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.flow
import java.util.*
import javax.inject.Inject

class DefaultFindMasterPoolAction @Inject constructor(private val api: PoolApi) :
    FindMasterPoolAction {

    override suspend fun execute(masterPoolID: UUID): Flow<MasterPool> =
        flow {
            emit(api.getMasterPool(masterPoolID.toString()))
        }

}