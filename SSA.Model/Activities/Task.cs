using System;
using System.Collections.Generic;
using System.Text;

namespace SSA.Core.Activities
{
    public class Task
    {
        public int IdTask { get; set; }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (ValidationString.IsValidFormat(value, TypeValidation.NameThing) && this.Status != EnumStatus.Authorized)
                {
                    _Title = value;
                }
            }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set
            {
                if (this.Status != EnumStatus.Authorized)
                    _Description = value;
            }
        }

        private int _IdCategory;

        public int IdCategory
        {
            get { return _IdCategory; }
            set { _IdCategory = value; }
        }


        private float _ProgressPercentage;

        public float ProgressPercentage
        {
            get { return _ProgressPercentage; }
            set
            {
                if (this.Status != EnumStatus.Authorized)
                    _ProgressPercentage = value;
            }
        }

        public TimeSpan AmountTime { get; set; }

        private DateTime _DateTimeStart;

        public DateTime DateTimeStart
        {
            get { return _DateTimeStart; }
            set
            {
                if (this.Status != EnumStatus.Authorized)
                    _DateTimeStart = value;
            }
        }

        private DateTime _DateTimeEnd { get; set; }
        public DateTime DateTimeEnd
        {
            get { return _DateTimeEnd; }
            set
            {
                if (value > DateTimeStart && this.Status != EnumStatus.Authorized)
                {
                    _DateTimeEnd = value;
                }
                else
                {
                    throw new Exception("DateTimeEnd must be greater than DatetimeStart");
                }
            }
        }

        public EnumStatus Status { get; set; }

        public Task(int id, string title, string descripcion, int idCategory, DateTime start, DateTime end, TimeSpan amountTime, float progress, EnumStatus status = EnumStatus.PendingApproval)
        {
            this.Status = status;
            this.IdTask = id;
            this.DateTimeStart = start;
            this.DateTimeEnd = end;
            this.Title = title;
            this.Description = descripcion;
            this.IdCategory = idCategory;
            this.ProgressPercentage = progress;
            this.AmountTime = amountTime;
            //this.Status = EnumStatus.Initialized;
        }
    }
}
