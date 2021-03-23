package com.pippel.tyche.mypools.data

import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import com.pippel.tyche.Gambler
import com.pippel.tyche.api.PoolApi
import kotlinx.coroutines.flow.Flow
import javax.inject.Inject

class DefaultFindMyPoolsAction @Inject constructor(
    private val api: PoolApi,
    private val gambler: Gambler
) :
    FindMyPoolsAction {

    override suspend fun execute(filter: String): Flow<PagingData<MyPoolModel>> = Pager(
        config = PagingConfig(pageSize = 20, prefetchDistance = 2),
        pagingSourceFactory = {
            MyPoolsDataSource(MyPoolsQuery(api, gambler)).apply {
                this.filter = filter
            }
        }
    ).flow

}
