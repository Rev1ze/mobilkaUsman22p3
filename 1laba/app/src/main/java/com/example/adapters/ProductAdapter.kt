package com.example.adapters

import android.content.Intent
import android.net.Uri
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.annotation.NonNull
import androidx.recyclerview.widget.RecyclerView
import androidx.recyclerview.widget.RecyclerView.LayoutManager
import com.bumptech.glide.Glide
import com.example.Product
import com.example.a1laba.R
import java.net.URL

open class ProductAdapter : RecyclerView.Adapter<ProductAdapter.ProductViewHolder>  {

    var products:List<Product>
    var deleteListener: OnProductDeleteListener
    constructor(products: List<Product>, onproductDeleteListener: OnProductDeleteListener)
    {
        this.products = products
        deleteListener= onproductDeleteListener

    }

    companion object class ProductViewHolder : RecyclerView.ViewHolder {

         var titleTextView: TextView ; var priceTextView: TextView ; var Image: ImageView
        constructor(@NonNull view : View) : super(view) {
            titleTextView = itemView.findViewById(R.id.tv_title)
            priceTextView = view.findViewById(R.id.tv_price)
            Image = view.findViewById(R.id.image)

        }

    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ProductViewHolder {
        val view: View =
            LayoutInflater.from(parent.context).inflate(R.layout.product_activity, parent, false)
        return ProductViewHolder(view)
    }






















































































    override fun getItemCount(): Int {
        return products.size
    }

    override fun onBindViewHolder(holder: ProductViewHolder, position: Int) {
        val note: Product = products[position]
        holder.titleTextView.setText("Товар: " + note.name)
        holder.priceTextView.setText("Цена: "+note.price)
//        var imageUri = note.pathToPng
//        Glide.with(holder.itemView.context)
//            .load(imageUri)
//            .into(holder.Image)

//        holder.itemView.setOnClickListener { view ->
//            val intent = Intent(
//                view.context,
//                ViewNoteActivity::class.java
//            )
//            intent.putExtra("note", note)
//            view.context.startActivity(intent)
//        }
    }
    interface OnProductDeleteListener {
        fun onProductDelete(product: Product)
    }
}