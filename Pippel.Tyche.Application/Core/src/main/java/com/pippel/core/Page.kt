package com.pippel.core

data class Page<TModel : Any>(
    val currentPage: Int,
    val pageSize: Int,
    val itemsCount: Int,
    val items: List<TModel>
) {

    fun hasNext(): Boolean {
        return currentPage < itemsCount / pageSize
    }

    fun next(): Int? {
        return if (hasNext()) currentPage + 1 else null
    }

}
