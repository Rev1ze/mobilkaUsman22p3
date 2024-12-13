data class User(
    var id: Int = 100,
    var userName: String,
    var password: String,
    var name: String,
    var surname: String,
    var midname: String,
    var email: String,
    var phone: String,
    var birthday: String,
    var isBlocked: Boolean,
) {

    fun defactorFullName(): String {
        return "$surname $name $midname"
    }
}