using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AspIT.Company.Common.Entities
{
    public class Person
    {
        #region Fields
        /// <summary>
        /// The first name of the person
        /// </summary>
        protected string firstName;
        /// <summary>
        /// The last name of the person
        /// </summary>
        protected string lastName;
        /// <summary>
        /// The birth date of the person
        /// </summary>
        protected DateTime birthDate;
        /// <summary>
        /// The address of the person
        /// </summary>
        protected string address;
        /// <summary>
        /// The city of the person
        /// </summary>
        protected string city;
        /// <summary>
        /// The region of the person
        /// </summary>
        protected string region;
        /// <summary>
        /// The postal code of the person
        /// </summary>
        protected string postalCode;
        /// <summary>
        /// The country the person lives in
        /// </summary>
        protected string country;
        /// <summary>
        /// The contact info of the person
        /// </summary>
        protected ContactInfo contactInfo;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        /// <param name="firstName">The person's firstname</param>
        /// <param name="lastName">The person's lastname</param>
        /// <param name="birthDate">The person's birthdate</param>
        /// <param name="address">The person's address</param>
        /// <param name="city">The city of the person</param>
        /// <param name="region">The region of the city</param>
        /// <param name="postalCode">The postal code of the city</param>
        /// <param name="country">The country</param>
        /// <param name="contactInfo">The person's contact informations</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when firstname, or postalCode is greater than 10. Thrown when lastName is greater than 20. Thrown when city, region, or country is greater than 15. Thrown when address is greater than 60</exception>
        /// <exception cref="ArgumentException">Thrown when firstName or lastname, address, city, region, postalCode, or country is empty, null, numbers, or has special characters</exception>
        public Person(string firstName, string lastName, DateTime birthDate, string address, string city, string region, string postalCode, string country, ContactInfo contactInfo)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Address = address;
            City = city;
            Region = region;
            PostalCode = postalCode;
            Country = country;
            ContactInfo = contactInfo;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is greater than 10</exception>
        /// <exception cref="ArgumentException">Thrown when the value is empty, null or has special characters, or numbers</exception>
        public string FirstName
        {
            get => firstName;
            set
            {
                if(!string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, "^[ A-Za-z]+$"))
                {
                    if(value.Length <= 10)
                    {
                        firstName = value;
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

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is greater than 20</exception>
        /// <exception cref="ArgumentException">Thrown when the value is empty, null or has special characters, or numbers</exception>
        public string LastName
        {
            get => lastName;
            set
            {
                if(!string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, "^[ A-Za-z]+$"))
                {
                    if(value.Length <= 20)
                    {
                        lastName = value;
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

        /// <summary>
        /// Gets or sets the birthdate
        /// </summary>
        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                // TODO: Validate, so you can't set birthdate to something higher than todays date
                /*
                if()
                {
                    throw new ArgumentException();
                }*/
            }
        }

        /// <summary>
        /// Gets or sets the address
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is greater than 60</exception>
        /// <exception cref="ArgumentException">Thrown when the value is empty, null or has special characters</exception>
        public string Address
        {
            get => address;
            set
            {
                if(!string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, @"^[\sA-Za-z0-9-.]+$"))
                {
                    if(value.Length <= 60)
                    {
                        address = value;
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

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is greater than 15</exception>
        /// <exception cref="ArgumentException">Thrown when the value is empty, null or has special characters, or numbers</exception>
        public string City
        {
            get => city;
            set
            {
                if(!string.IsNullOrEmpty(value) && Regex.IsMatch(value, "^[ A-Za-z]+$"))
                {
                    if(value.Length <= 15)
                    {
                        city = value;
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

        /// <summary>
        /// Gets or sets the region
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is greater than 15</exception>
        /// <exception cref="ArgumentException">Thrown when the value is empty or has special characters, or numbers</exception>
        public string Region
        {
            get => region;
            set
            {
                if(value != null)
                {
                    if(value == string.Empty || Regex.IsMatch(value, "^[ A-Za-z]+$"))
                    {
                        if(value.Length <= 15)
                        {
                            region = value;
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
        }

        /// <summary>
        /// Gets or sets the postal code
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is greater than 10</exception>
        /// <exception cref="ArgumentException">Thrown when the value is empty, null, or has special characters</exception>
        public string PostalCode
        {
            get => postalCode;
            set
            {
                if(!string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, "^[ A-Z0-9]+$"))
                {
                    if(value.Length <= 10)
                    {
                        postalCode = value;
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

        /// <summary>
        /// Gets or sets the country
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is greater than 15</exception>
        /// <exception cref="ArgumentException">Thrown when the value is empty, null, has special characters, or numbers</exception>
        public string Country
        {
            get => country;
            set
            {
                if(!string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, "^[ A-Za-z]+$"))
                {
                    if(value.Length <= 15)
                    {
                        country = value;
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

        /// <summary>
        /// Gets or sets the contact info of the person
        /// </summary>
        public ContactInfo ContactInfo { get => contactInfo; set => contactInfo = value; }

        /// <summary>
        /// Gets or sets the ID of the person
        /// </summary>
        public int Id { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Checks the specified name to see if it is valid.
        /// </summary>
        /// <param name="name">The name you want to validate</param>
        /// <returns>A boolean telling whether it is valid or not, and a string error message</returns>
        public static (bool, string) IsValidName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return (false, "Navnet må ikke være tomt.");
            }

            if(name.Contains('-'))
            {
                if(name.StartsWith("-") || name.EndsWith("-"))
                {
                    return (false, "Karakteren '-' må ikke være i starten eller slutningen af navnet.");
                }

                if(!char.IsUpper(name[name.IndexOf('-') + 1]))
                {
                    return (false, "Navnet efter '-' tegnet skal starte med stort.");
                }
            }
            if(!char.IsUpper(name[0]))
            {
                return (false, "Navn skal starte med stort.");
            }

            // Check for numbers
            for(int i = 0; i < name.Length; i++)
            {
                if(char.IsNumber(name[i]))
                {
                    return (false, "Der må ikke være tal i navnet.");
                }
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Checks the specified address to see if it is valid.
        /// </summary>
        /// <param name="address">The address you want to validate</param>
        /// <returns>A boolean telling whether it is valid or not, and a string error message</returns>
        public static (bool, string) IsValidAddress(string address)
        {
            if(string.IsNullOrEmpty(address))
            {
                return (false, "Addressen må ikke være tom.");
            }

            if(!Regex.IsMatch(address, @"^[\sA-Za-z0-9-.]+$"))
            {
                return (false, "Addressen må kun indholde bogstaver, tal, mellemrum og '-' tegnet");
            }
            return (true, string.Empty);
        }

        /// <summary>
        /// Checks the specified city to see if it is valid.
        /// </summary>
        /// <param name="city">The city you want to validate</param>
        /// <returns>A boolean telling whether it is valid or not, and a string error message</returns>
        public static (bool, string) IsValidCity(string city)
        {
            if(string.IsNullOrEmpty(city))
            {
                return (false, "By må ikke være tom.");
            }

            if(!Regex.IsMatch(city, "^[ A-Za-z]+$"))
            {
                return (false, "By må kun indholde bogstaver og mellemrum");
            }
            return (true, string.Empty);
        }

        /// <summary>
        /// Checks the specified region to see if it is valid.
        /// </summary>
        /// <param name="region">The region you want to validate</param>
        /// <returns>A boolean telling whether it is valid or not, and a string error message</returns>
        public static (bool, string) IsValidRegion(string region)
        {
            if(region != String.Empty && !Regex.IsMatch(region, @"^[ A-Za-z]+$"))
            {
                return (false, "Region må kun indholde bogstaver og mellemrum");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Checks the specified postal code to see if it is valid.
        /// </summary>
        /// <param name="postalCode">The postal code you want to validate</param>
        /// <returns>A boolean telling whether it is valid or not, and a string error message</returns>
        public static (bool, string) IsValidPostalCode(string postalCode)
        {
            if(string.IsNullOrEmpty(postalCode))
            {
                return (false, "Postnummeret må ikke være tom.");
            }

            if(!Regex.IsMatch(postalCode, "^[ 0-9A-Z]+$"))
            {
                return (false, "Postnummeret må kun indholde tal, mellemrum og bogstaver");
            }
            return (true, string.Empty);
        }

        /// <summary>
        /// Checks the specified country to see if it is valid.
        /// </summary>
        /// <param name="country">The country you want to validate</param>
        /// <returns>A boolean telling whether it is valid or not, and a string error message</returns>
        public static (bool, string) IsValidCountry(string country)
        {
            if(string.IsNullOrEmpty(country))
            {
                return (false, "Land må ikke være tom.");
            }

            if(!Regex.IsMatch(country, "^[ A-Za-z]+$"))
            {
                return (false, "Land må kun indholde bogstaver og mellemrum");
            }
            return (true, string.Empty);
        }
        #endregion
    }
}
