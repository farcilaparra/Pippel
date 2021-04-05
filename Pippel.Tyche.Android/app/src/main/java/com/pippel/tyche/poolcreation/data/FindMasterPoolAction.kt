package com.pippel.tyche.poolcreation.data

import com.pippel.tyche.MasterPool
import kotlinx.coroutines.flow.Flow
import java.util.*

interface FindMasterPoolAction {

    suspend fun execute(masterPoolID: UUID): Flow<MasterPool>

}