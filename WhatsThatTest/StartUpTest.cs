using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsThatSong;
using Xunit;

namespace WhatsThatTest
{
    public class StartUpTest
    {
        [Fact]
        public void StartupTest()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.NotNull(webHost);
        }
    }
}
