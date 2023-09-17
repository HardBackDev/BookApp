using AutoMapper;
using BookAppServer.Contracts.RepositoriesContracts;
using BookAppServer.data;
using BookAppServer.Dto.BooksDto;
using BookAppServer.Dto.UserDto;
using BookAppServer.Exceptions;
using BookAppServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookAppServer.Services
{
    public class AuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private User? _user;
        private JwtConfiguration _jwtConfiguration = new JwtConfiguration();

        public AuthenticationService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            configuration.Bind(JwtConfiguration.Section, _jwtConfiguration);
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, "User");
            return result;
        }

        public async Task<bool> ValidateUser(UserForLogin userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.UserName) ??
                throw new BadRequestException("wrond username");
            var result = await _userManager.CheckPasswordAsync(_user, userForAuth.Password);

            if (!result)
                throw new BadRequestException("wrong password");
            return result;
        }

        public async Task<UserToken> CreateToken()
        {
            var roles = await _userManager.GetRolesAsync(_user);

            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(roles);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new UserToken()
            {
                token = token,
                userRoles = roles.ToList()
            };
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(IList<string> roles)
        {
            var claims = new List<Claim>{ new Claim(ClaimTypes.Name, _user.UserName) };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken
            (
                issuer: _jwtConfiguration.ValidIssuer,
                audience: _jwtConfiguration.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
