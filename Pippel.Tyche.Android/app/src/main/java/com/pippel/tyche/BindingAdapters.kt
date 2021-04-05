
package com.pippel.tyche

import android.content.res.ColorStateList
import android.util.TypedValue
import android.view.View
import android.widget.ImageView
import android.widget.TextView
import androidx.core.content.ContextCompat
import androidx.core.view.isVisible
import androidx.databinding.BindingAdapter
import com.pippel.core.toLocalDateString
import java.util.*

@BindingAdapter("styleFromDifference")
fun setStyleFromDifference(view: ImageView, difference: Int) {
    when {
        difference == 0 -> {
            view.imageTintList =
                ColorStateList.valueOf(resolveAttr(view, R.attr.colorStableIndicator))
            view.setImageDrawable(
                ContextCompat.getDrawable(
                    view.context,
                    R.drawable.ic_baseline_remove_24
                )
            )
            view.rotation = 0f
        }
        difference > 0 -> {
            view.imageTintList = ColorStateList.valueOf(resolveAttr(view, R.attr.colorUpIndicator))
            view.setImageDrawable(
                ContextCompat.getDrawable(
                    view.context,
                    R.drawable.ic_baseline_north_24
                )
            )
            view.rotation = 0f
        }
        else -> {
            view.imageTintList =
                ColorStateList.valueOf(resolveAttr(view, R.attr.colorDownIndicator))
            view.setImageDrawable(
                ContextCompat.getDrawable(
                    view.context,
                    R.drawable.ic_baseline_north_24
                )
            )
            view.rotation = 180f
        }
    }
}

@BindingAdapter("styleFromDifference")
fun setStyleFromDifference(view: TextView, difference: Int) {
    when {
        difference == 0 -> {
            view.isVisible = false
            view.setTextColor(
                ColorStateList.valueOf(
                    resolveAttr(
                        view,
                        R.attr.colorStableIndicator
                    )
                )
            )
        }
        difference > 0 -> {
            view.isVisible = true
            view.setTextColor(ColorStateList.valueOf(resolveAttr(view, R.attr.colorUpIndicator)))
        }
        else -> {
            view.isVisible = true
            view.setTextColor(ColorStateList.valueOf(resolveAttr(view, R.attr.colorDownIndicator)))
        }
    }
}

@BindingAdapter("date")
fun setStyleFromDifference(view: TextView, date: Date) {
    view.text = date.toLocalDateString()
}

private fun resolveAttr(view: View, attrID: Int): Int {
    val value = TypedValue()
    view.context.theme.resolveAttribute(attrID, value, true)
    return value.data
}

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
