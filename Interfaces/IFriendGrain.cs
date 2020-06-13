using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IFriendGrain : Orleans.IGrainWithStringKey
    {
        Task<string> GetName();

        /// <summary>
        /// Returnes current profile picture for current user
        /// </summary>
        /// <returns>Byte array</returns>
        Task<byte[]> GetProfileImage();

        Task<string> GetDescription();
    }
}
