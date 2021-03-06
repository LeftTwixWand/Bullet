﻿using Orleans;
using System.Collections.Generic;
using Interfaces;
using System.Threading.Tasks;
using Orleans.Providers;
using Core.Models;

namespace Grains
{
    [StorageProvider]
    public class UserGrain : Grain<UserArchive>, IUserGrain, IFriendGrain
    {
        public Task<string> GetLogin() => Task.FromResult(this.GetPrimaryKeyString());

        public Task<string> GetName() => Task.FromResult(this.State.Name);

        public Task<byte[]> GetProfileImage() => Task.FromResult(this.State.ProfileImage);

        public Task<bool> CheckPassword(string password) => Task.FromResult(password == this.State.Password);

        public Task<string> GetDescription() => Task.FromResult(this.State.Description);

        public async Task SetDescription(string description)
        {
            this.State.Description = description;
            await this.WriteStateAsync();
        }

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

        public async Task<bool> Register(string name, string password)
        {
            if (this.State.Name == null && this.State.Password == null)
            {
                this.State.Name = name;
                this.State.Password = password;
                await this.WriteStateAsync();
                return true;
            }

            return false;
        }

        public async Task AddFriend(string login)
        {
            if (!this.State.Friends.Contains(login))
            {
                this.State.Friends.Add(login);
                await this.WriteStateAsync();
            }
        }

        public async Task AddChat(string id)
        {
            if (!this.State.Chats.Contains(id))
            {
                this.State.Chats.Add(id);
                await this.WriteStateAsync();
            }
        }

        public Task<IEnumerable<WallPost>> GetWall() => Task.FromResult<IEnumerable<WallPost>>(this.State.Wall);

        public async Task AddWallPost(WallPost post)
        {
            this.State.Wall.Add(post);
            await this.WriteStateAsync();
        }
    }

    public class UserArchive
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Description { get; set; }

        public byte[] ProfileImage { get; set; }

        public List<string> Friends { get; } = new List<string>();

        public List<string> Chats { get; } = new List<string>();

        public List<WallPost> Wall { get; } = new List<WallPost>();
    }
}