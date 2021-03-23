package com.pippel.tyche.mypool

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.pippel.tyche.R
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class MyPoolActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.my_pool_activity)
    }

}
