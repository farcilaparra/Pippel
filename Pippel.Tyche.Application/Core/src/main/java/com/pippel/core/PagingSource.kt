package com.pippel.core

import androidx.paging.PagingState
import java.lang.Exception

open class PagingSource<TModel : Any>(private val query: Query<TModel>) :
    androidx.paging.PagingSource<Int, TModel>() {

    private var _filter: String = ""
    var filter: String
        get() = _filter
        set(value) {
            _filter = value
        }

    override fun getRefreshKey(state: PagingState<Int, TModel>): Int = 0

    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, TModel> {
        val pageNumber = params.key ?: 0
        val pageSize = params.loadSize

        return try {
            val page = query.invoke(pageNumber * pageSize, pageSize, _filter)
            LoadResult.Page(
                data = page.items,
                prevKey = null,
                nextKey = page.next()
            )
        } catch (ex: Exception) {
            LoadResult.Error(ex)
        }
    }

}