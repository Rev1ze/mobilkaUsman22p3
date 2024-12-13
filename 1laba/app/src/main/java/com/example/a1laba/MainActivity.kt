package com.example.a1laba

import User
import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.EditText
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.github.kittinunf.fuel.httpPost
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import org.json.JSONObject

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
    }
    fun regClick(view: View) {
        val registerActivity = Intent(this, Registration::class.java)
        startActivity(registerActivity)
    }

    fun loginClick(view: View) {
        val emailField = view.rootView.findViewById<EditText>(R.id.editTextLogin)
        val passwordField = view.rootView.findViewById<EditText>(R.id.editTextPassword)

        val email = emailField.text.toString()
        val password = passwordField.text.toString()

        val jsonBody = JSONObject()
            .put("email", email)
            .put("password", password)
            .toString()

        val url = "http://10.0.2.2:5182/api/User/Authorization2"
        url.httpPost()
            .header("Content-Type" to "application/json")
            .body(jsonBody)
            .response { request, response, result ->
                result.fold(
                    success = { data->
                        runOnUiThread {
                            Toast.makeText(this, "Успешная авторизация", Toast.LENGTH_SHORT).show()
                        }
                        val jsonRespone = String(data)
                        val user = Gson().fromJson<User>(jsonRespone,object: TypeToken<User>(){}.type)

                        val placeHolderActivity = Intent(this@MainActivity, MainSite::class.java)
                        placeHolderActivity.putExtra("email", email)
                        placeHolderActivity.putExtra("user", Gson().toJson(user))

                            startActivity(placeHolderActivity)
                    },
                    failure = { error ->
                        runOnUiThread {
                            Toast.makeText(this, "Ошибка ${error.message}",
                                Toast.LENGTH_SHORT).show()
                        }
                    }
                )
            }
    }
}