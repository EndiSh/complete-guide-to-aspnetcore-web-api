using My_books.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_books.Data.ViewModels
{
    public class CustomActionResultVM
    {
        public Exception Exception { get; set; }
        public Publisher Publisher { get; set; }
    }
}
