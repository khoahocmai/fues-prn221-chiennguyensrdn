using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repo
{
    public class ResetPasswordRepo
    {
        private static readonly Dictionary<string, (string email, DateTime expiry)> ResetTokens = new Dictionary<string, (string, DateTime)>();

        public async Task<bool> SendPasswordResetEmail(string email)
        {
            using var db = new FUESManagementContext();
            var user =  db.Users.SingleOrDefault(u => u.Email == email);
            if (user != null)
            {
                var token = Guid.NewGuid().ToString();
                var expiryTime = DateTime.UtcNow.AddMinutes(1); 
                ResetTokens[token] = (user.Email, expiryTime);
                var resetLink = $"https://localhost:7146/Account/ResetPassword?token={token}";

                // Send email with the reset link
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("studentexchangeweb@gmail.com", "fwpl wpkw zhqe peyh")
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("studentexchangeweb@gmail.com"),
                    Subject = "Password Reset Request",
                    Body = $@"
                    <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                color: #333333;
                            }}
                            .container {{
                                background-color: #ffffff;
                                padding: 20px;
                                margin: 0 auto;
                                width: 80%;
                                max-width: 600px;
                                border-radius: 10px;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                            }}
                            h2 {{
                                color: #00cc00;
                            }}
                            p {{
                                line-height: 1.6;
                            }}
                            .footer {{
                                margin-top: 20px;
                                font-size: 0.9em;
                                color: #777777;
                            }}
                            @media (max-width: 600px) {{
                                .container {{
                                    width: 100%;
                                    padding: 10px;
                                }}
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h2>Password Reset Request</h2>
                            <p>Dear {user.Email},</p>
                            <p>We received a request to reset your password. Please click the link below to reset your password:</p>
                            <p><a href='{resetLink}'>Reset Password</a></p>
                            <p>If you did not request this change, please ignore this email.</p>
                            <br>
                            <p>Best regards,</p>
                            <p>Student Exchange Web Team</p>
                            <div class='footer'>
                                <p>This is an automated message, please do not reply.</p>
                            </div>
                        </div>
                    </body>
                    </html>
                ",
                    IsBodyHtml = true
                };
                mailMessage.To.Add(user.Email);

                await smtpClient.SendMailAsync(mailMessage);

                return true;
            }
            return false;
        }

        public async Task<bool> ResetPassword(string token, string newPassword)
        {
            if (ResetTokens.TryGetValue(token, out var tokenData))
            {
                if (DateTime.UtcNow <= tokenData.expiry)
                {
                    using var db = new FUESManagementContext();
                    var user = await db.Users.SingleOrDefaultAsync(u => u.Email == tokenData.email);
                    if (user != null)
                    {
                        user.Password = newPassword;
                        db.Users.Update(user);
                        await db.SaveChangesAsync();

                        // Send email notification to the user
                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            EnableSsl = true,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential("studentexchangeweb@gmail.com", "fwpl wpkw zhqe peyh")
                        };

                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress("studentexchangeweb@gmail.com"),
                            Subject = "Password Reset Successfully",
                            Body = $@"
                        <html>
                        <head>
                            <style>
                                body {{
                                    font-family: Arial, sans-serif;
                                    background-color: #f4f4f4;
                                    color: #333333;
                                }}
                                .container {{
                                    background-color: #ffffff;
                                    padding: 20px;
                                    margin: 0 auto;
                                    width: 80%;
                                    max-width: 600px;
                                    border-radius: 10px;
                                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                }}
                                h2 {{
                                    color: #00cc00;
                                }}
                                p {{
                                    line-height: 1.6;
                                }}
                                .footer {{
                                    margin-top: 20px;
                                    font-size: 0.9em;
                                    color: #777777;
                                }}
                                @media (max-width: 600px) {{
                                    .container {{
                                        width: 100%;
                                        padding: 10px;
                                    }}
                                }}
                            </style>
                        </head>
                        <body>
                            <div class='container'>
                                <h2>Password Reset Successfully</h2>
                                <p>Dear {user.Email},</p>
                                <p>Your password has been successfully reset.</p>
                                <p>If you did not request this change, please contact our support team immediately.</p>
                                <br>
                                <p>Best regards,</p>
                                <p>Student Exchange Web Team</p>
                                <div class='footer'>
                                    <p>This is an automated message, please do not reply.</p>
                                </div>
                            </div>
                        </body>
                        </html>
                    ",
                            IsBodyHtml = true
                        };
                        mailMessage.To.Add(user.Email);

                        await smtpClient.SendMailAsync(mailMessage);

                        // Remove the token after use
                        ResetTokens.Remove(token);

                        return true;
                    }
                }
                else
                {
                    // Token has expired
                    ResetTokens.Remove(token);
                }
            }
            return false;
        }
    }
}
