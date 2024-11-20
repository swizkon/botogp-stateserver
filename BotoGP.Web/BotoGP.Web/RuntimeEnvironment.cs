using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BotoGP.Web;

public static class RuntimeEnvironment
{
    public static bool IsDevelopment
    {
        get; private set;
    }

    public static void Setup(IWebHostEnvironment env)
    {
        IsDevelopment = env.IsDevelopment(); //.IsDevelopment();
    }

}