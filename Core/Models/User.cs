using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Media;

namespace Core.Models
{
    public class User
    {
        public string Name { get; set; }

        public string Login { get; set; }
        
        public ImageSource ProfilePicture { get; set; }

        public string Description { get; set; }

        public IEnumerable<WallPost> Enumerable { get; set; } 

        public User()
        {
        }

        public User(string name, string login, ImageSource profilePicture, string description, IEnumerable<WallPost> enumerable)
        {
            Name = name;
            Login = login;
            ProfilePicture = profilePicture;
            Description = description;
            Enumerable = enumerable;
        }
    }
}
