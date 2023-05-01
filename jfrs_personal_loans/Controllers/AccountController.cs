using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace jfrs_personal_loans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration, IEmailService emailService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
            this._emailService = emailService;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInfo model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                { 
                    UserName = model.Email, 
                    Email = model.Email, 
                    UserPhotoPath = model.UserPhotoPath,
                    Name = model.Name,
                    LastName = model.LastName,
                };
                
                var result = await _userManager.CreateAsync(user, model.Password);

                var customClaims = new List<Claim>()
                {
                    new Claim(Constants.ClaimTenantId, user.Id),
                };
                await _userManager.AddClaimsAsync(user, customClaims);




                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    if (!string.IsNullOrEmpty(token))
                    {
                        await SendEmailConfirmationEmail(user, token);
                    }

                    return BuildToken(model);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return BuildToken(userInfo);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private IActionResult BuildToken(UserInfo userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Super_secret_key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(5);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "jfrspersonalloans.com",
               audience: "jfrspersonalloans.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            });

        }

        [Route("Changepassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePwd([FromBody] ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user !=null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return Ok("Password changed successfully");
                    }
                    else
                    {
                        return BadRequest("Invalid change password attempt.");
                    }
                }
                else
                {
                    return BadRequest("Invalid change password attempt.");
                }

                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private async Task SendEmailConfirmationEmail(ApplicationUser user, string token)
        {
            var appDomain = _configuration.GetSection("Application:AppDomain").Value;
            var confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions userEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                Placeholders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.Email),
                    new KeyValuePair<string, string>("{{Name}}", user.Name),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmationLink, user.Id, token)),
                }
            };

            await _emailService.SendEmailForEmailConfirmation(userEmailOptions);
        }

        [Route("confirmemail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string uid, string token)
        {
            var user = await _userManager.FindByIdAsync(uid);
            UserEmailOptions userEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                Placeholders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.Email),
                    new KeyValuePair<string, string>("{{Name}}", user.Name),
                }
            };

            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    await _emailService.SendEmailForEmailConfirmationSuccess(userEmailOptions);
                }
                else
                {
                    await _emailService.SendEmailForEmailConfirmationFail(userEmailOptions);
                }
            }

            return Ok("Solicitud completada con éxito, por favor consulte su dirección de correo electrónico para más detalles.");
        }

        [Route("resetpasswordemail")]
        [HttpPost]
        public async Task<IActionResult> ResetPasswordEmail([FromBody] UserInforResetPasswordEmail model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    Random r = new Random();
                    int randNum = r.Next(100000);
                    string token = randNum.ToString("D6");
                    string tokenExpirationInMinutes = "2";
                    var expiration = DateTime.Now.AddMinutes(int.Parse(tokenExpirationInMinutes));

                    user.ResetPasswordCode = token;
                    user.ResetPasswordCodeTimeStamp = expiration;

                    var result = await _userManager.UpdateAsync(user);

                    UserEmailOptions userEmailOptions = new UserEmailOptions
                    {
                        ToEmails = new List<string>() { user.Email },
                        Placeholders = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("{{UserName}}", user.Email),
                        new KeyValuePair<string, string>("{{Name}}", user.Name),
                        new KeyValuePair<string, string>("{{VerificationCode}}", token),
                        new KeyValuePair<string, string>("{{ExpiratinTime}}", tokenExpirationInMinutes),
                    }
                    };

                    if (result.Succeeded)
                    {

                        await _emailService.SendEmailForResetPassword(userEmailOptions);
                        return Ok("Email Sent successfully");
                    }
                    else
                    {
                        return BadRequest("Invalid input attempt");
                    }
                }
                else
                {
                    return BadRequest("Invalid input attempt");
                }
            }
            return BadRequest("Invalid input attempt");
        }
        [Route("validateresetpasswordtoken")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] UserInfoResetPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (user.Email == model.Email && user.ResetPasswordCode == model.Token && user.ResetPasswordCodeTimeStamp > DateTime.Now)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        return Ok(token);
                    }
                    else
                    {
                        return BadRequest("Invalid input attempt");
                    }
                }
                else
                {
                    return BadRequest("Invalid input attempt");
                }
            }
            return BadRequest("Invalid input attempt");
        }

        [Route("changepasswordreset")]
        [HttpPost]
        public async Task<IActionResult> ResetPasswordChange([FromBody] ResetPasswordChange model) 
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

                    if (result.Succeeded)
                    {
                        return Ok("Password changed successfully");
                    }
                    else
                    {
                       return BadRequest("Invalid input attempt");
                    }
                }
                else
                {
                   return BadRequest("Invalid input attempt");
                }
            }
            return BadRequest(ModelState);
        }
    }

}
