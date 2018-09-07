using Evolve.Application;
using Evolve.Application.Services;
using Evolve.Domain.UserAggr;
using Evolve.Models;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Modules
{
    public class AccountModule : NancyModule
    {
        public AccountModule(IUserService userService)
        {
            Get["/login"] = _ => View["User/Login.sshtml", new { ReturnUrl =this.Request.Query.returnUrl !=null ? this.Request.Query.returnUrl:null }];

            Get["/register"] = _ => View["User/Register.sshtml"];

            Post["/login"] = getParam =>
                {
                    var formData = this.Request.Form;
                    User user = userService.GetUser(formData.email, formData.password);
                    if (user == null)
                    {
                        return View["User/Login.sshtml", new { ErrorMessage  = "Invalid credentials."}];
                    }
                    UserIdentity ui = new UserIdentity()
                    {
                        UserName = user.Username,
                        Claims = new List<string>()
                    };
                    string redirect = this.Request.Form.returnUrl;
                    if (redirect == null)
                        redirect = "/";
                    DateTime? expiry = null;
                    if (formData.rememberMe != null && formData.rememberMe == true)
                    {
                        expiry = DateTime.Now.AddDays(14);
                    }

                    return this.LoginAndRedirect(user.UserCredentials.UniqueNumber, expiry, redirect);
                };

            Post["/register"] = _ =>
                {
                    var formData = this.Request.Form;
                    try
                    {
                        if (((string)formData.password).Length < 6 )
                        {
                            return View["User/Register.sshtml", new { ErrorMessage = "Password must be at least 6 symbols." }];
                        }
                        userService.CreateUser(formData.email, formData.username, formData.password);
                    }
                    catch(EntityExistException e)
                    {
                        return View["User/Register.sshtml", new { ErrorMessage = e.Message}];
                    }
                    catch(Exception e)
                    {
                        return View["User/Register.sshtml", new { ErrorMessage = "Internal error" }];
                    }
                    return View["User/CancelRegistration.sshtml", new { Username = formData.username }];
                };

            Get["/logout"] = _ => this.LogoutAndRedirect("/");
        }
    }
}
