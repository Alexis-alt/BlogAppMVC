using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Areas.Identity.Pages.Account.Manage
{
    //Esta vista es para el perfil del usuario

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Teléfono")]
            public string PhoneNumber { get; set; }

         
            public string Nombre { get; set; }

            [Display(Name = "Dirección")]
            public string Direccion { get; set; }

          
            public string Ciudad { get; set; }

            [Display(Name = "País")]
            public string Pais { get; set; }

        }


        //Carga los datos en de el perfil
        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nombre = user.Nombre,
                Ciudad = user.Ciudad,
                Pais = user.Pais,
                Direccion = user.Direccion
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {

           
          

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nombre = user.Nombre,
                Ciudad = user.Ciudad,
                Pais = user.Pais,
                Direccion = user.Direccion
            };

            //await LoadAsync(user);
            return Page();
        }



        //Se ejecuta cuando se guardan los cambios y se editan los datos
        public async Task<IActionResult> OnPostAsync()
        {
            //Este User proviene de PageModel que es de donde hereda esta clase dedicada para la vista del perfil 
            //Es de tipo ClaimsPrincipal
           

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            user.Nombre = Input.Nombre;
            user.PhoneNumber = Input.PhoneNumber;
            user.Pais = Input.Pais;
            user.Ciudad = Input.Ciudad;
            user.Direccion = Input.Direccion;


            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "El perfil se ha actualizado correctamente";
            return RedirectToPage();
        }
    }
}
