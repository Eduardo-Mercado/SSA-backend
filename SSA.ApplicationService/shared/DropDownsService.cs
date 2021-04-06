using SSA.ApplicationService.DTO;
using SSA.Core.Seed;
using SSA.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSA.ApplicationService.shared
{
    public class DropDownsService
    {
        public List<DropDown> GetListCoworker()
        {
            return new DropDownRepository().CoworkerList();
        }

        public List<DropDown> GetListRolCoworkerProject()
        {
            return new DropDownRepository().RolCoworkerProject();
        }

        public object GetTeampProject(int idProject)
        {
            return new DropDownRepository().TeamByProject(idProject);
        }

        public object GetCatalogStatusAndCategories()
        {
            return new DropDownRepository().GetCatalogStatusAndCategories();
        }

        public object GetCategoryActivities()
        {
            return new DropDownRepository().CategoryActivity();
        }
        
        public object GetCategoryTasks()
        {
            return new DropDownRepository().CategoryTask();
        }

        public List<DropDown> GetListRolApp()
        {
            return new DropDownRepository().RolApp();
        }
    }
}
