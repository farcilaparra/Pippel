package com.pippel.core.coroutines

import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModel
import androidx.lifecycle.lifecycleScope
import androidx.lifecycle.viewModelScope
import kotlinx.coroutines.launch

fun Fragment.launchAsyncWhenCreated(block: suspend () -> Unit) {
    viewLifecycleOwner.lifecycleScope.launchWhenCreated {
        block()
    }
}

fun ViewModel.launchAsync(block: suspend () -> Unit) {
    viewModelScope.launch {
        block()
    }
}
