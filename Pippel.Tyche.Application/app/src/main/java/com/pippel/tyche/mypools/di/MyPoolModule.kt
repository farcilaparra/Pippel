package com.pippel.tyche.mypool.di

import com.pippel.core.ModelReader
import com.pippel.tyche.mypool.*
import com.pippel.tyche.mypools.MyPoolModel
import dagger.Binds
import dagger.Module
import dagger.hilt.InstallIn
import dagger.hilt.android.components.ViewComponent

@Module
@InstallIn(ViewComponent::class)
abstract class MyPoolModule {

    @Binds
    abstract fun provideMyPoolModelReader(impl: MyPoolModelReaderInMemory): ModelReader<MyPoolModel>

}
