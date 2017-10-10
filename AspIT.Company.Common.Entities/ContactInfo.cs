using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AspIT.Company.Common.Entities
{
    public struct ContactInfo
    {
        /// <summary>
        /// Home phone number
        /// </summary>
        private string homePhone;

        /// <summary>
        /// Initialises an instance of this struct
        /// </summary>
        /// <param name="homePhone">The home phone number</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when homePhone is greater than 24</exception>
        /// <exception cref="ArgumentException">Thrown when homePhone is empty, null, has special characters, or letters</exception>
        public ContactInfo(string homePhone) : this()
        {
            HomePhone = homePhone;
        }

        #region Properties
        /// <summary>
        /// Gets or sets the home phone
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is greater than 24</exception>
        /// <exception cref="ArgumentException">Thrown when the value is empty, null, has special characters, or letters</exception>
        public string HomePhone
        {
            get => homePhone;
            set
            {
                if(!string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, "^[ 0-9()-]+$"))
                {
                    if(value.Length <= 24)
                    {
                        homePhone = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Checks the specified phone number, to see if it is valid.
        /// </summary>
        /// <param name="phoneNumber">The phone number you want to validate</param>
        /// <returns>A boolean telling whether it is valid or not, and a string error message</returns>
        public static (bool, string) IsValidPhone(string phoneNumber)
        {
            if(phoneNumber == string.Empty)
            {
                return (false, "Nummeret må ikke være tomt.");
            }

            // Check for non-numbers
            if(!Regex.IsMatch(phoneNumber, "^[ 0-9()-]+$"))
            {
                return (false, "Nummeret må kun indholde tal, (), og -");
            }

            return (true, string.Empty);
        }
        #endregion
    }
}
