package com.pippel.tyche.mypool

import com.pippel.core.ModelReader
import com.pippel.core.mvp.BasePresenter
import com.pippel.core.mvp.BaseView

object MyPoolContract {

    interface Presenter : BasePresenter {

        fun init()

    }

    interface View : BaseView<Presenter> {

        fun display(modelReader: ModelReader<MyPoolModel>)

    }

}
