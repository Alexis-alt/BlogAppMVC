using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Utilidades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Blog.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
      


        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;


        //NO OLVIDAR CAMBIAR LA CONFIGURACIÓN DONDE SE INYECTA EL SERVICIO DE IDENTITY
        //Para hacer uso de los Roles
        private readonly RoleManager<IdentityRole> _roleManager;



        //Constructor

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }



        //Properties
        [BindProperty]


        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }


        //Clase que usamos como Model para la entrada y de la cual creamos una propiedad en esta clase para usar las propiedades necesesarias para registro
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Nombre { get; set; }

            public string Direccion { get; set; }

            public string Ciudad { get; set; }

            public string Pais { get; set; }

            public string PhoneNumber { get; set; }

        }


        //Methods

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            //Expresión de coalecencia
            //Si ***returnUrl*** viene null regresa ***Url.Content("~/")***
            //Si no viene null regresa el mismo ***returnUrl***

            returnUrl = returnUrl ?? Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser{
                    UserName = Input.Email, 
                    Email = Input.Email,
                    Nombre = Input.Nombre,
                    Ciudad = Input.Ciudad,
                    Direccion = Input.Direccion,
                    Pais = Input.Pais,
                    PhoneNumber = Input.PhoneNumber,
                    EmailConfirmed = true
                
                };

                                                            
                var result = await _userManager.CreateAsync(user, Input.Password);


                if (result.Succeeded)
                {
                    //Validamos si existen los roles
                    //De lo contrario se insertan en bd los roles especificados abajo, esto solo pasa una vez cuando hacemos el primer registro

                    if (! await _roleManager.RoleExistsAsync(Constants.Admin))
                    {

                        await _roleManager.CreateAsync(new IdentityRole(Constants.Admin));

                        await _roleManager.CreateAsync(new IdentityRole(Constants.User));

                    }

                    //Obtener el rol seleccionado 


                    //De la solicitud vamos al Form y extraemos el valor de la propiedad con el nomre especificado
                    string rol = Request.Form["radUsuarioRole"].ToString();

                    //Validamos si el rol seleccionado es admin y si lo es se agrega dicho rol al user

                    if (rol == Constants.Admin)
                    {
                        await _userManager.AddToRoleAsync(user,Constants.Admin);
                    }
                    else
                    {
                        if(rol == Constants.User)
                        {
                            await _userManager.AddToRoleAsync(user, Constants.User);

                        }


                    }



                    _logger.LogInformation("Usuario creado con exito.");


                    await _signInManager.SignInAsync(user,isPersistent:false);

                    return LocalRedirect(returnUrl);

                  /*  var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                  

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }*/
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
