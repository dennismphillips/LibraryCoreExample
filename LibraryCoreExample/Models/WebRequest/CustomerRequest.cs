using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCoreExample.Models.WebRequest
{
    public class CustomerRequest
    {
        [JsonProperty(Required = Required.Always)]
        [Required(ErrorMessage = "A customer name is required")]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        [Required(ErrorMessage = "A customer phone number is required")]
        public string Phone { get; set; }

        [JsonProperty(Required = Required.Always)]
        [Required(ErrorMessage = "A customer email is required")]
        public string Email { get; set; }
    }
}
