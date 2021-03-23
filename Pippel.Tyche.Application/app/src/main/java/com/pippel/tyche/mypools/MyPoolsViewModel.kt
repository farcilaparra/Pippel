package com.pippel.tyche.mypools

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import com.pippel.core.coroutines.launchAsync
import com.pippel.tyche.mypools.data.FindMyPoolsAction
import com.pippel.tyche.mypools.data.MyPoolModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.*
import javax.inject.Inject

@HiltViewModel
class MyPoolsViewModel @Inject constructor(private val findMyPoolsAction: FindMyPoolsAction) :
    ViewModel() {

    private var _myPoolsFlow = MutableStateFlow<PagingData<MyPoolModel>>(PagingData.empty())
    val myPoolsFlow: Flow<PagingData<MyPoolModel>>
        get() = _myPoolsFlow.asStateFlow()

    init {
        loadMyPools()
    }

    private suspend fun createMyPoolsFlow(filter: String = ""): Flow<PagingData<MyPoolModel>> {
        return findMyPoolsAction.execute(filter).cachedIn(viewModelScope)
    }

    private fun loadMyPools() =
        launchAsync {
            createMyPoolsFlow().collectLatest {
                _myPoolsFlow.value = it
            }
        }

    fun filterMyPools(filter: String) =
        launchAsync {
            createMyPoolsFlow(filter).cachedIn(viewModelScope).collectLatest {
                _myPoolsFlow.value = it
            }
        }

}
