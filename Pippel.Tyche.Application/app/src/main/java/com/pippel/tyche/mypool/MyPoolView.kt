package com.pippel.tyche.mypool

import android.content.Context
import android.util.AttributeSet
import android.view.LayoutInflater
import android.widget.LinearLayout
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.pippel.core.ModelReader
import com.pippel.tyche.R
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class MyPoolView(context: Context, attrs: AttributeSet?) : LinearLayout(context, attrs),
    MyPoolContract.View {

    constructor(context: Context) : this(context, null)

    private lateinit var presenter: MyPoolContract.Presenter

    @Inject
    lateinit var myPoolDataReader: ModelReader<MyPoolModel>

    init {
        inflate()
        initPresenter(DefaultMyPoolPresenter(this))
        initComponents()
    }

    private fun inflate() {
        val inflater = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater
        inflater.inflate(R.layout.my_pool_layout, this, true)
    }

    private fun initComponents() {
        presenter.init()
    }

    override fun display(modelReader: ModelReader<MyPoolModel>) {
        val dataView = findViewById<RecyclerView>(R.id.dataView)
        dataView.layoutManager = LinearLayoutManager(context)
        dataView.adapter = MyPoolAdapter(myPoolDataReader)
    }

    override fun initPresenter(presenter: MyPoolContract.Presenter) {
        this.presenter = presenter
    }

}
