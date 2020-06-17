using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    /// <summary>
    /// Orleans grain communication interface that will save all user information
    /// </summary>
    public interface IUserGrain : Orleans.IGrainWithStringKey
    {
        Task<string> GetLogin();

        Task<string> GetName();

        Task SetName(string newName);

        Task SetDescription(string description);
        
        /// <summary>
        /// Returnes current profile picture for current user
        /// </summary>
        /// <returns>Byte array</returns>
        Task<byte[]> GetProfileImage();

        Task AddFriend(string login);

        Task SetProfileImage(byte[] data);

        Task<string> GetDescription();

        Task<bool> CheckPassword(string password);

        Task<bool> SetPassword(string newPassword, string oldPassword);

        Task<bool> Register(string name, string password);

        Task AddChat(string id);

        // Task<IEnumerable<byte[]>> GetAvatars();

        // Task<IEnumerable<IChatGrain>> GetChats();
        Task<IEnumerable<string>> GetChats();

        Task<IEnumerable<WallPost>> GetWall();

        Task AddWallPost(WallPost post);

        //Task<IEnumerable<IUserGrain>> GetFriends(); 
        Task<IEnumerable<string>> GetFriends();
    }
}