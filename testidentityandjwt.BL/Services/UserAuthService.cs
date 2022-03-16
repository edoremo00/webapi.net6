using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using testidentityandjwt.BL.DTO;
using testidentityandjwt.BL.IServices;
using testidentityandjwt.DAL.Entities;
using IUserAuthService = testidentityandjwt.BL.IServices.IUserAuthService;

namespace testidentityandjwt.BL.Services
{

   //THIS IS MY TRY TO IMPLEMENT WHAT MOSH IS DOING 
   /* public class UserregisteredEventArgs : EventArgs
    {
        public MyUser MyUser { get; set; }

        public delegate Task<object> UserregisteredEventHandler(object source, UserregisteredEventArgs userregisteredEventArgs);

        public event UserregisteredEventHandler Userregistered;

        protected async Task<object> OnRegistereduser()
        {
            if (Userregistered != null)
                Userregistered(this, new UserregisteredEventArgs() { MyUser =});
        }
    }*/

  /* public class UserAuthFacade : IUserAuthFacade
   {
       private readonly IUserAuthService _authService;
       private readonly ISendEmailService _emailService;

       public UserAuthFacade(IUserAuthService authService, ISendEmailService emailService)
       {
           _authService = authService;
           _emailService = emailService;

           _authService.UserRegistered += _emailService.OnRegisteredUser;
       }

       public async Task<bool> RegisterUser(Registerdto registeredDto)
       {
           return await _authService.RegisterUser(registeredDto);
       }
   }*/

    //PUBLISHER CLASS. PUBLISH THE EVENT ONUSERREGISTERED

    //EVENTS AND DELEGATES STEPS

    
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<MyUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ISendEmailService _sendEmailService;

        public delegate object RegisterUserEventHandler(object source, UserArgs userargs);/*1-DECLARE A DELEGATE. THIS DELEGATE MAKES THE COMMUNICATION HAPPEN.
                                                                                           * THANKS TO HIM IT IS POSSIBLE TO KNOW WHICH METHOD IS GOING TO 
                                                                                           * BE CALLED IN THE SUBSCRIBER CLASS
                                                                                           * SOURCE IS THE PUBLISHER, EVENTARGS OR A CLASS THAT DERIVES FROM IT
                                                                                           * ARE EVERY INFORMATION THAT I LIKE TO PASS TO THIS EVENT
                                                                                           * IN THE SUBSCRIBER CLASS
                                                                                           */

        public event RegisterUserEventHandler UserRegistered;/*DECLARE AN EVENT OF SAME TYPE OF THE DELEGATE
                                                              * IN THE CONSTRUCTOR REGISTER THE SUBSCRIBERS FOR THIS EVENT
                                                              */

        protected virtual void OnUserRegistered(string email)/*DECLARE A METHOD FOR CONVENTIONS PROTECTED VIRTUAL VOID
                                                              * AND WITH ON[NAME OF THE EVENT] AS METHOD NAME
                                                              * CHECK IF THERE ARE SUBSCRIBERS FOR THIS EVENT
                                                              * IF SO RAISE THE EVENT
                                                              * */
        {
            UserRegistered?.Invoke(this, new UserArgs { Email = email });
        }

        public UserAuthService(UserManager<MyUser> userManager, IConfiguration configuration,ISendEmailService sendEmailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _sendEmailService = sendEmailService;
            UserRegistered += _sendEmailService.OnRegisteredUser;//HERE I REGISTER THE SENDEMAILSERVICE AS A SUBSCRIBER OF THE USERREGISTERED EVENT
        }
        

        

        /*protected virtual void OnRegisteredUser()
        {
            //this does the same null check and if its not null, invokes our event handler
            UserRegistered?.Invoke(this, EventArgs.Empty);
        }*/

        public JwtSecurityToken createtoken(List<Claim> userclaim)
        {
            
            var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: userclaim,
                signingCredentials: new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256)

                );
            return token;

        }

        public async Task<bool> RegisterUser(Registerdto register)  
        {
            MyUser useralreadyregistered = await _userManager.FindByNameAsync(register.Username.ToLower());
            if (useralreadyregistered is null)
            {
                MyUser user = new MyUser()
                {
                    Email = register.Email,
                    NormalizedEmail = register.Email.ToUpper(),
                    UserName = register.Username,
                    NormalizedUserName = register.Username.ToUpper(),
                    birthday = register.birthday.Date,
                    EmailConfirmed = false




                };
                IdentityResult registerresult = await _userManager.CreateAsync(user, register.Password);
                if (!registerresult.Succeeded)
                    return false;


                //SEND EMAIL TO USER WHO HAS JUST REGISTERED

                //WHAT HAPPENS IF EMAIL SENDING GOES WRONG??? MAYBE A TRY CATCH IS BETTER?--> IT IS :)
                try
                {
                    OnUserRegistered(register.Email);/*HERE I RAISE THE EVENT
                                                  * THE SUBSCRIBER GETS NOTIFIED AND CALLS THE ONREGISTEREDUSER
                                                  * IN THIS CASE. BUT MORE AS A RULE CALLS A METHOD WITH THE SAME
                                                  * SIGNATURE AS DECLARED BY THE PUBLISHER'S DELEGATE
                                                  */
                                                                      /*AFTER THE SUBSCRIBER GOT NOTIFIED 
                                                                      * BY THE EVENT AND PROCESS IT
                                                                      * I CAN REMOVE THE SUBSCRIBER REGISTRATION 
                                                                      * TO AVOID MEMORY LEAKS
                                                                      * */
                }catch(Exception e)
                {
                    UserRegistered -= _sendEmailService.OnRegisteredUser;
                }
               
                return true;

            }
            return false;
        }

        public async Task<JwtSecurityToken?> Login(LoginDTO login)
        {
            MyUser checkifexist = await _userManager.FindByEmailAsync(login.email.ToLower());

            if (checkifexist is null)
                return null;
            //SignInResult result=await new SignInManager<MyUser> signinmanager();

            bool checkpass = await _userManager.CheckPasswordAsync(checkifexist, login.Password);


            if (checkpass)
            {
                // var providers = await _userManager.GetValidTwoFactorProvidersAsync(checkifexist);
                string otp = await _userManager.GenerateTwoFactorTokenAsync(checkifexist, "Email");
                //crea un otp. servono emailconfirmed a true e twofactorenabled a true nell'utente per farle andare

                List<Claim> userclaims = new List<Claim>()
                {
                new Claim(ClaimTypes.Name,checkifexist.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                 };

                JwtSecurityToken token = createtoken(userclaims);

                OnUserRegistered(login.email);

                return token;
            }
            return null;
        }
    }

    public class UserArgs : EventArgs//CREATING A CLASS THAT EXTENDS FROM EVENTARGS IN ORDER TO PASS CUSTOM DATA TO THE EVENT
    {
        public string Email { get; set; }
    }
}
