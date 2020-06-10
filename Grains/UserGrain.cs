using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using Interfaces;
using System.Threading.Tasks;

namespace Grains
{
    public class UserGrain : Grain, IUserGrain
    {
        public Task<string> SayHello() => Task.FromResult("Hello world!");
    }
}
