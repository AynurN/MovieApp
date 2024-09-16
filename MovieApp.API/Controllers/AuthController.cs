using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.DTOs.TokenDTOS;
using MovieApp.Business.DTOs.UserDTOs;
using MovieApp.Business.Exceptions;
using MovieApp.Business.Interfaces;
using MovieApp.Core.Models;
using System.Transactions;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAuthService authService;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.authService = authService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromForm] UserRegisterDTO dto)
        {
            try
            {
                await authService.Register(dto);
            }
            catch (UnsuccesfulOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) { 
             return BadRequest(ex.Message); 
            }
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromForm] UserLoginDTO dto)
        {
            TokenResponseDTO token = null;
            try
            {
              token=  await authService.Login(dto);
            }
            catch (UnsuccesfulOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(token);
        }
        //[HttpPost("[action]")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1 = new IdentityRole("Admin");
        //    IdentityRole role2 = new IdentityRole("SuperAdmin");
        //    IdentityRole role3 = new IdentityRole("Member");
        //    IdentityRole role4 = new IdentityRole("Editor");

        //    await roleManager.CreateAsync(role4);
        //    await roleManager.CreateAsync(role1);
        //    await roleManager.CreateAsync(role2);
        //    await roleManager.CreateAsync(role3);

        //    return Ok();

        //}
       //[HttpPost("[action]")]
       // public async Task<IActionResult> CreateAdmin()
        //{
            //    AppUser user = new AppUser();
            //    user.UserName = "SuperAdmin";
            //    user.Email = "super@gmail.com";
            //    user.Fullname = "Super Admin";
            //    await userManager.CreateAsync(user, "Salam123@");
       //     AppUser user = await userManager.FindByNameAsync("SuperAdmin");
       //     await userManager.AddToRoleAsync(user, "SuperAdmin");
       //     return Ok();
       // }
    }
}
