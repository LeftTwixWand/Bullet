using Orleans;
using System.Collections.Generic;
using Interfaces;
using System.Threading.Tasks;
using Orleans.Providers;

namespace Grains
{
    [StorageProvider]
    public class UserGrain : Grain<Archive>, IUserGrain
    {
        public Task<string> GetLogin() => Task.FromResult(this.GetPrimaryKeyString());

        public Task<string> GetName() => Task.FromResult(this.State.Name);

        public Task<byte[]> GetProfileImage() => Task.FromResult(this.State.ProfileImage);

        public Task<bool> CheckPassword(string password) => Task.FromResult(password == this.State.Password);

        public Task<string> GetDescription() => Task.FromResult(this.State.ProfileDescription);

        public Task<IEnumerable<string>> GetChats() => Task.FromResult<IEnumerable<string>>(this.State.Chats);

        public Task<IEnumerable<string>> GetFriends() => Task.FromResult<IEnumerable<string>>(this.State.Friends);

        public async Task SetName(string name)
        {
            this.State.Name = name;
            await this.WriteStateAsync();
        }

        public async Task<bool> SetPassword(string newPassword, string oldPassword)
        {
            if (this.State.Password == oldPassword)
            {
                this.State.Password = newPassword;
                await WriteStateAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task SetProfileImage(byte[] data)
        {
            this.State.ProfileImage = data;
            await WriteStateAsync();
        }
    }

    public class Archive
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string ProfileDescription { get; set; }

        public byte[] ProfileImage { get; set; }

        public List<string> Friends { get; } = new List<string>();

        public List<string> Chats { get; } = new List<string>();
    }
}