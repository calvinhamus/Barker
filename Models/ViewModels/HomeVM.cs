using Barker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barker.Models.ViewModels
{
    public class HomeVM
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Following { get; set; }
        public int Followers { get; set; }
        public List<Bark> Barks {get;set;}

    }
}