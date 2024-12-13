package com.example.a1laba

import User
import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.GridLayout
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import androidx.core.view.children
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import androidx.recyclerview.widget.RecyclerView.LayoutManager
import com.example.Product
import com.example.adapters.ProductAdapter
import com.github.kittinunf.fuel.httpGet
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import java.io.File

class MainSite : AppCompatActivity(), ProductAdapter.OnProductDeleteListener {
    var email: String = ""
    lateinit var productsAdapter: ProductAdapter

    lateinit var user: String
    lateinit var stackpanel: RecyclerView
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main_site)
        user = intent.getStringExtra("user").toString()
        stackpanel = findViewById(R.id.stackPanel)
        stackpanel.layoutManager= LinearLayoutManager(this)
        load_data()

    }
    fun yourLoginClick(view: View){
        var intent = Intent(this@MainSite, YourProfile::class.java)
        intent.putExtra("user",user)
        startActivity(intent)
    }
    fun load_data(){
        val url = "http://10.0.2.2:5182/api/Products/GetItems"
        url.httpGet()
            .response { _,_,result ->
                result.fold(
                    success = { data ->
                        val jsonResponse = String(data)
                        val Products = Gson().fromJson<List<Product>>(jsonResponse,
                            object: TypeToken<List<Product>>() {}.type)
                        runOnUiThread{
                            productsAdapter = ProductAdapter(Products, this)
                            stackpanel.setAdapter(productsAdapter)
                        }


//                        for (item in Products){
//                            if (item.id != -1){
//                            }
//                        }
                    },
                    failure = { e ->

                    }
                )

            }
    }

    override fun onProductDelete(product: Product) {
        TODO("Not yet implemented")
    }

}