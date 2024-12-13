package com.example

data class Product(var id: Int = 100, var name: String, var category: String,
                   var price: Double, var rating: Double, var pathToPng: String)
{
    fun generateId(){// $$$$$
        this.id = 100
    }
}

