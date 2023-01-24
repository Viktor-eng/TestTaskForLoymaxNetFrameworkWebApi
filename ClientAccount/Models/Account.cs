using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ClientAccount.Models
{
    public class Account
    {
        public int ClientId { get; set; }

        public int Balance { get; set; }

        [JsonIgnore]
        public Client Client { get; set; }
    }
}