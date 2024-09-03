﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eAppointmentServer.Application.Services;
using eAppointmentServer.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace eAppointmentServer.InfraStructure.Services;

public sealed class JwtProvider:IJwtProvider
{
    public string CreateToken(AppUser appUser)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new Claim(ClaimTypes.Name, appUser.FullName),
            new Claim(ClaimTypes.Email, appUser.Email ?? string.Empty),
            new Claim("UserName", appUser.UserName ?? string.Empty),
        };

        DateTime expires = DateTime.Now.AddDays(1);

        SymmetricSecurityKey symmetricSecurityKey =
            new(Encoding.UTF8.GetBytes(string.Join("-", Guid.NewGuid(), Guid.NewGuid())));

        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);
        
        JwtSecurityToken jwtSecurityToken = new(
            issuer: "Ofluoğlu",
            audience: "eAppointment",
            claims: claims, 
            notBefore: DateTime.Now, 
            expires: expires, 
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(jwtSecurityToken);

        return token;
    }
}