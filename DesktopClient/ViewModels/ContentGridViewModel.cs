using Core.Models;
using DesktopClient.Helpers;
using DesktopClient.Services;
using DesktopClient.Views;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopClient.ViewModels
{
    public class ContentGridViewModel : Observable
    {
        //private ICommand itemClickComamnd;

        //public ICommand ItemClickComamnd => itemClickComamnd ?? (itemClickComamnd = new RelayCommand<UserModel>(OnItemClick));

        //public ObservableCollection<UserModel> Source { get; } = new ObservableCollection<UserModel>(); 

        //private void OnItemClick(UserModel item)
        //{
        //    if (item != null)
        //    {
        //        NavigationService.Frame.SetListDataItemForNextConnectedAnimation(item);
        //        NavigationService.Navigate<ProfilePage>(item);
        //    }
        //}

        //public async Task LoadDataAsync()
        //{
        //    await Task.Run(() =>
        //    {
        //        Source.Clear();
        //        Source.Add(new UserModel() { Username = "LeftTwixWand" });
        //        return Task.CompletedTask;
        //    });
        //}
    }
}
