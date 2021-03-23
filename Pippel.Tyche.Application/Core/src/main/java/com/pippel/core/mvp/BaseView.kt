package com.pippel.core.mvp

interface BaseView<TPresenter> {

    fun initPresenter(presenter: TPresenter)

}