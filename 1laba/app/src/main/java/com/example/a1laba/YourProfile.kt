package com.example.a1laba

import User
import android.os.Bundle
import android.view.View
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.github.kittinunf.fuel.httpGet
import com.github.kittinunf.fuel.httpPut
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import org.json.JSONObject

class YourProfile : AppCompatActivity() {
    val id:Int = 0
    lateinit var etEmail: EditText
    lateinit var etFio: EditText
    lateinit var etNumber: EditText
    lateinit var etPassword: EditText
    lateinit var etBirth: EditText
    lateinit var user: User
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_your_profile)
        etEmail = findViewById<EditText>(R.id.etEmail)
        etFio = findViewById<EditText>(R.id.etFIO)
        etNumber = findViewById<EditText>(R.id.etNumber)
        etPassword = findViewById<EditText>(R.id.etPassword)
        etBirth = findViewById<EditText>(R.id.etBirthday)
         user = Gson().fromJson<User>(intent!!.getStringExtra("user").toString(),
             object : TypeToken<User>() {}.type)
        loadData()

    }
    fun loadData(){
        val url = "http://10.0.2.2:5182/api/User/${user.id}"
        url.httpGet()
            .response { _, _, result ->
                result.fold(
                    success = { data ->
                        val jsonResponse = String(data)
                        println(String(data))
                        val user = Gson().fromJson<User>(jsonResponse,
                            object : TypeToken<User>() {}.type)
                        etEmail.setText(user.email.toString())
                        etNumber.setText(user.phone .toString())
                        etPassword.setText(user.password.toString())
                        etFio.setText(user.defactorFullName().toString())
                        etBirth.setText(user.birthday.toString())
                    },
                    failure = { error ->

                    }
                )
            }
    }
    fun saveData(view: View){
        user.email = etEmail.text.toString()
        user.phone = etNumber.text.toString()
        user.password = etPassword.text.toString()
        user.birthday = etBirth.text.toString()
        user.surname = etFio.text.toString().split(' ')[0]
        user.name = etFio.text.toString().split(' ')[1]
        user.midname = etFio.text.toString().split(' ')[2]
        val jsonBody = Gson().toJson(user).toString()
        val url = "http://10.0.2.2:5182/api/User/EditUser?iduser=${user.id}"
        url.httpPut()
            .header("Content-Type" to "application/json")
            .body(jsonBody)
            .response { _,_,result->
                result.fold(
                    success = {data->
                        runOnUiThread {
                            Toast.makeText(this, "Успешно измененны данные", Toast.LENGTH_SHORT).show()
                        }
                    },
                    failure = { error->
                        runOnUiThread {
                            Toast.makeText(this, "${error.message}", Toast.LENGTH_SHORT).show()
                        }
                    }
                )

            }
    }

}