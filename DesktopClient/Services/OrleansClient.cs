using Interfaces;
using Orleans;

namespace DesktopClient.Services
{
    public static class OrleansClient
    {
        public static IClusterClient Client { get; set; }

        public static IUserGrain User { get; set; } 

        public static IFriendGrain OtherUser { get; set; }

        public static string Login { get; set; }

        public static void InitializeUser(string login)
        {
            Login = login;
            User = Client.GetGrain<IUserGrain>(login);
        }
        public static void InitializeOtherUser(string login) => OtherUser = Client.GetGrain<IFriendGrain>(login);
    }
}