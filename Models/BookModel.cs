using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TRABAJO3_REST.Models
{
    public class BookModel
    {
        public long Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "error {0} There isn't a title name with more than {1} min is {2}", MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "error {0} There isn't a author name with more than {1} min is {2}", MinimumLength = 2)]
        public string Author { get; set; }
        [StringLength(20, ErrorMessage = "error {0} There isn't a subject name with more than {1} min is {2}", MinimumLength = 2)]
        public string Subject { get; set; }
        [StringLength(20, ErrorMessage = "error {0} There isn't a editorial name with more than {1} min is {2}", MinimumLength = 2)]
        public string Editorial { get; set; }
        public int Pages { get; set; }
        public int? CountLike { get; set; }
        public string Format { get; set; }
        
    }
}
