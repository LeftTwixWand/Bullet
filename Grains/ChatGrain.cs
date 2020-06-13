using Orleans.Providers;
using System.Collections.Generic;
using Orleans;
using Interfaces;
using System.Threading.Tasks;
using Core.Models;

namespace Grains
{
    [StorageProvider]
    public class ChatGrain : Grain<ChatArchive>, IChatGrain
    {
        public Task<string> GetID() => Task.FromResult(this.GetPrimaryKeyString());

        public Task<string> GetName() => Task.FromResult(this.State.Name);

        public Task<byte[]> GetImage() => Task.FromResult(this.State.Image);

        public async Task SetImage(byte[] image)
        {
            this.State.Image = image;
            await WriteStateAsync();
        }

        public async Task SetName(string name)
        {
            this.State.Name = name;
            await WriteStateAsync();
        }

        public async Task AddMessage(Message message)
        {
            this.State.Messages.Add(message);
            await this.WriteStateAsync();
        }

        public async Task AddUser(string login)
        {
            this.State.Users.Add(login);
            await this.WriteStateAsync();
        }

        public Task<IEnumerable<Message>> GetMessages() => Task.FromResult<IEnumerable<Message>>(this.State.Messages);

        public Task<IEnumerable<string>> GetUsers() => Task.FromResult<IEnumerable<string>>(this.State.Users);

        public async Task RemoveUser(string login)
        {
            if (this.State.Users.Contains(login))
            {
                this.State.Users.Remove(login);
                await this.WriteStateAsync();
            }
        }
    }

    public class ChatArchive
    {
        public string Name { get; set; }

        public byte[] Image { get; set; }

        public List<string> Users { get; } = new List<string>();

        public List<Message> Messages { get; } = new List<Message>();
    }
}