package com.pippel.tyche.poolcreation

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.widget.addTextChangedListener
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import com.pippel.core.coroutines.launchAsyncWhenCreated
import com.pippel.tyche.R
import com.pippel.tyche.databinding.FragmentPoolCreationBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.collectLatest
import java.util.*

@AndroidEntryPoint
class PoolCreationFragment : Fragment() {

    private lateinit var binding: FragmentPoolCreationBinding

    private val viewModel: PoolCreationViewModel by viewModels()

    private var _isLoadingFlow = MutableStateFlow<Boolean>(false)
    val isLoadingFlow: Flow<Boolean>
        get() = _isLoadingFlow.asStateFlow()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentPoolCreationBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.viewModel = viewModel

        initPoolNameTextEdit()
        initObservers()

        requireArguments().getString("masterPoolID")?.let {
            viewModel.loadMasterPool(UUID.fromString(it))
        }
    }

    private fun initObservers() {
        launchAsyncWhenCreated {
            viewModel.isLoadingFlow.collectLatest {
                _isLoadingFlow.value = it
            }
        }
    }

    private fun initPoolNameTextEdit() {
        with(binding.poolNameTextEdit) {
            addTextChangedListener {
                validatePoolName()
            }
            setOnFocusChangeListener { v, hasFocus ->
                if (hasFocus) {
                    validatePoolName()
                }
            }
        }
    }

    private fun validatePoolName(mustGainFocus: Boolean = false): Boolean {
        var isEmpty = true

        binding.poolNameTextEdit.text?.let {
            isEmpty = it.trim().isEmpty()
        }

        with(binding.poolNameInputLayout) {
            if (isEmpty) {
                error = getString(R.string.pool_name_empty_error)
            } else {
                isErrorEnabled = false
            }
        }

        if (isEmpty && mustGainFocus) {
            binding.poolNameTextEdit.requestFocus()
        }

        return !isEmpty
    }

    private fun validateAll(): Boolean {
        if (!validatePoolName(true)) {
            return false
        }
        return true
    }

    fun save() {
        if (!validateAll()) {
            return
        }
        viewModel.addPool()
    }

}