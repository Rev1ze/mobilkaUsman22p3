package com.example.a1laba

import android.os.Bundle
import android.widget.ImageView
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import com.bumptech.glide.Glide
import com.example.Product
import com.github.kittinunf.fuel.httpGet
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import java.util.Date

class ProductActivity : AppCompatActivity() {
    lateinit var tvTitle : TextView
    lateinit var imageView: ImageView
    lateinit var tvPrice:TextView
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.product_activity)
        tvTitle = findViewById(R.id.tv_title)
        tvPrice = findViewById(R.id.tv_price)
        imageView = findViewById(R.id.image)

        val product_id = intent.getIntExtra("product_id", -1)
        if (product_id != -1) {
            val url = "http://10.0.2.2:5182/api/Products/GetItems"
            url.httpGet()
                .response { _,_,result ->
                    result.fold(
                        success = { data ->
                            val jsonResponse = String(data)
                            val Products = Gson().fromJson<List<Product>>(jsonResponse,
                                object : TypeToken<List<Product>>() {}.type
                            )
                            val product: Product = Products.get(product_id)
                            tvTitle.setText(product.name)
                            tvPrice.setText(product.price.toString())
//                            val imageUri = product.pathToPng

//                            Glide.with(this)
//                                .load(imageUri)
//                                .into(imageView)

                        },
                        failure = { er->

                        })
                }
        }
    }


}