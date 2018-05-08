using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public enum RandomStringEnum { Word, Sentence, Paragraph, Unique };

    public class RandomData
    {
         private  Dictionary<string,string> countryIso2CodesMapping;

         public RandomData()
         {
             #region country code data
             countryIso2CodesMapping = countryIso2CodesMapping ?? new Dictionary<string, string>() {
               { "AFG", "AF" },    // Afghanistan
               { "DEU", "DE" },    // Germany
               { "EGY", "EG" },    // Egypt
               { "ESP", "ES" },    // Spain
               { "FRA", "FR" },    // France
               { "GBR", "GB" },    // United Kingdom
               { "GRC", "GR" },    // Greece
               { "ITA", "IT" },    // Italy
               { "JPN", "JP" },    // Japan
               { "RUS", "RU" },    // Russia
               { "TUR", "TR" },    // Turkey
               { "USA", "US" }    // United States
               };
             #endregion
         }

        public string PhoneNumber
        {
            get
            {
                return Faker.RandomNumber.Next(100000000, 199999999).ToString();
            }
        }

        public string Email
        {
            get
            {
                var guid = Guid.NewGuid();
                return string.Format("test_{0}@checkouttest.co.uk", guid);
            }
        }

        public string UpdatedEmail
        {
            get
            {
                var guid = Guid.NewGuid();
                return string.Format("updated_{0}@checkouttest.co.uk", guid);
            }
        }

        /// <summary>
        /// Full name with first name and lastname e.g. muhsin meydan
        /// </summary>
        public string FullName
        {
            get
            {
                return Faker.Name.FullName();
            }
        }

        /// <summary>
        /// Firstname e.g. muhsin
        /// </summary>
        public string FirstName
        {
            get
            {
                return Faker.Name.First();
            }
        }

        /// <summary>
        /// Lastname e.g. meydan
        /// </summary>
        public string LastName
        {
            get
            {
                return Faker.Name.Last();
            }
        }

        /// <summary>
        /// CompanyName e.g. meydan
        /// </summary>
        public string CompanyName
        {
            get
            {
                return Faker.Company.Name();
            }
        }

        #region address properties
        /// <summary>
        /// Generates full address with street,city,ukpostcode and country
        /// </summary>
        public string Address
        {
            get
            {
                return string.Format("{0}, {1}, {2}, {3}", Faker.Address.StreetAddress(), Faker.Address.City(), Faker.Address.UkPostCode(), Faker.Address.Country());
            }

        }

        /// <summary>
        /// Generates random street address
        /// </summary>
        public string StreetAddress
        {
            get
            {
                return Faker.Address.StreetAddress();
            }

        }


        /// <summary>
        /// Generates random street name
        /// </summary>
        public string Street
        {
            get
            {
                return Faker.Address.StreetName();
            }
        }

        /// <summary>
        /// Generates random city
        /// </summary>
        public string City
        {
            get
            {
                return Faker.Address.City();
            }

        }

        /// <summary>
        /// Generates random uk postCode
        /// </summary>
        public string PostCode
        {
            get
            {
                return Faker.Address.UkPostCode();
            }

        }

        /// <summary>
        /// Generates random country
        /// </summary>
        public string Country
        {
            get
            {
                return Faker.Address.Country();
            }

        }

        /// <summary>
        /// Generates random country
        /// </summary>
        public string CountryISO2
        {
            get
            {
                return  countryIso2CodesMapping.ElementAt(Faker.RandomNumber.Next(0, countryIso2CodesMapping.Count)).Value;
            }

        }

        #endregion

        /// <summary>
        /// Generates random string
        /// </summary>
        public string String
        {
            get
            {
                return GetString();
            }

        }

        /// <summary>
        /// Generates random unique string
        /// </summary>
        public string UniqueString
        {
            get
            {
                return GetString(RandomStringEnum.Unique);
            }

        }

        
        /// <summary>
        /// Generates random string based on the RandomStringEnum options
        /// </summary>
        /// <param name="randomStringOption"></param>
        /// <param name="length">string length</param>
        public string GetString(RandomStringEnum randomStringOption = RandomStringEnum.Word, int length = 250)
        {
            switch (randomStringOption)
            {
                case RandomStringEnum.Word:
                    return Faker.Lorem.Sentence().Split(' ').First();//.Substring(0, length);
                case RandomStringEnum.Sentence:
                    return Faker.Lorem.Sentence();//.Substring(0, length);
                case RandomStringEnum.Paragraph:
                    return Faker.Lorem.Paragraph();//.Substring(0, length);
                case RandomStringEnum.Unique:
                    var guid = Guid.NewGuid();
                    var randomString = Convert.ToBase64String(guid.ToByteArray()).Replace("/", "-").Replace("+", "_").Replace("=", "");
                    return randomString;//.Substring(0, length);

                default:
                    goto case RandomStringEnum.Word;
            };
        }

        /// <summary>
        /// Generates a random number
        /// </summary>
        public static int GetNumber(int rangeFrom = 1, int rangeTo = 10000)
        {
            return Faker.RandomNumber.Next(rangeFrom, rangeTo);
        }

        /// <summary>
        /// Generates a random number
        /// </summary>
        public static decimal GetDecimalNumber(int rangeFrom = 0, int rangeTo = 10000, int precision=2)
        {

            Func<int> getRange = () =>
            {
                int range = 1;

                while (precision > 0)
                {
                    range *= 10;
                    precision--;
                }

                return range;
            };

            decimal.TryParse(string.Format("{0}.{1}", Faker.RandomNumber.Next(rangeFrom, rangeTo), Faker.RandomNumber.Next(0, getRange.Invoke())), out decimal randomDecimal);

            return randomDecimal;
        }
    }
}
