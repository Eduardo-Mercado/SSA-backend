//using Newtonsoft.Json;

namespace SSA.Security.DTO
{
    public class UserDTO
    {
        public int IdCoworker { get; set; }
        public int IdRol { get; set; }
        public string UserName { get; set; }
        //[JsonIgnore]
        public string Password { get; set; }
        public int Id { get; internal set; }
    }

    public class UserIndexDTO
    {
        public int Id { get; set; }
        public string NameCoworker { get; set; }
        public string Rol { get; set; }
        public string UserName { get; set; }
    }
}

//The [JsonIgnore] attribute prevents the password property from being serialized and returned in api responses.