package com.pippel.tyche.poolcreation.data

data class PoolModel(
    val poolID: String?,
    val masterPoolID: String,
    val ownerGamblerID: String,
    val name: String
)