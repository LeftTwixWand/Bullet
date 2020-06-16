using Windows.UI.Xaml.Media;

namespace Core.Models
{
    public class User
    {
        public string Name { get; set; }

        public string Login { get; set; }
        
        public ImageSource ProfilePicture { get; set; }

        public string Description { get; set; }

        public User()
        {
        }

        public User(string name, string login, ImageSource profilePicture, string description)
        {
            Name = name;
            Login = login;
            ProfilePicture = profilePicture;
            Description = description;
        }
    }
}
