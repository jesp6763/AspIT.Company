using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AspIT.Company.Common.Entities
{
    /// <summary>
    /// Represents an employee
    /// </summary>
    public class Employee : Person
    {
        #region Fields
        /// <summary>
        /// The title of the employee
        /// </summary>
        private string title;
        /// <summary>
        /// The title of courtesy of the employee
        /// </summary>
        private string titleOfCourtesy;
        /// <summary>
        /// The hire date of the employee
        /// </summary>
        private DateTime hireDate;
        /// <summary>
        /// The extension of the employee
        /// </summary>
        private string extension;
        /// <summary>
        /// The photo of the employee
        /// </summary>
        private Uri photo;
        /// <summary>
        /// The notes of the employee
        /// </summary>
        private string notes;
        /// <summary>
        /// The id of the person to report to
        /// </summary>
        private int? reportsTo;
        /// <summary>
        /// The path to the photo
        /// </summary>
        private string photoPath;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        /// <param name="title">The title of the employee</param>
        /// <param name="hireDate">The date the employee got hired</param>
        /// <param name="extension">The extension the employee is in</param>
        /// <param name="reportsTo">The id of the person to report to</param>
        /// <param name="firstName">The person's firstname</param>
        /// <param name="lastName">The person's lastname</param>
        /// <param name="birthDate">The person's birthdate</param>
        /// <param name="address">The person's address</param>
        /// <param name="city">The city of the person</param>
        /// <param name="region">The region of the city</param>
        /// <param name="postalCode">The postal code of the city</param>
        /// <param name="country">The country</param>
        /// <param name="contactInfo">The person's contact informations</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when firstname, or postalCode is greater than 10.
        /// Thrown when lastName is greater than 20.
        /// Thrown when city, region, or country is greater than 15.
        /// Thrown when address is greater than 60. Thrown when title is greater than 30.
        /// Thrown when extension is greater than 4.</exception>
        /// <exception cref="ArgumentException">Thrown when title, extension, firstName, lastname, address, city, region, postalCode, or country is empty, null, numbers, or has special characters</exception>
        public Employee(string title, DateTime hireDate, string extension, int? reportsTo, string firstName, string lastName, DateTime birthDate, string address, string city, string region, string postalCode, string country, ContactInfo contactInfo) : base(firstName, lastName, birthDate, address, city, region, postalCode, country, contactInfo)
        {
            Title = title;
            HireDate = hireDate;
            Extension = extension;
            ReportsTo = reportsTo;
            PhotoPath = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        /// <param name="titleOfCourtesy">The title of courtesy of the person</param>
        /// <param name="title">The title of the employee</param>
        /// <param name="hireDate">The date the employee got hired</param>
        /// <param name="extension">The extension the employee is in</param>
        /// <param name="reportsTo">The id of the person to report to</param>
        /// <param name="firstName">The person's firstname</param>
        /// <param name="lastName">The person's lastname</param>
        /// <param name="birthDate">The person's birthdate</param>
        /// <param name="address">The person's address</param>
        /// <param name="city">The city of the person</param>
        /// <param name="region">The region of the city</param>
        /// <param name="postalCode">The postal code of the city</param>
        /// <param name="country">The country</param>
        /// <param name="contactInfo">The person's contact informations</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when firstname, or postalCode is greater than 10.
        /// Thrown when lastName is greater than 20.
        /// Thrown when city, region, or country is greater than 15.
        /// Thrown when address is greater than 60. Thrown when title is greater than 30.
        /// Thrown when titleOfCourtesy is greater than 25.
        /// Thrown when extension is greater than 4.</exception>
        /// <exception cref="ArgumentException">Thrown when title, titleOfCourtesy, extension, firstName, lastname, address, city, region, postalCode, or country is empty, null, numbers, or has special characters</exception>
        public Employee(string titleOfCourtesy, string title, DateTime hireDate, string extension, int? reportsTo, string firstName, string lastName, DateTime birthDate, string address, string city, string region, string postalCode, string country, ContactInfo contactInfo) : this(title, hireDate, extension, reportsTo, firstName, lastName, birthDate, address, city, region, postalCode, country, contactInfo)
        {
            TitleOfCourtesy = titleOfCourtesy;
        }

        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        /// <param name="titleOfCourtesy">The title of courtesy of the person</param>
        /// <param name="title">The title of the employee</param>
        /// <param name="hireDate">The date the employee got hired</param>
        /// <param name="extension">The extension the employee is in</param>
        /// <param name="notes">The notes of the employee</param>
        /// <param name="reportsTo">The id of the person to report to</param>
        /// <param name="firstName">The person's firstname</param>
        /// <param name="lastName">The person's lastname</param>
        /// <param name="birthDate">The person's birthdate</param>
        /// <param name="address">The person's address</param>
        /// <param name="city">The city of the person</param>
        /// <param name="region">The region of the city</param>
        /// <param name="postalCode">The postal code of the city</param>
        /// <param name="country">The country</param>
        /// <param name="contactInfo">The person's contact informations</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when firstname, or postalCode is greater than 10.
        /// Thrown when lastName is greater than 20.
        /// Thrown when city, region, or country is greater than 15.
        /// Thrown when address is greater than 60. Thrown when title is greater than 30.
        /// Thrown when titleOfCourtesy is greater than 25.
        /// Thrown when extension is greater than 4.</exception>
        /// <exception cref="ArgumentException">Thrown when title, titleOfCourtesy, extension, firstName, lastname, address, city, region, postalCode, or country is empty, null, numbers, or has special characters</exception>
        public Employee(string titleOfCourtesy, string title, DateTime hireDate, string extension, string notes, int? reportsTo, string firstName, string lastName, DateTime birthDate, string address, string city, string region, string postalCode, string country, ContactInfo contactInfo) : this(titleOfCourtesy, title, hireDate, extension, reportsTo, firstName, lastName, birthDate, address, city, region, postalCode, country, contactInfo)
        {
            Notes = notes;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the title
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is greater than 30</exception>
        /// <exception cref="ArgumentException">Thrown when the value is empty, null or has special characters, or numbers</exception>
        public string Title
        {
            get => title;
            set
            {
                if(!string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, "^[ A-Za-z,]+$"))
                {
                    if(value.Length <= 30)
                    {
                        title = value;
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
        /// Gets or sets the title of courtesy
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is greater than 25</exception>
        /// <exception cref="ArgumentException">Thrown when the value is empty, null or has special characters, or numbers</exception>
        public string TitleOfCourtesy
        {
            get => titleOfCourtesy;
            set
            {
                if(!string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, "^[ A-Za-z.]+$"))
                {
                    if(value.Length <= 25)
                    {
                        titleOfCourtesy = value;
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
        /// Gets or sets the hire date
        /// </summary>
        public DateTime HireDate
        {
            get => hireDate;
            set
            {
                hireDate = value;
                // TODO: Validate, so you can't set hire date to something higher than todays date
                /*
                if()
                {
                    throw new ArgumentException();
                }*/
            }
        }

        /// <summary>
        /// Gets or sets the extension
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is greater than 4</exception>
        /// <exception cref="ArgumentException">Thrown when the value is empty, null, or has special characters</exception>
        public string Extension
        {
            get => extension;
            set
            {
                if(!string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, "^[0-9]+$"))
                {
                    if(value.Length <= 4)
                    {
                        extension = value;
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
        /// Gets or sets the photo
        /// </summary>
        public Uri Photo
        {
            get
            {
                if(photo == null)
                {
                    photo = new Uri(PhotoPath, UriKind.RelativeOrAbsolute);
                }
                return photo;
            }
            set
            {
                photo = value;
            }
        }

        /// <summary>
        /// Gets or sets the notes
        /// </summary>
        public string Notes
        {
            get => notes;
            set => notes = value;
        }

        /// <summary>
        /// Gets or sets the id of the person to report to
        /// </summary>
        public int? ReportsTo
        {
            get => reportsTo;
            set => reportsTo = value;
        }

        /// <summary>
        /// Gets or sets the photo path
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is greater than 255</exception>
        public string PhotoPath
        {
            get => photoPath;
            set
            {
                if(!string.IsNullOrEmpty(value) || File.Exists(value))
                {
                    if(value.Length <= 255)
                    {
                        photoPath = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    photoPath = "/Images/IngenPhoto.png";
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Checks the specified title to see if it is valid.
        /// </summary>
        /// <param name="title">The title you want to validate</param>
        /// <returns>A boolean telling whether it is valid or not, and a string error message</returns>
        public static (bool, string) IsValidTitle(string title)
        {
            if(string.IsNullOrEmpty(title))
            {
                return (false, "Titel må ikke være tom.");
            }

            if(!Regex.IsMatch(title, "^[ A-Za-z,]+$"))
            {
                return (false, "Titel må kun indholde bogstaver og mellemrum.");
            }
            return (true, string.Empty);
        }

        /// <summary>
        /// Checks the specified extension to see if it is valid.
        /// </summary>
        /// <param name="extension">The extension you want to validate</param>
        /// <returns>A boolean telling whether it is valid or not, and a string error message</returns>
        public static (bool, string) IsValidExtension(string extension)
        {
            if(string.IsNullOrWhiteSpace(extension))
            {
                return (false, "Afdeling må ikke være tom.");
            }

            if(!Regex.IsMatch(extension, "^[0-9]+$"))
            {
                return (false, "Afdeling må kun indholde tal.");
            }
            return (true, string.Empty);
        }

        /// <summary>
        /// Checks the specified 'reports to' to see if it is valid.
        /// </summary>
        /// <param name="reportsTo">The 'reports to' you want to validate</param>
        /// <returns>A boolean telling whether it is valid or not, and a string error message</returns>
        public static (bool, string) IsValidReportsTo(string reportsTo)
        {
            if(reportsTo != string.Empty && !Regex.IsMatch(reportsTo, "^[0-9]+$"))
            {
                return (false, "'Reportere til' må kun tal.");
            }
            return (true, string.Empty);
        }
        #endregion
    }
}
