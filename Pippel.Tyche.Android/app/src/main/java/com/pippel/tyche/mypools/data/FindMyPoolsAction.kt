package com.pippel.tyche.mypools.data

import androidx.paging.PagingData
import kotlinx.coroutines.flow.Flow

interface FindMyPoolsAction {

    suspend fun execute(filter: String = ""): Flow<PagingData<MyPoolModel>>

}