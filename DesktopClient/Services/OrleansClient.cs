using Interfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Services
{
    public static class OrleansClient
    {
        public static IClusterClient Client { get; set; }

        public static IUserGrain User { get; set; } 

        public static void InitializeUser(string login) => User = Client.GetGrain<IUserGrain>(login);

    }
}
