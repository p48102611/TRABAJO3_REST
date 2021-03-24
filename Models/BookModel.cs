using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRABAJO3_REST.Models
{
    public class BookModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Editorial { get; set; }
        public int? Like { get; set; }
        public string Format { get; set; }
    }
}
