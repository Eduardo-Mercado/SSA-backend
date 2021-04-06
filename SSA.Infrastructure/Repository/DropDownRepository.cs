using SSA.Core.Seed;
using SSA.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSA.Infrastructure.Repository
{
    public class DropDownRepository
    {
        private readonly SSAContext context;

        public DropDownRepository()
        {
            context = new SSAContext();
        }

        public List<DropDown> CoworkerList()
        {
            try
            {
                return context.Coworker.Where(x => x.RecordStatus == true).Select(y => new DropDown
                {
                    Id = y.IdCoworker,
                    Value = y.FullName + ' ' + y.Position
                }).ToList();
            }
            catch (Exception ex)
            {
                return new List<DropDown>();
            }
        }

        public object GetCatalogStatusAndCategories()
        {
            var listStatus = context.Status.Where(x => x.RecordStatus).Select(y => new
            {
                Id = y.IdStatus,
                Name = y.Name,
                Color = y.Color
            });

            var categories = context.Category.Where(x => x.RecordStatus).Select(y => new
            {
                Id = y.IdCategory,
                Name = y.Name,
                Color = y.Color
            });

            return listStatus.Union(categories).ToList();
        }

        public List<DropDown> RolApp()
        {
            try
            {
                return context.Role.Where(x => x.RecordStatu)
                                .Select(y => new DropDown
                                {
                                    Id = y.IdRole,
                                    Value = y.Name
                                }).ToList();
            }
            catch (Exception ex)
            {
                return new List<DropDown>();
            }
        }

        public object TeamByProject(int idProject)
        {
            try
            {
                return context.ProjectCoworker.Join(context.Coworker, pc => pc.IdCoworker, c => c.IdCoworker, (pc, c) => new { PC = pc, C = c })
                                .Where(x => x.PC.IdProject == idProject && x.PC.RecordStatus && x.C.RecordStatus)
                                .Select(y => new DropDown
                                {
                                    Id = y.PC.IdProjectCoworker,
                                    Value = y.C.FullName + ' ' + y.C.Position
                                }).ToList();
            }
            catch (Exception ex)
            {
                return new List<DropDown>();
            }
        }

        /// <summary>
        /// return the id of the relation Coworker vs Project  and the name + position of each coworker.
        /// </summary>
        /// <param name="idProject"></param>
        /// <returns></returns>
        public List<DropDown> CoworkerListByProject(int idProject)
        {
            try
            {
                return context.Coworker
                    .Join(context.ProjectCoworker, c => c.IdCoworker, pc => pc.IdCoworker, (c, pc) => new { C = c, PC = pc })
                    .Where(x => x.C.RecordStatus == true && x.PC.IdProject == idProject).Select(y => new DropDown
                    {
                        Id = y.C.IdCoworker,
                        Value = y.C.FullName + ' ' + y.C.Position
                    }).ToList();
            }
            catch (Exception ex)
            {
                return new List<DropDown>();
            }
        }

        public List<DropDown> RolCoworkerProject()
        {
            try
            {
                return context.RolCoworkerProject.Where(x => x.RecordStatus).Select(y => new DropDown
                {
                    Id = y.IdRolCoworkerProject,
                    Value = y.Name
                }).ToList();
            }
            catch (Exception ex)
            {

                return new List<DropDown>();
            }
        }

        public List<DropDown> CategoryActivity()
        {
            try
            {
                return context.Category.Where(x => x.RecordStatus && x.CategoryType == "Activity").Select(y => new DropDown
                {
                    Id = y.IdCategory,
                    Value = y.Name
                }).ToList();
            }
            catch (Exception ex)
            {

                return new List<DropDown>();
            }
        }

        public List<DropDown> CategoryTask()
        {
            try
            {
                return context.Category.Where(x => x.RecordStatus && x.CategoryType == "Task").Select(y => new DropDown
                {
                    Id = y.IdCategory,
                    Value = y.Name
                }).ToList();
            }
            catch (Exception ex)
            {

                return new List<DropDown>();
            }
        }

        public List<DropDown> Status()
        {
            try
            {
                return context.Status.Where(x => x.RecordStatus).Select(y => new DropDown
                {
                    Id = y.IdStatus,
                    Value = y.Name
                }).ToList();
            }
            catch (Exception ex)
            {

                return new List<DropDown>();
            }
        }
    }
}
