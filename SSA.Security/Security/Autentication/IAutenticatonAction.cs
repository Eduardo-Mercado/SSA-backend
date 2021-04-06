using SSA.Security.DTO;
using System.Collections.Generic;

namespace SSA.Security.Security.Autentication
{
    public interface IAutenticatonAction
    {
        bool IsValidUserAndPasswordCombination(string userName, string password);
        bool IsValidAccess(string userName, string Password);
        UserIndexDTO CreateAccess(UserDTO userData);
        UserDTO GetUserById(int id);
        UserIndexDTO GetInfoUserByName(string userName);
        List<UserIndexDTO> GetAllUsers();
        System.Threading.Tasks.Task<List<MenuDTO>> GetMenuByUserNameAsync(string userName);
    }
}
