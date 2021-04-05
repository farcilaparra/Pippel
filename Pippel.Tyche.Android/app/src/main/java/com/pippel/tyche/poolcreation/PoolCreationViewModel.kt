package com.pippel.tyche.poolcreation

import androidx.databinding.Bindable
import com.pippel.core.ObservableViewModel
import com.pippel.core.coroutines.launchAsync
import com.pippel.tyche.BR
import com.pippel.tyche.Gambler
import com.pippel.tyche.poolcreation.data.AddPoolsAction
import com.pippel.tyche.poolcreation.data.FindMasterPoolAction
import com.pippel.tyche.poolcreation.data.PoolModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.collectLatest
import java.util.*
import javax.inject.Inject

@HiltViewModel
class PoolCreationViewModel @Inject constructor(
    private val findMasterPoolAction: FindMasterPoolAction,
    private val addPoolsAction: AddPoolsAction,
    private val gambler: Gambler
) :
    ObservableViewModel() {

    private lateinit var _masterPoolID: UUID

    private var _masterPoolName = ""
    val msaterPoolName: String
        get() {
            return _masterPoolName
        }

    private var _poolName = ""
    var poolName: String
        @Bindable get() {
            return _poolName
        }
        set(value) {
            _poolName = value
            notifyPropertyChanged(BR.poolName)
        }

    private var _isLoadingFlow = MutableStateFlow<Boolean>(false)
    val isLoadingFlow: Flow<Boolean>
        get() = _isLoadingFlow.asStateFlow()

    fun loadMasterPool(masterPoolID: UUID) {
        _masterPoolID = masterPoolID
        launchAsync {
            findMasterPoolAction.execute(masterPoolID).collectLatest {
                _masterPoolName = it.name
                poolName = _masterPoolName
            }
        }
    }

    private fun startLoading() {
        _isLoadingFlow.value = true
    }

    private fun endLoading() {
        _isLoadingFlow.value = false
    }

    fun addPool() {
        startLoading()
        launchAsync {
            addPoolsAction.execute(mutableListOf<PoolModel>().apply {
                add(
                    PoolModel(
                        null,
                        _masterPoolID.toString(),
                        gambler.userID.toString(),
                        _poolName
                    )
                )
            }).collectLatest {
                endLoading()
            }
        }
    }

}