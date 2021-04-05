package com.pippel.tyche.mypools

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import androidx.paging.LoadState
import androidx.paging.PagingDataAdapter
import com.pippel.core.afterTextChangedDelayed
import com.pippel.core.coroutines.launchAsyncWhenCreated
import com.pippel.tyche.PagingLoadStateAdapter
import com.pippel.tyche.R
import com.pippel.tyche.VerticalDividerItemDecoration
import com.pippel.tyche.databinding.FragmentMyPoolsBinding
import com.pippel.tyche.mypools.data.MyPoolModel
import com.pippel.tyche.mypools.data.MyPoolsViewHolder
import com.pippel.tyche.showMessage
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.flow.collectLatest
import javax.inject.Inject

@AndroidEntryPoint
class MyPoolsFragment : Fragment() {

    @Inject
    lateinit var dataViewAdapter: PagingDataAdapter<MyPoolModel, MyPoolsViewHolder>

    private lateinit var binding: FragmentMyPoolsBinding

    private val viewModel: MyPoolsViewModel by viewModels()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentMyPoolsBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        initDataView()
        initSearchView()
    }

    private fun initDataView() {
        postponeEnterTransition()

        with(binding.dataView) {
            adapter = dataViewAdapter.withLoadStateHeaderAndFooter(
                PagingLoadStateAdapter(),
                PagingLoadStateAdapter()
            )
            viewTreeObserver.addOnPreDrawListener {
                startPostponedEnterTransition()
                true
            }
            addItemDecoration(VerticalDividerItemDecoration(context))
        }

        launchAsyncWhenCreated {
            viewModel.myPoolsFlow.collectLatest {
                dataViewAdapter.submitData(it)
            }
        }

        with(binding.dataSwipeRefreshView) {
            setOnRefreshListener {
                dataViewAdapter.refresh()
            }
        }

        with(dataViewAdapter) {
            launchAsyncWhenCreated {
                loadStateFlow.collectLatest {
                    binding.dataSwipeRefreshView.isRefreshing = false

                    if (it.refresh is LoadState.Error) {
                        notifyNetworkError()
                    }
                }
            }
        }
    }

    private fun initSearchView() {
        binding.searchTextEdit.afterTextChangedDelayed {
            viewModel.filterMyPools(it.trim())
        }
    }

    private fun notifyNetworkError() {
        showMessage(getString(R.string.network_error_message))
    }

}
