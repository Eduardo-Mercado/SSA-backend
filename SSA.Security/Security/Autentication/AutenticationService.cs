using SSA.Security.DTO;
using System.Collections.Generic;

namespace SSA.Security.Security.Autentication
{
    public class AutenticationService
    {
        private readonly IAutenticatonAction _IAutenticatonAction;
        public AutenticationService(IAutenticatonAction iAutenticatonAction)
        {
            this._IAutenticatonAction = iAutenticatonAction;
        }

        public List<UserIndexDTO> GetAllUsers()
        {
            return _IAutenticatonAction.GetAllUsers();
        }

        public UserDTO GetById(int userId)
        {
            return _IAutenticatonAction.GetUserById(userId);
        }

        public bool IsValidAccess(UserDTO model)
        {
            return _IAutenticatonAction.IsValidAccess(model.UserName, model.Password);
        }

        public UserIndexDTO GetInfoUserByName(string userName)
        {
            return _IAutenticatonAction.GetInfoUserByName(userName);
        }

        public UserIndexDTO CreateUser(UserDTO userData)
        {
            return _IAutenticatonAction.CreateAccess(userData);
        }

        public async System.Threading.Tasks.Task<List<MenuDTO>> GetMenuByUserNameAsync(string userName)
        {
            return await _IAutenticatonAction.GetMenuByUserNameAsync(userName);
        }
    }
}
