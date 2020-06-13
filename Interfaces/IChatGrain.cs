using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IChatGrain : Orleans.IGrainWithStringKey
    {
        Task<string> GetID();

        Task<string> GetName();

        Task SetName(string name);

        Task<byte[]> GetImage();

        Task SetImage(byte[] image);

        Task AddUser(string login);

        Task RemoveUser(string login);

        Task<IEnumerable<string>> GetUsers();

        Task AddMessage(Message message);

        Task<IEnumerable<Message>> GetMessages();
    }
}
