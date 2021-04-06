using SSA.Security.DTO;
using SSA.Security.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSA.Security.Security.Autentication
{
    public class AuthenticatonAction : IAutenticatonAction
    {
        private readonly SSASecurityContext context;

        public AuthenticatonAction()
        {
            context = new SSASecurityContext();
        }
        public UserIndexDTO CreateAccess(UserDTO userData)
        {
            if (IsUniqueUser(userData.UserName))
            {
                try
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(userData.Password);
                    UserIndexDTO userIndexDTO = new UserIndexDTO();
                    Account temp = new Account();
                    temp.IdCoworker = userData.IdCoworker;
                    temp.IdRol = userData.IdRol;
                    temp.Passwd = passwordHash;
                    temp.RecordDate = DateTime.Now;
                    temp.RecordStatus = true;
                    temp.UserName = userData.UserName;

                    context.Account.Add(temp);
                    context.SaveChanges();

                    userIndexDTO.UserName = userData.UserName;
                    userIndexDTO.Rol = context.Role.Find(userData.IdRol).Name;
                    userIndexDTO.NameCoworker = context.Coworker.Find(userData.IdCoworker).FullName;
                    userIndexDTO.Id = temp.IdUser;

                    return userIndexDTO;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            throw new Exception("");
        }

        public UserIndexDTO GetInfoUserByName(string userName)
        {
            UserIndexDTO userIndexDTO = new UserIndexDTO();
            var userdata = context.Account.Where(x => x.UserName == userName).FirstOrDefault();

            userIndexDTO.UserName = userdata.UserName;
            userIndexDTO.Rol = context.Role.Find(userdata.IdRol).Name;
            userIndexDTO.NameCoworker = context.Coworker.Find(userdata.IdCoworker).FullName;
            userIndexDTO.Id = userdata.IdUser;

            return userIndexDTO;
        }

        public List<UserIndexDTO> GetAllUsers()
        {

            return context.Account.Join(context.Role, u => u.IdRol, r => r.IdRole, (u, r) => new { u, r })
                 .Join(context.Coworker, data => data.u.IdCoworker, c => c.IdCoworker, (data, c) => new { data, c })
                .Where(x => x.data.r.RecordStatu && x.data.u.RecordStatus && x.c.RecordStatus)
                .Select(y => new UserIndexDTO
                {
                    Id = y.data.u.IdUser,
                    NameCoworker = y.c.FullName,
                    Rol = y.data.r.Name,
                    UserName = y.data.u.UserName
                }).ToList();
        }

        public UserDTO GetUserById(int id)
        {
            var info = context.Account.Find(id);

            return new UserDTO
            {
                Id = info.IdUser,
                IdCoworker = info.IdCoworker,
                IdRol = info.IdRol,
                Password = "",
                UserName = info.UserName
            };
        }
        private bool IsUniqueUser(string userName)
        {
            var allUser = context.Account.Select(y => y.UserName).ToList();
            return !allUser.Contains(userName);
        }

        public bool IsValidUserAndPasswordCombination(string userName, string password)
        {
            var user = context.Account.Where(x => x.RecordStatus && x.UserName == userName
            ).FirstOrDefault();

            return (user != null && BCrypt.Net.BCrypt.Verify(password, user.Passwd));
        }

        public bool IsValidAccess(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                //string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                if (IsValidUserAndPasswordCombination(userName, password))
                {
                    return true;
                }

                throw new System.NotImplementedException($" The user : {userName} and password : {password} wasn't found");
            }

            throw new System.NotImplementedException("User and Password is required");
        }

        public async System.Threading.Tasks.Task<List<MenuDTO>> GetMenuByUserNameAsync(string userName)
        {
            var userinfo = context.Account.Where(x => x.UserName == userName).Select(y => y).FirstOrDefault();
            List<MenuDTO> listOptions = new List<MenuDTO>();
            listOptions = await context.LoadStoredProc("security.loadOptionByRol")
                                 .WithSqlParam("Id", userinfo.IdRol)
                                 .ExecuteStoredProc<MenuDTO>();

            return listOptions;
        }

    }
}
