using SSA.Core.Coworkers;
using SSA.Infrastructure.EF;
using SSA.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CoworkerEF = SSA.Infrastructure.EF.Coworker;

namespace SSA.Infrastructure.Repository
{
    public class CoworkerRepository
    {
        private readonly SSAContext context;

        public CoworkerRepository()
        {
            context = new SSAContext();
        }

        /// <summary>
        /// Get all record with RecordStatus = 1
        /// </summary>
        /// <returns></returns>
        public List<Core.Coworkers.Coworker> GetAll()
        {
            try
            {
                return context.Coworker
                       .Where(x => x.RecordStatus == true)
                       .Select(y => new Core.Coworkers.Coworker
                       {
                           Email = y.Email,
                           FullName = y.FullName,
                           Id = y.IdCoworker,
                           Position = y.Position,
                           Status = (Core.EnumStatus)y.Status,
                           ProfilePicture = y.ProfilePicture
                       }).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Core.Coworkers.Coworker GetCoworkerById(int id)
        {
            if (id > 0)
            {
                return context.Coworker.Where(x => x.IdCoworker == id).Select(y => new Core.Coworkers.Coworker
                {
                    Email = y.Email,
                    FullName = y.FullName,
                    Id = id,
                    Position = y.Position,
                    Status = (Core.EnumStatus)y.Status,
                    ProfilePicture = y.ProfilePicture
                }).FirstOrDefault();
            }

            throw new Exception("The parameter values is not a valid Id");
        }
        /// <summary>
        /// Search for Coworker by Id of relation in ProjectCoworker
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Core.Coworkers.Coworker GetCoworkerByIdProjectCoworker(int id)
        {
            if (id > 0)
            {

                return context.ProjectCoworker.Where(x => x.IdProjectCoworker == id).Select(y => new Core.Coworkers.Coworker
                {
                    Email = y.IdCoworkerNavigation.Email,
                    FullName = y.IdCoworkerNavigation.FullName,
                    Id = y.IdProjectCoworker,
                    Position = y.IdCoworkerNavigation.Position,
                    Status = (Core.EnumStatus)y.IdCoworkerNavigation.Status,
                    ProfilePicture = y.IdCoworkerNavigation.ProfilePicture
                }).FirstOrDefault();
            }

            throw new Exception("The parameter values is not a valid Id");
        }

        public bool Add(ref Core.Coworkers.Coworker record)
        {
            try
            {
                CoworkerEF temp = new CoworkerEF();
                temp.RecordStatus = true;
                temp.RecordDate = DateTime.Now;
                temp.Position = record.Position;
                temp.FullName = record.FullName;
                temp.Email = record.Email;
                temp.Phone = "99999999";
                temp.ProfilePicture = record.ProfilePicture;
                temp.IdUserCreated = 1; // temporal value.

                context.Coworker.Add(temp);
                context.SaveChanges();
                record.Id = temp.IdCoworker;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Core.Coworkers.Coworker record)
        {
            try
            {
                CoworkerEF temp = context.Coworker.Where(x => x.IdCoworker == record.Id).FirstOrDefault();
                temp.RecordStatus = true;
                temp.Position = record.Position;
                temp.FullName = record.FullName;
                temp.Email = record.Email;
                temp.RecordUpdated = DateTime.Now;
                temp.ProfilePicture = record.ProfilePicture;
                temp.IdUserUpdated = 1; // temporal value.
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                CoworkerEF temp = context.Coworker.Where(x => x.IdCoworker == id).FirstOrDefault();
                temp.RecordStatus = false;
                temp.RecordUpdated = DateTime.Now;
                temp.IdUserUpdated = 1; // temporal value.
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
