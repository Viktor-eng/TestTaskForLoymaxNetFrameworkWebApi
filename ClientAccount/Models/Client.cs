using System;
using System.ComponentModel.DataAnnotations;

namespace ClientAccount.Models
{
    public class Client : IClient
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public DateTime BirthDate { get; set; }

        public Account Account { get; set; }
    }
}