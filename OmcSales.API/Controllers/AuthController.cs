using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public IMapper Mapper { get; set; }
        public AuthController(UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext,
            IMapper mapper,RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            DbContext = dbContext;
            Mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromForm] RegisterDTO registerDTO)
        {

            var applicationUser = new ApplicationUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email
            };

            var userCreationResult = await UserManager.CreateAsync(applicationUser, registerDTO.Password);
           
            if (!userCreationResult.Succeeded)
            {
               
            }

            if (registerDTO.UserType == UserType.Admin)
            {
                await UserManager.AddToRoleAsync(applicationUser, "admin");
                
                foreach(var item in registerDTO.ProductsToSell)
                {
                    var product = new Product
                    {
                        ProductName = item,
                        UserId = applicationUser.Id
                    };
                   await DbContext.Products.AddAsync(product);
                }
                  await DbContext.SaveChangesAsync();
            }
            else if(registerDTO.UserType == UserType.Manager)
            {
                await UserManager.AddToRoleAsync(applicationUser, "manager");
            }


            return Ok();
        }
    }
}