package com.pippel.tyche

import android.content.Context
import androidx.core.content.ContextCompat
import androidx.recyclerview.widget.DividerItemDecoration

class VerticalDividerItemDecoration(context: Context) :
    DividerItemDecoration(context, DividerItemDecoration.VERTICAL) {

    init {
        ContextCompat.getDrawable(context, R.drawable.divider)?.let { setDrawable(it) }
    }

}