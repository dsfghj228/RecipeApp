using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Models.Dto;
using api.Models.FavoriteRecipesController;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = registerModel.UserName,
                    Email = registerModel.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerModel.Password);

                if(createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(new NewUserModel {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser),
                            PhotoName = appUser.PhotoName
                        });
                    }else{
                        return StatusCode(500, roleResult.Errors);
                    }
                }else {
                    return StatusCode(500, createdUser.Errors);
                }

            } catch (Exception e) {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginModel.UserName.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid username!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if(!result.Succeeded)
            {
                return Unauthorized("Username not found and/or password incorrect");
            }

            return Ok(new NewUserModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                PhotoName = user.PhotoName
            });
        }

        [HttpPost("uploadAvatar")]
        public async Task<IActionResult> UploadAvatar([FromBody] string photoFileName)
        {
            if (string.IsNullOrEmpty(photoFileName))
            {
                return BadRequest("Photo file name is required.");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            try
            {
                user.PhotoName = photoFileName;

                var updateResult = await _userManager.UpdateAsync(user);
        
                if (!updateResult.Succeeded)
                {
                    return StatusCode(500, updateResult.Errors);
                }

                return Ok(new 
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhotoName = user.PhotoName
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

    }
}