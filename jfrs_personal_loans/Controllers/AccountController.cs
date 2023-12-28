using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            var userExist = await _userManager.FindByEmailAsync(model.Email);

            if (userExist==null)
            {
                if (ModelState.IsValid && model.Password.Length>7)
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
                            //return Ok(token);
                            try
                            {
                                await SendEmailConfirmationEmail(user, token);
                            }
                            catch (Exception e)
                            {

                                return BadRequest(e.Message);
                            }


                        }

                        return await BuildToken(model);
                    }
                    else
                    {
                        ModelState.AddModelError("message", "Could not register the user.");
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid model state.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("message", "The user already exist.");
                return BadRequest(ModelState);
            }

            

        }

        
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userInfo.Email);

                if (user!=null && !user.EmailConfirmed && (await _userManager.CheckPasswordAsync(user,userInfo.Password)))
                {
                    ModelState.AddModelError("message", "Email not confirmed yet.");
                    return BadRequest(ModelState);
                }

                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return await BuildToken(userInfo);
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //[Route("sendemail")]
        //[HttpPost]
        //public async Task<IActionResult> sendemail()
        //{

        //    MailMessage mail = new MailMessage
        //    {
        //        Subject = "Maricon",
        //        Body = "Esto es una broma",
        //        From = new MailAddress("info@jrspersonalloans.com", "Maria"),
        //        IsBodyHtml = false,
        //    };
        //    mail.To.Add("jrosario19@hotmail.com");

        //    NetworkCredential networkCredential = new NetworkCredential(_configuration["Email_user_name"], _configuration["Email_password"]);
        //    SmtpClient smtpClient = new SmtpClient()
        //    {
        //        Host = "mail5019.site4now.net",
        //        Port = 25,
        //        EnableSsl = false,
        //        UseDefaultCredentials = false,
        //        Credentials = networkCredential
        //    };

        //    try
        //    {
        //        await smtpClient.SendMailAsync(mail);
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex);
        //    }

            

        //    return Ok("Mariconazo");
        //}

            private async Task<IActionResult> BuildToken(UserInfo userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jdskljfkljasdklfjklasjdfkljklfdsjklfjsd"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(5);

            var user =  await _userManager.FindByEmailAsync(userInfo.Email);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "jfrspersonalloans.com",
               audience: "jfrspersonalloans.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration,
                user = user
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
                        return Ok(new { message = "Password changed successfully" });
                    }
                    else
                    {
                        ModelState.AddModelError("message", "Invalid change password attempt.");
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid change password attempt.");
                    return BadRequest(ModelState);
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
                    string tokenExpirationInMinutes = "5";
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
                        return Ok(new { message= "Email Sent successfully", user=user });
                    }
                    else
                    {
                        ModelState.AddModelError("message", "Invalid input attempt.");
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid input attempt.");
                    return BadRequest(ModelState);
                }
            }
            ModelState.AddModelError("message", "Invalid input attempt.");
            return BadRequest(ModelState);
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
                        return Ok(new { token = token, user=user } );
                    }
                    else
                    {
                        ModelState.AddModelError("message", "Invalid input attempt.");
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid input attempt.");
                    return BadRequest(ModelState);
                }
            }
            ModelState.AddModelError("message", "Invalid input attempt.");
            return BadRequest(ModelState);
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
                        return Ok(new { message = "Password changed successfully" });
                    }
                    else
                    {
                        ModelState.AddModelError("message", "Invalid input attempt.");
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid input attempt.");
                    return BadRequest(ModelState);
                }
            }
            ModelState.AddModelError("message", "Invalid input attempt.");
            return BadRequest(ModelState);
        }
    }

}
