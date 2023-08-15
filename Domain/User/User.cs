using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User
{
    public class User : IUser
    {
        public User(string id)
        {
            _id = id;
        }
        private string _id;

        public string Id
        {
            get => _id; set => _id = value;
        }
    }
}
