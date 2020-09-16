﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataValidation
{
    public class ValidateData
    {
        private readonly List<Contact> _contactList = null;

        public ValidateData()
        {
            _contactList = this.GetContacts();
        }
        public void Run()
        {
            try
            {
                if (this._contactList != null && this._contactList.Count > 0)
                {
                    Console.WriteLine("Contact List:");

                    foreach (Contact contact in this._contactList)
                    {
                        Console.WriteLine(contact.fullName.PadRight(25) + this.Validate(contact));
                    }

                    Console.WriteLine();

                    List<City> cityErrorList = this.GetCityErrorList();

                    Console.WriteLine("Errors by City:");

                    foreach (City city in cityErrorList)
                    {
                        Console.WriteLine(city.Name.PadRight(20) + city.ErrorCount);
                    }

                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("No contacts found.");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }

        private List<Contact> GetContacts()
        {
            string json  = this.GetJsonString();

            return JsonConvert.DeserializeObject<List<Contact>>(json).OrderBy(c => c.fullName).ToList();
        }

        private List<City> GetCityErrorList()
        {
            var cityList = this._contactList.Select(s => s.cityName).Distinct();
            List<City> result = new List<City>();

            foreach(var city in cityList)
            {
                var sumOfErrors = this._contactList.Where(w => w.cityName == city).Select(s => s.ErrorCount).Sum();
                result.Add(new City { Name = city, ErrorCount = sumOfErrors });
            }

            return result.OrderByDescending(o => o.ErrorCount).ToList();
        }

        private string Validate(Contact contact)
        {
            string result = "Valid";
            bool emailError = false;
            bool phoneNumberError = false;
            contact.ErrorCount = 0;
            
            if (contact.emailAddress.StartsWith("@") 
                || contact.emailAddress.IndexOf('@') == -1
                || contact.emailAddress.IndexOf('@') == contact.emailAddress.Length - 1 
                || contact.emailAddress.IndexOf('@') != contact.emailAddress.LastIndexOf('@'))
            {
                ++contact.ErrorCount;
                emailError = true;
                result = "Email is invalid.";
            }


            if (!Regex.IsMatch(contact.phoneNumber, @"^[0-9-\x20]*$"))
            {
                ++contact.ErrorCount;
                phoneNumberError = true;
                result = "Phone is invalid.";
            }

            if (emailError && phoneNumberError)
            {
                result = "Email and Phone are invalid";
            }

            return result;
        }

        private string GetJsonString()
        {
            return @"
                [
                      {
                        ""fullName"" : ""Emma Smith"",
                        ""cityName"" : ""Evansville"",
                        ""phoneNumber"" : ""+1 (555) 555-5555"",
                        ""emailAddress"" : ""emma.smith@example.com""
                      },
                      {
                        ""fullName"" : ""Liam Martinez"",
                        ""cityName"" : ""Chicago"",
                        ""phoneNumber"" : ""555 555-5555"",
                        ""emailAddress"" : ""liam.martinez@@example.com""
                      },
                      {
                        ""fullName"" : ""Olivia Johnson"",
                        ""cityName"" : ""Seattle"",
                        ""phoneNumber"" : ""555-555-5555"",
                        ""emailAddress"" : ""olivia.johnson@example.com""
                      },
                      {
                        ""fullName"" : ""James White"",
                        ""cityName"" : ""Kansas City"",
                        ""phoneNumber"" : ""555-555-5555"",
                        ""emailAddress"" : ""james.white@example.com""
                      },
                      {
                        ""fullName"" : ""Isabella Williams"",
                        ""cityName"" : ""New York"",
                        ""phoneNumber"" : ""555-555 5555"",
                        ""emailAddress"" : ""isabella.williams@example.com""
                      },
                      {
                        ""fullName"" : ""William Anderson"",
                        ""cityName"" : ""San Franscico"",
                        ""phoneNumber"" : ""555-555-5555"",
                        ""emailAddress"" : ""william.anderson@example.com""
                      },
                      {
                        ""fullName"" : ""Sophia Brown"",
                        ""cityName"" : ""Evansville"",
                        ""phoneNumber"" : ""555-555-5555"",
                        ""emailAddress"" : ""sophia.brown@example.com""
                      },
                      {
                        ""fullName"" : ""James Taylor"",
                        ""cityName"" : ""Chicago"",
                        ""phoneNumber"" : ""555-555-5555"",
                        ""emailAddress"" : ""james.taylor@examplecom""
                      },
                      {
                        ""fullName"" : ""Taylor Jones"",
                        ""cityName"" : ""Seattle"",
                        ""phoneNumber"" : ""555-555-5555"",
                        ""emailAddress"" : ""taylor.jones@example.com""
                      },
                      {
                        ""fullName"" : ""Logan Thomas"",
                        ""cityName"" : ""Kansas City"",
                        ""phoneNumber"" : ""(555) 555-5555 ext. 1234"",
                        ""emailAddress"" : ""logan.thomas@examplecom""
                      },
                      {
                        ""fullName"" : ""Charlotte Miller"",
                        ""cityName"" : ""New York"",
                        ""phoneNumber"" : ""555-555-5555"",
                        ""emailAddress"" : ""charlotte.miller@example.com""
                      },
                      {
                        ""fullName"" : ""Benjamin Hermandez"",
                        ""cityName"" : ""San Franscico"",
                        ""phoneNumber"" : ""555-555-5555"",
                        ""emailAddress"" : ""benjamin.hermandez@example.com""
                      },
                      {
                        ""fullName"" : ""Amelia Davis"",
                        ""cityName"" : ""Evansville"",
                        ""phoneNumber"" : ""555-555-5555"",
                        ""emailAddress"" : ""amelia.davis@example.com""
                      },
                      {
                        ""fullName"" : ""Mason Moore"",
                        ""cityName"" : ""Chicago"",
                        ""phoneNumber"" : ""555-555-5555"",
                        ""emailAddress"" : ""mason.moore@example.com""
                      },
                      {
                        ""fullName"" : ""Evelyn Garcia"",
                        ""cityName"" : ""Seattle"",
                        ""phoneNumber"" : ""555-555-5555"",
                        ""emailAddress"" : ""evelyn.garcia@example.com""
                      },
                      {
                        ""fullName"" : ""Elijah Martin"",
                        ""cityName"" : ""Kansas City"",
                        ""phoneNumber"" : ""5555555555"",
                        ""emailAddress"" : ""didn't provide one""
                      },
                      {
                        ""fullName"" : ""Abigail Rodriguez"",
                        ""cityName"" : ""New York"",
                        ""phoneNumber"" : ""555-5555-555"",
                        ""emailAddress"" : ""abigail.rodriguez@example.com""
                      },
                      {
                        ""fullName"" : ""Oliver Jackson"",
                        ""cityName"" : ""San Franscico"",
                        ""phoneNumber"" : ""555-5555-5555"",
                        ""emailAddress"" : ""oliver.jackson@example.com""
                      },
                      {
                        ""fullName"" : ""Mary Wilson"",
                        ""cityName"" : ""Evansville"",
                        ""phoneNumber"" : ""none"",
                        ""emailAddress"" : ""mary.wilson@example""
                      },
                      {
                        ""fullName"" : ""Jacob Thompson"",
                        ""cityName"" : ""Chicago"",
                        ""phoneNumber"" : ""555-555-5555"",
                        ""emailAddress"" : ""jacob.thompson@example.com""
                      }
                    ]";
        }
    }
}