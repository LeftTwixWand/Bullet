using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUserGrain : Orleans.IGrainWithStringKey
    {
        Task<string> SayHello();
    }
}
