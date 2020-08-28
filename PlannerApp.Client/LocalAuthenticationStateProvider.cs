﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PlannerApp.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlannerApp.Client
{
    public class LocalAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly ILocalStorageService _localStorageService;

        public LocalAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _localStorageService.ContainKeyAsync("User"))
            {
                // Create the user
                var userInfo = await _localStorageService.GetItemAsync<LocalUserInfo>("User");

                var claims = new[]
                {
                    new Claim("Email", userInfo.Email),
                    new Claim("FirstName", userInfo.FirstName),
                    new Claim("LastName", userInfo.LastName),
                    new Claim("AccessToken", userInfo.AccessToken),
                    new Claim(ClaimTypes.NameIdentifier, userInfo.Id),
                };

                var identity = new ClaimsIdentity(claims, "BearerToken");
                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;

            }
            return new AuthenticationState(new ClaimsPrincipal());
        }
    }
}
