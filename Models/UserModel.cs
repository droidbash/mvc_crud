using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_crud.Models
{
    public class UserModel
    {
        public UserModel(int Id)
        {
            this.Id = Id;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Level { get; set; }
    }
}