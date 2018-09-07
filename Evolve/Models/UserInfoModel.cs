using Evolve.Domain.PostAggr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Models
{
    public class UserInfoModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Image { get; set; }

        public string Summary { get; set; }

        public string CreatedDate { get; set; }

        public List<PostViewModel> Posts { get; set; }

        public bool IsOwner { get; set; }

        public bool ShowNext { get; set; }

        public bool NeedSubscribe { get; set; }

        public bool IsSubscribed { get; set; }

    }
}
