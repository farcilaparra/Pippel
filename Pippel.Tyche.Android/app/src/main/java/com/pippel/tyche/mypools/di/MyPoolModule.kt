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
interface MyPoolModule {

    @Binds
    fun provideMyPoolPagingDataAdapter(impl: MyPoolPagingDataAdapter): PagingDataAdapter<MyPoolModel, MyPoolsViewHolder>

    @Binds
    @Singleton
    fun provideFindMyPoolsAction(impl: DefaultFindMyPoolsAction): FindMyPoolsAction

}
