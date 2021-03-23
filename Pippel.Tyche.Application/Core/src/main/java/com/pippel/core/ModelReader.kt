package com.pippel.core

import androidx.annotation.NonNull

interface ModelReader<TModel> {

    fun count(): Int

    fun getByIndex(@NonNull index: Int): TModel

}
