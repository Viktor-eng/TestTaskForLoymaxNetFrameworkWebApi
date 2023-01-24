using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientAccount.Models
{
    public class RegisterClientModel
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public DateTime BirthDate { get; set; }
    }
}