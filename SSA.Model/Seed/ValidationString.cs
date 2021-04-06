using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SSA.Core
{

    public enum TypeValidation
    {
        NamePerson,
        Email,
        Phone,
        NameThing
    }

    public static class ValidationString
    {
        public static bool IsValidFormat(string args, TypeValidation type)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                throw new Exception("Must support data");
            }

            Regex pattern = GetValidationRegex(type);

            if (!pattern.IsMatch(args))
            {
                throw new Exception(string.Format("Data doesn't match with pattern : {0}", type));
            }

            return true;
        }

        private static Regex GetValidationRegex(TypeValidation type)
        {
            Regex pattern = new Regex(@"");
            switch (type)
            {
                case TypeValidation.NamePerson:
                    pattern = new Regex(@"^[a-zA-Z '.-]*$");
                    break;
                case TypeValidation.Email:
                    break;
                case TypeValidation.Phone:
                    break;
                case TypeValidation.NameThing:
                    pattern = new Regex(@"^[a-zA-Z '.-]*$");
                    break;
            }

            return pattern;
        }
    }


}
