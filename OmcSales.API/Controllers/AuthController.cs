using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OmcSales.API.Helpers;
using OmcSales.API.Helpers.DTOs;
using OmcSales.API.Helpers.Enums;
using OmcSales.API.Models;

namespace OmcSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public ApplicationDbContext DbContext { get; set; }
        public SignInManager<ApplicationUser> SignInManager { get; set; }
        public AppSettings AppSettings { get; set; }
        public IMapper Mapper { get; set; }

        public AuthController(UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            IOptions<AppSettings> appsettings,
            IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            DbContext = dbContext;
            Mapper = mapper;
            AppSettings = appsettings.Value;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO registerDTO)
        {

            var applicationUser = new ApplicationUser
            {
                Name = registerDTO.Username,
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                Token = Misc.GenerateToken(registerDTO.Email)
            };

            var userCreationResult = await UserManager.CreateAsync(applicationUser, registerDTO.Password);

            if (!userCreationResult.Succeeded)
            {
                return BadRequest(userCreationResult.Errors);
            }

            if (registerDTO.UserType == UserType.Admin)
            {
                applicationUser.Role = "admin";
                await UserManager.AddToRoleAsync(applicationUser, "admin");

                foreach (var item in registerDTO.ProductsToSell)
                {
                    var product = new Product
                    {
                        ProductName = DbContext.ProductBanks.Find(Convert.ToInt32(item)).ProductName,
                        UserId = applicationUser.Id
                    };
                    await DbContext.Products.AddAsync(product);
                }
                await DbContext.SaveChangesAsync();
            }
            else if (registerDTO.UserType == UserType.Manager)
            {
                applicationUser.Role = "manager";
                 await UserManager.AddToRoleAsync(applicationUser, "manager");
                //var managerWithTokenExists = DbContext.Users.Any(i => i.Token == registerDTO.Token);
                //if (managerWithTokenExists)
                //{
                //}
                //else
                //{
                //    return BadRequest(new { message = "No Manager exists with the specified token" });
                //}
            }

            return await Login(new LoginDTO
            {
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                RememberMe = true
            });
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            ApplicationUser user = null;

            user = await UserManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
                return BadRequest("Invalid Credentials");

            var signInResult = await SignInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);

            if (signInResult.Succeeded)
            {
                var userForReturn = Mapper.Map<UserForReturnDTO>(user);

                return Ok(new { token = GenerateToken(user, loginDTO.RememberMe), user = userForReturn });
            }
            return BadRequest("Invalid Credentials");
        }


        string GenerateToken(ApplicationUser user, bool rememberMe = false)
        {
            var claims = new List<Claim>
           {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var userRoles = DbContext.UserRoles.Where(r => r.UserId == user.Id);

            foreach (var userRole in userRoles)
            {
                var roleName = DbContext.Roles.Find(userRole.RoleId).Name;
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.SigningKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            DateTime expiryDate;
            if (rememberMe)
            {
                expiryDate = DateTime.Now.AddYears(50);
            }
            else
            {
                expiryDate = DateTime.Now.AddDays(1);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiryDate,
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}