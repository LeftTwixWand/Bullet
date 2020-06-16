using Core.Models;
using Core.Services;
using DesktopClient.Helpers;
using DesktopClient.Services;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace DesktopClient.ViewModels
{
    public class FriendsViewModel : Observable
    {
        private User _selected;

        public User Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<User> SampleItems { get; private set; } = new ObservableCollection<User>();

        public FriendsViewModel()
        {
        }

        public async Task LoadDataAsync(MasterDetailsViewState viewState)
        {
            SampleItems.Clear();

            //var data = await SampleDataService.GetMasterDetailDataAsync();

            //foreach (var item in data)
            //{
            //    SampleItems.Add(item);
            //}

            //await OrleansClient.User.AddFriend("LeftTwixWand");
            //await OrleansClient.User.AddFriend("eeeeee");

            foreach (var item in await OrleansClient.User.GetFriends())
            {
                OrleansClient.InitializeOtherUser(item);

                SampleItems.Add(new User(await OrleansClient.OtherUser.GetName(), item, await ImageToBytesConverter.ToImage(await OrleansClient.OtherUser.GetProfileImage()), await OrleansClient.OtherUser.GetDescription()));
            }

            if (viewState == MasterDetailsViewState.Both)
            {
                Selected = SampleItems.First();
            }
        }
    }
}