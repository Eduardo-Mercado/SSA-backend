using System;
using System.Collections.Generic;
using System.Text;
using SSA.Core.Coworkers;

namespace SSA.Core.Projects
{
    public class Project
    {
        public int Id { get; set; }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            private set
            {
                if (ValidationString.IsValidFormat(value, TypeValidation.NameThing))
                {
                    _Name = value;
                }
            }
        }

        public string Description { get; set; }

        public EnumStatus Status { get; private set; }

        public DateTime StartDate { get; set; }

        private DateTime? _FinishDate;

        public DateTime? FinishDate
        {
            get { return _FinishDate; }
            set
            {
                if (!(value >= this.StartDate))
                    throw new ArgumentException("Invalid, Value must be iqueal or greater than Creation Date");

                _FinishDate = value;
            }
        }

        public List<Coworker> TeamMembers { get; private set; }


        public Project()
        {
            this.TeamMembers = new List<Coworker>();
            this.Status = EnumStatus.Initialized;
        }

        /// <summary>
        /// ChangeStatus change the status of the project and , if the status is completed then, the completed Date is set to actual date.
        /// </summary>
        /// <param name="status"></param>
        public void ChangeStatus(EnumStatus status)
        {
            if (this.Status.Equals(EnumStatus.Completed))
            {
                throw new Exception("Invalid Operation, It's no allow to change status of a completed project");
            }

            this.Status = status;
            if (this.Status == EnumStatus.Completed)
            {
                this.FinishDate = DateTime.Now;
            }
        }

        /// <summary>
        /// Name must to be unique on all over the existents projects.
        /// </summary>
        /// <param name="name"></param>
        public void ChangeName(string name, List<string> Names)
        {
            if (Names.Contains(name))
            {
                throw new Exception("Invalid Operation,It's no allow to repite name with others projects");
            }

            this.Name = name;
        }

        public void AddTeamMember(Coworker _coworker)
        {
            if (_coworker.Status == EnumStatus.Active)
            {
                TeamMembers.Add(_coworker);
            }
            else
            {
                throw new Exception("Invalid asignation of coworker, coworker must be active, current status :" + _coworker.Status);
            }
        }

        public void RemoveTeamMember(int Id)
        {
            var coworker = TeamMembers.Find(x => x.Id == Id);
            this.TeamMembers.Remove(coworker);
        }

       
    }
}
