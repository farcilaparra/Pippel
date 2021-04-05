package com.pippel.tyche.mypools

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.pippel.tyche.databinding.ActivityMyPoolsBinding
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class MyPoolActivity : AppCompatActivity() {

    private lateinit var viewBinding: ActivityMyPoolsBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        viewBinding = ActivityMyPoolsBinding.inflate(layoutInflater)
        val view = viewBinding.root
        setContentView(view)
    }

}
