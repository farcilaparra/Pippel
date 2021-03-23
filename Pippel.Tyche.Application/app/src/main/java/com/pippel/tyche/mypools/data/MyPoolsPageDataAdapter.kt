package com.pippel.tyche.mypools

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.paging.PagingDataAdapter
import androidx.recyclerview.widget.DiffUtil
import com.pippel.tyche.R
import com.pippel.tyche.mypools.data.MyPoolModel
import javax.inject.Inject

class MyPoolPagingDataAdapter @Inject constructor() :
    PagingDataAdapter<MyPoolModel, MyPoolsViewHolder>(MyPoolComparator) {

    override fun onBindViewHolder(holder: MyPoolsViewHolder, position: Int) {
        getItem(position)?.let { holder.bind(it) }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyPoolsViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.layout_my_pool_item, parent, false)
        return MyPoolsViewHolder(view)
    }

}

object MyPoolComparator : DiffUtil.ItemCallback<MyPoolModel>() {

    override fun areItemsTheSame(oldItem: MyPoolModel, newItem: MyPoolModel) =
        oldItem.poolID == newItem.poolID

    override fun areContentsTheSame(oldItem: MyPoolModel, newItem: MyPoolModel) =
        oldItem == newItem

}
