package com.pippel.tyche.mypools.di

import androidx.paging.PagingDataAdapter
import com.pippel.tyche.mypools.data.*
import dagger.Binds
import dagger.Module
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
abstract class MyPoolModule {

    @Binds
    abstract fun provideMyPoolPagingDataAdapter(impl: MyPoolPagingDataAdapter): PagingDataAdapter<MyPoolModel, MyPoolsViewHolder>

    @Binds
    @Singleton
    abstract fun provideFindMyPoolsAction(impl: DefaultFindMyPoolsAction): FindMyPoolsAction

}
