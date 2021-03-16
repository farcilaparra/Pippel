package com.pippel.tyche.mypool

import javax.inject.Inject

class DefaultMyPoolPresenter @Inject constructor(var view: MyPoolContract.View) :
    MyPoolContract.Presenter {

    override fun init() {
        view.display(MyPoolModelReaderInMemory())
    }

    override fun onDestroy() {
    }

}
