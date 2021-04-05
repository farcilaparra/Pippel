package com.pippel.tyche.api

import com.pippel.core.Page
import com.pippel.tyche.MasterPool
import com.pippel.tyche.mypools.data.MyPoolModel
import com.pippel.tyche.poolcreation.data.PoolModel
import kotlinx.coroutines.delay
import java.util.*
import kotlin.collections.ArrayList

class PoolApiInMemory : PoolApi {

    private val myPools: MutableList<MyPoolModel> = ArrayList()

    init {
        initMyPoolsModels()
    }

    private fun initMyPoolsModels() {
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 1",
                1,
                2
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Eliminatorias al mundial Qatar 2024 marzo 20/21 de 2021 y abril 2/3 de 2021",
                7,
                2
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 3",
                3,
                3
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 4",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 5",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 6",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 7",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 8",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 9",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 10",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 11",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 12",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 13",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 14",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 15",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 16",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 17",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 18",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 19",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 20",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 21",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 22",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 23",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 24",
                null,
                null
            )
        )
        myPools.add(
            MyPoolModel(
                UUID.randomUUID(),
                UUID.randomUUID(),
                "Polla 25",
                null,
                null
            )
        )
    }

    override suspend fun getMyPools(
        gamblerID: String,
        skip: Int,
        take: Int,
        filter: String
    ): Page<MyPoolModel> {

        delay(1000)

        val newMyPools = myPools.filter {
            it.masterPoolName.toLowerCase(Locale.ROOT).contains(
                filter.toLowerCase(
                    Locale.ROOT
                )
            )
        }

        var toIndex = skip + take
        if (toIndex > newMyPools.size) {
            toIndex = newMyPools.size
        }

        return Page(
            skip / take,
            take,
            newMyPools.size,
            newMyPools.subList(skip, toIndex)
        )
    }

    override suspend fun getMasterPool(masterPoolID: String): MasterPool {
        return MasterPool(UUID.fromString(masterPoolID), "Copa América 2021")
    }

    override suspend fun addPools(poolsModels: List<PoolModel>): List<PoolModel> {
        delay(3000)
        return poolsModels
    }

}