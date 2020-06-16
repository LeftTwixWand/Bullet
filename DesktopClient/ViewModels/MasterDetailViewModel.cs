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
    public class MasterDetailViewModel : Observable
    {
        private SampleOrder _selected;

        public SampleOrder Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new ObservableCollection<SampleOrder>();

        public MasterDetailViewModel()
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

                SampleItems.Add(new SampleOrder(await OrleansClient.OtherUser.GetName(), item, await ImageToBytesConverter.ToImage(await OrleansClient.OtherUser.GetProfileImage())));
            }

            if (viewState == MasterDetailsViewState.Both)
            {
                Selected = SampleItems.First();
            }
        }
    }
}