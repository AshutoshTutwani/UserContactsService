using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace Contact.Domain.Models
{
    public class UserDetail
    {
        public int Id { get; set; }
        //[JsonProperty("FirstName", NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string DateOfBirth{ get; set; }
        public List<string> Emails { get; set; } = new List<string>();
        public List<string> PhoneNumbers { get; set; } = new List<string>();
    }
}
