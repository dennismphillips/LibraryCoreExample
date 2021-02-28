using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryCoreExample.Models.WebRequest
{
    public class BookRequest
    {

        [JsonProperty(Required = Required.Always)]
        [Required (ErrorMessage = "A title is required")]
        public string Title { get; set; }

        [JsonProperty(Required = Required.Always)]
        [Required(ErrorMessage = "An author is required")]
        public string Author { get; set; }

        [JsonProperty(Required = Required.Always)]
        [Required(ErrorMessage = "The number of pages is required")]
        [Range (1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int NumPages { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}
