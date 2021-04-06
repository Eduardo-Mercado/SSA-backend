using System;
using System.Collections.Generic;
using System.Text;

namespace SSA.Core.Coworkers
{
    public class Coworker
    {
        public int Id { get;  set; }

        private string _FullName;
        public string FullName
        {
            get { return _FullName; }
            set
            {
                if (ValidationString.IsValidFormat(value, TypeValidation.NamePerson))
                    _FullName = value;
            }
        }

        public string Position { get; set; }

        public EnumStatus Status { get; set; }

        public string Email { get; set; }
        public string ProfilePicture { get; set; }

        public int IdRolInProject { get; set; }
                                           //  private string _Phone;

        //public string Phone
        //{
        //    get { return _Phone; }
        //    set
        //    {
        //        if (ValidationString.IsValidFormat(value, TypeValidation.Phone))
        //            _Phone = value;
        //    }
        //}

        public Coworker()
        {

        }
    }
}
