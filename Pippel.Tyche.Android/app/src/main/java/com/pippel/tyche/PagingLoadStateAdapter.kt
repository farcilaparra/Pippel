package com.pippel.tyche

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.paging.LoadState
import androidx.paging.LoadStateAdapter
import androidx.recyclerview.widget.RecyclerView

class PagingLoadStateAdapter : LoadStateAdapter<NetworkStateItemViewHolder>() {

    override fun onBindViewHolder(holder: NetworkStateItemViewHolder, loadState: LoadState) {}

    override fun onCreateViewHolder(
        parent: ViewGroup,
        loadState: LoadState
    ): NetworkStateItemViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.layout_network_state_indicator, parent, false)
        return NetworkStateItemViewHolder(view)
    }

}

class NetworkStateItemViewHolder(view: View) : RecyclerView.ViewHolder(view)
