using Yarp.Example.Gateway;
using Yarp.ReverseProxy.LoadBalancing;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<ILoadBalancingPolicy, TenantBasedLoadBalancingPolicy>()
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapReverseProxy();

app.Run();
