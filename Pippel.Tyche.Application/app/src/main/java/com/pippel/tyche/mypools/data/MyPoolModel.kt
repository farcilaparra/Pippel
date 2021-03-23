package com.pippel.tyche.mypools

import com.pippel.tyche.ProgressIndicatorModel
import java.util.*

data class MyPoolModel(
    val poolID: UUID,
    val gamblerID: UUID,
    val masterPoolName: String,
    val currentPosition: Int?,
    val beforePosition: Int?
) {

    val positionReview: ProgressIndicatorModel =
        ProgressIndicatorModel(currentPosition, beforePosition)

}
