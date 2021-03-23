package com.pippel.tyche

import android.content.res.ColorStateList
import android.util.TypedValue
import android.widget.ImageView
import android.widget.TextView
import androidx.databinding.BindingAdapter

@BindingAdapter("tintFromAttr")
fun setTintFromAttr(view: ImageView, attrID: Int) {
    val value = TypedValue()
    view.context.theme.resolveAttribute(attrID, value, true)
    view.imageTintList = ColorStateList.valueOf(value.data)
}

@BindingAdapter("textColorFromAttr")
fun setColorFromAttr(view: TextView, attrID: Int) {
    val value = TypedValue()
    view.context.theme.resolveAttribute(attrID, value, true)
    view.setTextColor(ColorStateList.valueOf(value.data))
}
