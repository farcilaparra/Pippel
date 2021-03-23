package com.pippel.tyche.mypools

import com.pippel.core.PagingSource
import com.pippel.core.Query
import javax.inject.Inject

class MyPoolsDataSource @Inject constructor(query: Query<MyPoolModel>) :
    PagingSource<MyPoolModel>(query)
