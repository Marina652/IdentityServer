using AutoMapper;
using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Application.DTOs;

namespace User.Application.Services;

public class AccountService : IAccountService
{
    private readonly SignInManager<Entities.User> _signInManager;
    private readonly UserManager<Entities.User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public AccountService(SignInManager<Entities.User> signInManager, UserManager<Entities.User> userManager, RoleManager<IdentityRole> roleManager, HttpClient client, IMapper mapper)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _httpClient = client;
        _mapper = mapper;
    }

    public async Task CreateAccount(NewAccountModel user)
    {
        var map = _mapper.Map<Entities.User>(user);

        var result = await _userManager.CreateAsync(map, user.Password);
        if (result.Succeeded)
        {
            //await _userManager.AddToRoleAsync(user, "User");
        }
    }

    public async Task<AuthenticatedAccountInfoDto> AuthenticationAccount(AuthenticationAccountDto authenticationAccount)
    {
        var accountIsvalid = await AccountIsValid(authenticationAccount);

        if (accountIsvalid is null)
            throw new Exception("Wrong username or password");

        var tokens = await GetTokens(authenticationAccount);

        return new AuthenticatedAccountInfoDto
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            UserRoles = await _userManager.GetRolesAsync(accountIsvalid),
        };
    }

    private async Task<Entities.User?> AccountIsValid(AuthenticationAccountDto authenticationAccount)
    {
        var account = await _userManager.FindByNameAsync(authenticationAccount.UserName);

        var res = await _signInManager.PasswordSignInAsync(authenticationAccount.UserName, authenticationAccount.Password, false, false);

        if (res.Succeeded)
            return account;

        return null;
    }

    private async Task<TokenResponse> GetTokens(AuthenticationAccountDto user)
    {
        var tokenRequest = new PasswordTokenRequest()
        {
            Address = "https://localhost:7177/connect/token",
            ClientId = "client",
            Scope = "Event.API",
            UserName = user.UserName,
            Password = user.Password,
            ClientSecret = "secret".Sha256()
        };

        var tokenResponse = await _httpClient.RequestPasswordTokenAsync(tokenRequest);

        return tokenResponse;
    }
}
