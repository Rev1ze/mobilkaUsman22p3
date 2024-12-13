package com.example.a1laba

import android.app.DatePickerDialog
import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.DatePicker
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.github.kittinunf.fuel.httpPost
import org.json.JSONObject
import java.util.Calendar
import java.util.Date

class Registration : AppCompatActivity() {
    var d: DatePickerDialog? = null
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_registration)
    val etBirthDate = findViewById<EditText>(R.id.etBirthDate)
    etBirthDate.setOnClickListener { v: View? ->
        val calendar: Calendar = Calendar.getInstance()
        val year: Int = calendar.get(Calendar.YEAR)
        val month: Int = calendar.get(Calendar.MONTH)
        val day: Int = calendar.get(Calendar.DAY_OF_MONTH)

        val datePickerDialog = DatePickerDialog(
            this,
            { view: DatePicker?, year1: Int, month1: Int, dayOfMonth: Int ->
                etBirthDate.setText(year1.toString() + "-" + (month1 + 1) + "-" + dayOfMonth.toString())

            }, year, month, day
        )
        datePickerDialog.show()
        d = datePickerDialog
    }

}

fun registerClicked(view: View) {
    val fullnameField = view.rootView.findViewById<EditText>(R.id.etFullName)
    val birthdayField = view.rootView.findViewById<EditText>(R.id.etBirthDate)
    val emailField = view.rootView.findViewById<EditText>(R.id.etEmail)
    val passwordField = view.rootView.findViewById<EditText>(R.id.etPassword)

    val fullname = fullnameField.text.toString()
    val name = fullname.split(' ')[1]
    val Surname = fullname.split(' ')[0]
    val Midname = fullname.split(' ')[2]
    val lst = birthdayField.text.split('-');
    val birth = Date.UTC(lst[0].toInt(), lst[1].toInt(), lst[2].toInt(),0,0,0)
    val birth2 = birthdayField.text

    val email = emailField.text.toString()
    val password = passwordField.text.toString()

    val jsonBody = JSONObject()
        .put("id", 0)
        .put("Name", name)
        .put("Surname", Surname)
        .put("MidName", Midname)
        .put("Birthday", birth2)
        .put("Email", email)
        .put("UserName", email)
        .put("Password", password)
        .put("IsBlocked", false)
        .toString()

    val url = "http://10.0.2.2:5182/api/User/Registration2"
    url.httpPost()
        .header("Content-Type" to "application/json")
        .body(jsonBody)
        .response { request, response, result ->
            result.fold(
                success = {
                    runOnUiThread {
                        Toast.makeText(this, "Успешная регистрация", Toast.LENGTH_SHORT).show()
                    }
                    val mainActivity = Intent(this, MainActivity::class.java)
                    startActivity(mainActivity)
                },
                failure = { error ->
                    runOnUiThread {
                        Toast.makeText(this, "Ошибка ${error.response.headers }", Toast.LENGTH_LONG).show()
                    }
                }
            )
        }
}

fun backClicked(view: View) {
    val registerActivity = Intent(this, MainActivity::class.java)
    startActivity(registerActivity)
}
}