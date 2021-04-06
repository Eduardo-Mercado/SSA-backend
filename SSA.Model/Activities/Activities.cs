using SSA.Core.Coworkers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSA.Core.Activities
{
    public class Activity
    {
        public int IdActivity { get; set; }

        public int IdProject { get; set; }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (ValidationString.IsValidFormat(value, TypeValidation.NameThing))
                    if (value.Length > 0 && value.Length <= 250)
                    {
                        _Name = value;
                    }
                    else
                    {
                        throw new Exception($"The length is {value.Length} , but exceed the max lenght of 250");
                    }
            }
        }

        public string Summary { get; set; }

        public Coworker AssignedCoworker { get; private set; }

        public Coworker CreatedBy { get; private set; }

        public int IdCategory { get; set; }
        public int IdPriority { get; set; }
        public DateTime AssignedDate { get; set; }
        private DateTime _CompletedDate;

        public float PercentCompleted { get; private set; }

        public EnumStatus Status { get; private set; }

        public DateTime CompletedDate
        {
            get { return _CompletedDate; }
            set
            {
                if (value < AssignedDate)
                {
                    throw new Exception(string.Format("Completed date : {0}  must be equal or greater than assigned date:{1}",
                        value.FormatStandarDate(), AssignedDate.FormatStandarDate()));
                }
                _CompletedDate = value;
            }
        }

        public List<Task> Tasks { get; private set; }

        /// <summary>
        /// Add new Task only if the status of the current activities is In Progress o Initialized
        /// </summary>
        /// <param name="newTask"></param>
        public void AddTask(Task newTask)
        {
            if (this.Status == EnumStatus.InPoggress || this.Status == EnumStatus.Initialized)
            {
                this.Tasks.Add(newTask);
                this.UpdatePercentCompleted(newTask.ProgressPercentage);
            }
            else
            {
                throw new Exception("Is not possible to add tasks to this activities");
            }
        }

        /// <summary>
        /// Remove task only if the status of the current activities is In Progress o Initialized
        /// </summary>
        /// <param name="id"></param>
        public void RemoveTask(int id)
        {
            if (this.Status == EnumStatus.InPoggress || this.Status == EnumStatus.Initialized)
            {
                Task temp = this.Tasks.Find(x => x.IdTask == id);
                this.Tasks.Remove(temp);
                this.UpdatePercentCompleted((-1 * temp.ProgressPercentage));
            }
            else
            {
                throw new Exception("Is not possible to remove tasks to this activities");
            }
        }

        public void UpdateTask(Task task)
        {
            if (task.IdTask <= 0)
                throw new Exception("Is not possible to update this task, because is not saved in database");

            var temp = this.Tasks.Find(x => x.IdTask == task.IdTask);
            temp.Title = task.Title;
            temp.Description = task.Description;
            temp.ProgressPercentage = task.ProgressPercentage;
            temp.IdCategory = task.IdCategory;
            temp.Status = task.Status;
            temp.DateTimeStart = task.DateTimeStart;
            temp.DateTimeEnd = task.DateTimeEnd;
            temp.AmountTime = task.AmountTime;
            this.UpdatePercentCompleted(task.ProgressPercentage);
        }

        public void SetAuthorizeStatus()
        {
            this.Status = EnumStatus.Authorized;
        }

        private void UpdatePercentCompleted(float percent)
        {
            this.PercentCompleted += percent;

            if (this.PercentCompleted >= 100)
            {
                this.CompletedDate = DateTime.Now;
                this.Status = EnumStatus.PendingApproval;
            }
            else
            {
                this.Status = EnumStatus.InPoggress;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coworker"></param>
        /// <param name="idCoworkerAllow"> list of all coworker who can participe in this project</param>
        public void SetAssignCoworker(Coworker coworker, List<int> idCoworkerAllow)
        {
            if (idCoworkerAllow.Contains(coworker.Id))
            {
                this.AssignedCoworker = coworker;
            }
            else
            {
                throw new Exception($"This Activity assignment is not allow, Because {coworker.FullName} is not part of this project");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coworker"></param>
        /// <param name="idCoworkerAllow"> list of all coworker who can participe in this project</param>
        public void SetCreatorActivity(Coworker coworker, List<int> idCoworkerAllow)
        {
            if (idCoworkerAllow.Contains(coworker.Id))
            {
                this.CreatedBy = coworker;
            }
            else
            {
                throw new Exception($"{coworker.FullName} can not create  Activities, Because  is not part of this project");
            }
        }


        public Activity()
        {
            this.Status = EnumStatus.Initialized;
            this.Tasks = new List<Task>();
        }
    }
}
