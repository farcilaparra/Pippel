package com.pippel.tyche.poolcreation

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.MenuItem
import android.view.WindowManager
import androidx.core.view.isVisible
import androidx.fragment.app.add
import androidx.fragment.app.commit
import com.pippel.core.coroutines.launchAsyncWhenCreated
import com.pippel.tyche.R
import com.pippel.tyche.databinding.ActivityPoolCreationBinding
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.flow.collectLatest
import java.util.*

@AndroidEntryPoint
class PoolCreationActivity : AppCompatActivity() {

    private lateinit var binding: ActivityPoolCreationBinding

    private lateinit var poolCreationFragment: PoolCreationFragment

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityPoolCreationBinding.inflate(layoutInflater)
        val view = binding.root
        setContentView(view)

        if (savedInstanceState == null) {
            initPoolCreationView()
        }
    }

    private fun initPoolCreationView() {
        val bundle = Bundle().apply {
            putString("masterPoolID", UUID.randomUUID().toString())
        }

        supportFragmentManager.commit {
            setReorderingAllowed(true)
            add<PoolCreationFragment>(R.id.poolCreationFragmentContainerView, args = bundle)
        }
    }

    override fun onPostCreate(savedInstanceState: Bundle?) {
        super.onPostCreate(savedInstanceState)

        poolCreationFragment =
            supportFragmentManager.findFragmentById(R.id.poolCreationFragmentContainerView) as PoolCreationFragment

        launchAsyncWhenCreated {
            poolCreationFragment.isLoadingFlow.collectLatest {
                showOrHideProgressIndicatorView(it)
            }
        }
    }

    private fun showOrHideProgressIndicatorView(mustShow: Boolean) {
        binding.progressIndicatorView.isVisible = mustShow
        if (mustShow) {
            window.setFlags(
                WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE,
                WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE
            )
        } else {
            window.clearFlags(WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE)
        }
    }

    fun onDoneClick(view: MenuItem) {
        poolCreationFragment.save()
    }

}