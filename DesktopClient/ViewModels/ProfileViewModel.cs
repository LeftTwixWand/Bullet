using DesktopClient.Helpers;
using DesktopClient.Services;
using System;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace DesktopClient.ViewModels
{
    public class ProfileViewModel : Observable
    {
        private string name;

        public string Name
        {
            get => name;
            set { Set(ref name, value); }
        }

        private string login;

        public string Login
        {
            get => login;
            set { Set(ref login, value); }
        }

        private string descriprion;

        public string Description
        {
            get => descriprion;
            set { Set(ref descriprion, value); }
        }

        private bool isNameEditing = false;

        public bool IsNameEditing
        {
            get => isNameEditing;
            set { Set(ref isNameEditing, value); }
        }

        private bool isDescriptionEditing = false;

        public bool IsDescriptionEditing
        {
            get => isDescriptionEditing;
            set { Set(ref isDescriptionEditing, value); }
        }

        private BitmapImage profileImage;

        public BitmapImage ProfileImage
        {
            get => profileImage;
            set { Set(ref profileImage, value); }
        }

        private ICommand uploadImageCommand;

        public ICommand UploadImageCommand => uploadImageCommand ?? (uploadImageCommand = new RelayCommand(UploadProfileImage));

        private ICommand deleteImageCommand;

        public ICommand DeleteImageCommand => deleteImageCommand ?? (deleteImageCommand = new RelayCommand(DeleteProfileImage));

        public async void UploadProfileImage()
        {
            // Set up the file picker.
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;

            // Filter to include a sample subset of file types.
            picker.FileTypeFilter.Clear();
            picker.FileTypeFilter.Add(".bmp");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".jpg");

            // Open the file picker.
            StorageFile file = await picker.PickSingleFileAsync();

            // 'file' is null if user cancels the file picker.
            if (file != null)
            {
                // Open a stream for the selected file.
                // The 'using' block ensures the stream is disposed
                // after the image is loaded.
                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    await OrleansClient.User.SetProfileImage(await ImageToBytesConverter.ToBytes(stream));

                    // Set the image source to the ProfileImage property.
                    BitmapImage image = new BitmapImage();
                    await image.SetSourceAsync(stream);
                    ProfileImage = image;
                }
            }
        }

        private void DeleteProfileImage()
        {

        }

        public ProfileViewModel()
        {

        }

        public async void Initialize()
        {
            byte[] data = await OrleansClient.User.GetProfileImage();
            if (data == null)
            {
                ProfileImage = new BitmapImage(new Uri("ms-appx:///Assets/Unknown.jpg"));
            }
            else
            {
                ProfileImage = await ImageToBytesConverter.ToImage(data);
            }

            Login = OrleansClient.Login;
            Name = await OrleansClient.User.GetName();

            OrleansClient.User.SetDescription("19 y.o. software developer.");
            Description = await OrleansClient.User.GetDescription();
        }
    }
}