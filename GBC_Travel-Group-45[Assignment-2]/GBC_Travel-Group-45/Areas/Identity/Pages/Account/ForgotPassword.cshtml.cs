using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace GBC_Travel_Group_45.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ForgotPasswordModel> _logger;

        public ForgotPasswordModel(UserManager<IdentityUser> userManager, ILogger<ForgotPasswordModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // Generate password reset token
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                // Construct callback URL for password reset
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                // Compose email message for password reset with a clickable link
                var emailBody = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

                // Send password reset email
                await SendEmailAsync(Input.Email, "Reset Password", "Reset your password", emailBody);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }

        private async Task SendEmailAsync(string email, string subject, string message, string htmlMessage)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 25,
                    Credentials = new NetworkCredential("noreplygbctravelgroup45@gmail.com", "sjmd rodc zsrh ornq"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage("noreplygbctravelgroup45@gmail.com", email, subject, message)
                {
                    IsBodyHtml = true,
                    Body = htmlMessage
                };

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log the exception for diagnosis
                Console.WriteLine($"Error sending email: {ex}");
            }
        }
    }
}
