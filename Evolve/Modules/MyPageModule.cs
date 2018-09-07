using Evolve.Application.Services;
using Evolve.Infrastructure;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Security;

namespace Evolve.Modules
{
    public class MyPageModule : NancyModule
    {
        public MyPageModule(IUserService userService, IFileManager fileManager)
        {
            this.RequiresAuthentication();
            Get["/mypage"] = _ =>
            {
                var user = userService.GetUserByUsername(this.Context.CurrentUser.UserName);
                return View["/User/MyPage.sshtml", new { User = user }];
            };

            Post["/mypage/update"] = _ =>
            {
                var data = this.Request.Form;
                var newImage = this.Request.Files.FirstOrDefault();
                string fileUrl = null;
                if (newImage != null)
                {
                    var hostAddress = this.Request.Url.SiteBase;
                    fileUrl = fileManager.SaveImage(hostAddress, newImage.Value);
                }
                var user = userService.GetUserByUsername(this.Context.CurrentUser.UserName);
                fileUrl = fileUrl == null ? user.UserDetails.ImagePath : fileUrl;
                var user2 = userService.ChangeUserData(this.Context.CurrentUser.UserName, data.firstName, data.lastName, fileUrl, data.summary);
                return View["/User/MyPage.sshtml", new { User = user2, Message = "Changed." }];
            };

            Post["/mypage/changepass"] = _ =>
            {
                var data = this.Request.Form;
                if ((string)data.newPass != (string)data.repeatNewPass)
                {
                    var user = userService.GetUserByUsername(this.Context.CurrentUser.UserName);
                    return View["/User/MyPage.sshtml", new {User = user, Error = "Password must matches."}];
                }
                else
                {
                    var user = userService.ChangePassword(this.Context.CurrentUser.UserName, data.oldPass, data.newPass);
                    return View["/User/MyPage.sshtml", new { User = user, Message = "Changed.", IsSuccess = true }];
                }
            };
        }
    }
}
