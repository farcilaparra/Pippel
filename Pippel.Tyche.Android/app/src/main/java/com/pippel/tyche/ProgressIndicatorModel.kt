package com.pippel.tyche

import kotlin.math.abs

data class ProgressIndicatorModel(
    val current: Int?,
    val before: Int?) {

    var difference: Int = (current ?: 0) - (before ?: 0)

    var differenceToDisplay: String = abs(difference).toString()

}
