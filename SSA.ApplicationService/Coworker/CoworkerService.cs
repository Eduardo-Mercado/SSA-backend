using SSA.ApplicationService.DTO;
using SSA.Core.Coworkers;
using SSA.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSA.ApplicationService.Coworkers
{
    public class CoworkerService
    {
        private Coworker coworker;

        public bool Add(ref CoworkerDTO data)
        {
            coworker = new Coworker
            {
                Email = data.Email,
                FullName = data.FullName,
                Id = 0,
                Position = data.Position,
                ProfilePicture = data.ProfilePicture,
                Status = (Core.EnumStatus)1
            };
            if (new CoworkerRepository().Add(ref coworker))
            {
                data.Id = coworker.Id;
                return true;
            }
            return false;
        }

        public Boolean GetById(int id, ref CoworkerDTO temp)
        {
            var record = new CoworkerRepository().GetCoworkerById(id);
            temp.Email = record.Email;
            temp.FullName = record.FullName;
            temp.Id = record.Id;
            temp.Position = record.Position;
            temp.ProfilePicture = record.ProfilePicture;
            
            return true;
        }

        public List<CoworkerDTO> GetAll()
        {
            return new CoworkerRepository().GetAll()
                .Select(y => new CoworkerDTO
                {
                    Email = y.Email,
                    FullName = y.FullName,
                    Id = y.Id,
                    Position = y.Position,
                    ProfilePicture = y.ProfilePicture
                }).ToList();
        }

        public bool Delete(int id)
        {
            return new CoworkerRepository().Delete(id);
        }

        public bool Update(CoworkerDTO record)
        {
            coworker = new Coworker
            {
                Id = record.Id,
                Email = record.Email,
                FullName = record.FullName,
                Position = record.Position,
                ProfilePicture = record.ProfilePicture
            };

            return new CoworkerRepository().Update(coworker);
        }
    }
}
