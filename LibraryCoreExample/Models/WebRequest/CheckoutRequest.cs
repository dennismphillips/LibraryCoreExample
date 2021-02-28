using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCoreExample.Models.WebRequest
{
    public class CheckoutRequest
    {
        [JsonProperty(Required = Required.Always)]
        [Required(ErrorMessage = "A customerIdentifier is required")]
        public int CustomerIdentifier { get; set; }

        [JsonProperty(Required = Required.Always)]
        [Required(ErrorMessage = "A bookIdentifier is required")]
        public int BookIdentifier { get; set; }
    }
}
