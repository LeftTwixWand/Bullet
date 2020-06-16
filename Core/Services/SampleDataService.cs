using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Core.Services
{
    public static class SampleDataService
    {
        private static IEnumerable<User> _allOrders;

        private static IEnumerable<User> AllOrders()
        {
            // The following is order summary data
            var companies = AllCompanies();
            return companies.SelectMany(c => c.Orders);
        }

        private static IEnumerable<SampleCompany> AllCompanies()
        {
            return new List<SampleCompany>()
            {
                new SampleCompany()
                {
                    CompanyID = "ALFKI",
                    CompanyName = "Company A",
                    ContactName = "Maria Anders",
                    ContactTitle = "Sales Representative",
                    Address = "Obere Str. 57",
                    City = "Berlin",
                    PostalCode = "12209",
                    Country = "Germany",
                    Phone = "030-0074321",
                    Fax = "030-0076545",
                    Orders = new List<User>()
                    {
                        new User()
                        {
                            Name = "MyName",
                            Login = "MyLogin",
                            ProfilePicture = new BitmapImage(new Uri("ms-appx:///Assets/Unknown.jpg")),
                        },
                        new User()
                        {
                            Name = "MyName",
                            Login = "MyLogin",
                            ProfilePicture = new BitmapImage(new Uri("ms-appx:///Assets/Unknown.jpg")),
                                                    },
                        new User()
                        {
                            Name = "MyName",
                            Login = "MyLogin",
                            ProfilePicture = new BitmapImage(new Uri("ms-appx:///Assets/Unknown.jpg")),
                        }
                    }
                },
                new SampleCompany()
                {
                    CompanyID = "ANATR",
                    CompanyName = "Company F",
                    ContactName = "Ana Trujillo",
                    ContactTitle = "Owner",
                    Address = "Avda. de la Constitución 2222",
                    City = "México D.F.",
                    PostalCode = "05021",
                    Country = "Mexico",
                    Phone = "(5) 555-4729",
                    Fax = "(5) 555-3745",
                    Orders = new List<User>()
                    {
                        new User()
                        {
                                                    Name = "MyName",
                            Login = "MyLogin",
                            ProfilePicture = new BitmapImage(new Uri("ms-appx:///Assets/Unknown.jpg")),
                        },
                        new User()
                        {
                          
                            Name = "MyName",
                            Login = "MyLogin",
                            ProfilePicture = new BitmapImage(new Uri("ms-appx:///Assets/Unknown.jpg")),
                     
                        }
                    }
                },
                new SampleCompany()
                {
                    CompanyID = "ANTON",
                    CompanyName = "Company Z",
                    ContactName = "Antonio Moreno",
                    ContactTitle = "Owner",
                    Address = "Mataderos  2312",
                    City = "México D.F.",
                    PostalCode = "05023",
                    Country = "Mexico",
                    Phone = "(5) 555-3932",
                    Fax = string.Empty,
                    Orders = new List<User>()
                    {
                        new User()
                        {
                           
                            Name = "MyName",
                            Login = "MyLogin",
                            ProfilePicture = new BitmapImage(new Uri("ms-appx:///Assets/Unknown.jpg")),
                          
                        },
                        new User()
                        {
                           
                            Name = "MyName",
                            Login = "MyLogin",
                            ProfilePicture = new BitmapImage(new Uri("ms-appx:///Assets/Unknown.jpg")),
                            },
                        new User()
                        {
                            Name = "MyName",
                            Login = "MyLogin",
                            ProfilePicture = new BitmapImage(new Uri("ms-appx:///Assets/Unknown.jpg")),
                         }
                    }
                }
            };
        }

        // TODO WTS: Remove this once your ContentGrid page is displaying real data.
        public static async Task<IEnumerable<User>> GetContentGridDataAsync()
        {
            if (_allOrders == null)
            {
                _allOrders = AllOrders();
            }

            await Task.CompletedTask;
            return _allOrders;
        }

        // TODO WTS: Remove this once your MasterDetail pages are displaying real data.
        public static async Task<IEnumerable<User>> GetMasterDetailDataAsync()
        {
            await Task.CompletedTask;
            return AllOrders();
        }

        // TODO WTS: Remove this once your TwoPaneView pages are displaying real data.
        public static async Task<IEnumerable<User>> GetTwoPaneViewDataAsync()
        {
            await Task.CompletedTask;
            return AllOrders();
        }
    }
}