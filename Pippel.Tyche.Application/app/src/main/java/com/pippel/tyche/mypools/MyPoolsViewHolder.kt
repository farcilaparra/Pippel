package com.pippel.tyche.mypools

import android.view.View
import androidx.recyclerview.widget.RecyclerView
import com.pippel.tyche.databinding.LayoutMyPoolItemBinding

class MyPoolsViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {

    private val binding: LayoutMyPoolItemBinding =
        LayoutMyPoolItemBinding.bind(itemView)

    fun bind(item: MyPoolModel) {
        binding.model = item
    }

}
