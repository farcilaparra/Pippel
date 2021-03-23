package com.pippel.tyche.mypool

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.annotation.NonNull
import androidx.recyclerview.widget.RecyclerView
import com.pippel.core.ModelReader
import com.pippel.tyche.R
import com.pippel.tyche.databinding.MyPoolItemLayoutBinding

class MyPoolAdapter(@NonNull val modelReader: ModelReader<MyPoolModel>) :
    RecyclerView.Adapter<MyPoolAdapter.MyPoolViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyPoolViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.my_pool_item_layout, parent, false)
        return MyPoolViewHolder(view)
    }

    override fun onBindViewHolder(holder: MyPoolViewHolder, position: Int) {
        holder.bind(modelReader.getByIndex(position))
    }

    override fun getItemCount(): Int {
        return modelReader.count()
    }

    inner class MyPoolViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {

        private val binding: MyPoolItemLayoutBinding =
            MyPoolItemLayoutBinding.bind(itemView)

        fun bind(item: MyPoolModel) {
            binding.model = item
        }

    }

}
