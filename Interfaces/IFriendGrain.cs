using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IFriendGrain : Orleans.IGrainWithStringKey
    {
        Task<string> GetName();

        Task<string> GetLogin();

        Task<IEnumerable<WallPost>> GetWall();

        /// <summary>
        /// Returnes current profile picture for current user
        /// </summary>
        /// <returns>Byte array</returns>
        Task<byte[]> GetProfileImage();

        Task<string> GetDescription();
    }
}
