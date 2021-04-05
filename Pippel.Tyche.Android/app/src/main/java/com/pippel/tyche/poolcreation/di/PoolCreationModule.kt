package com.pippel.tyche.poolcreation.di

import com.pippel.tyche.MasterPool
import com.pippel.tyche.poolcreation.data.*
import dagger.Binds
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.components.FragmentComponent
import dagger.hilt.android.components.ViewModelComponent
import java.util.*

@Module
@InstallIn(FragmentComponent::class)
interface PoolCreationFragmentModule {
}

@Module
@InstallIn(ViewModelComponent::class)
interface PoolCreationViewModelModule {

    @Binds
    fun provideFindFindMasterPoolAction(impl: DefaultFindMasterPoolAction): FindMasterPoolAction

    @Binds
    fun provideAddPoolsAction(impl: DefaultAddPoolsAction): AddPoolsAction

}

@Module
@InstallIn(ViewModelComponent::class)
object PoolCreationViewModelObjectModule {

    @Provides
    fun provideMasterPool(): MasterPool {
        return MasterPool(UUID.randomUUID(), "Copa América Colombia/Argentina 2021")
    }

}