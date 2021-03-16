package com.pippel.tyche.mypool

import android.util.Log
import com.pippel.core.ModelReader
import java.util.*
import javax.inject.Inject
import kotlin.collections.ArrayList

class MyPoolModelReaderInMemory @Inject constructor() : ModelReader<MyPoolModel> {

    private val myPools: MutableList<MyPoolModel> = ArrayList()

    init {
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
    }

    override fun count(): Int {
        return myPools.size
    }

    override fun getByIndex(index: Int): MyPoolModel {
        Log.d("TYCHE", index.toString())
        return myPools[index]
    }

}
