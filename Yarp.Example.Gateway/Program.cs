using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Yarp.Example.Gateway;
using Yarp.ReverseProxy.LoadBalancing;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<ILoadBalancingPolicy, TenantBasedLoadBalancingPolicy>()
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services
    .AddAuthentication(BearerTokenDefaults.AuthenticationScheme)
    .AddBearerToken();

builder.Services
    .AddAuthorization(x =>
    {
        x.AddPolicy("product-api-access",
            policy => policy.RequireAuthenticatedUser().RequireClaim("product-api-access", true.ToString()));

        x.AddPolicy("sales-api-access",
            policy => policy.RequireAuthenticatedUser().RequireClaim("sales-api-access", true.ToString()));
    });

var app = builder.Build();

app.MapGet("login", (bool productApi = false, bool salesApi = false) =>
    Results.SignIn(
        new ClaimsPrincipal(
            new ClaimsIdentity(
                [
                    new Claim("sub", Guid.NewGuid().ToString()),
                    new Claim("product-api-access", productApi.ToString()),
                    new Claim("sales-api-access", salesApi.ToString()),
                ],
                BearerTokenDefaults.AuthenticationScheme)),
        authenticationScheme: BearerTokenDefaults.AuthenticationScheme));

app.UseAuthentication();

app.UseAuthorization();

app.MapReverseProxy();

app.Run();
