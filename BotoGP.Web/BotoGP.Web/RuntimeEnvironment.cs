using System;
using Microsoft.AspNetCore.Hosting;

namespace BotoGP.Web
{
    public static class RuntimeEnvironment
    {
        public static bool IsDevelopment
        {
            get; private set;
        }

        public static void Setup(IHostingEnvironment env)
        {
            IsDevelopment = env.IsDevelopment();
        }

    }
}
