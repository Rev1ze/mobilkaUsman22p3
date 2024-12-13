using Newtonsoft.Json;
using rmpAuthorization;
namespace WebApplicationAuthorization.models
{
    public class Getter
    { 
        static public string path = @"C:\sharpAuthoriz\rmpAuthorization\bin\Debug\Users.json";
        static public string pathProducts = @"C:\sharpAuthoriz\rmpAuthorization\bin\Debug\Items.json";
        static public List<User> getUsers()
        {
            return JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(path));
        }
        static public List<Product> getProducts()
        {
            return JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(pathProducts));

        }
    }
}
