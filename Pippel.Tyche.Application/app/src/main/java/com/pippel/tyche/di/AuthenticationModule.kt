package com.pippel.tyche.di

import com.pippel.tyche.Gambler
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import java.util.*
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object AuthenticationModule {

    @Provides
    @Singleton
    fun provideGambler(): Gambler {
        return Gambler(UUID.fromString("49A849BD-B8BA-BC01-E053-020011ACD428"))
    }

}
