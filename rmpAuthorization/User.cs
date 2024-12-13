using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace rmpAuthorization
{
    public class User
    {
        public int Id { get; set; } = 100;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MidName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Pathtopn { get; set; }
        public string Birthday { get; set; }
        public bool IsBlocked {  get; set; }
        public User(string username, string password, string name, string surname, string midname, string email,string phone,string birthday, bool IsBlocked) 
        {
            this.Id = 100;
            this.UserName = username;
            this.Password = password;
            this.Name = name;
            this.Surname = surname;
            this.MidName = midname;
            this.Email = email;
            this.Birthday = birthday;
            this.IsBlocked = IsBlocked;
            this.Phone = phone;
        }

        public bool isValidEmail()
        {
            Regex g = new Regex("^([a-zA-Z0-9]|\\d)+@[a-zA-z]+.[a-zA-z]+$");
            if (g.IsMatch(Email) == false || Email.Contains("..") || Email.StartsWith(".")
             || !(Email.Contains("@")) || (Email.Split('@')[0].Any(x => char.IsLetter(x)) == false)
             || !Email.All(x => x == '.' || char.IsLetterOrDigit(x) || x == '@'))
            {
                return true;
            }
            return false;
        }
        public string defactorFullName()
        {
            return this.Surname + " " + this.Name + " " + this.MidName;
        }

        public override string ToString()
        {
            return $" email={Email}, username={UserName}, id={Id}";
        }
    }
    public class Product
    {
        public int id;
        public string name { get; set; }
        public string category {  get; set; }
        public double price { get; set; }
        public double rating { get; set; }
        
        public string pathToPng { get; set; }
        public Product(string name, string category, double price, double rating, string pathToPng)
        {
            this.id = 100;
            this.name = name;
            this.category = category;
            this.price = price;
            this.rating = rating;
            this.pathToPng = pathToPng;
        }
        public void setUniqueId()
        {
            List<Product> proudctAll = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText("C:\\sharpAuthoriz\\rmpAuthorization\\bin\\Debug\\items.json"))?? new List<Product>();
            int id_products = proudctAll.Max(i => i.id);
            this.id = id_products + 1;
        }

    }
}
