package com.pippel.core

interface Query<TModel : Any> {

    suspend fun invoke(skip: Int, take: Int, filter: String = ""): Page<TModel>

}
