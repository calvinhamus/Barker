using Barker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barker.Models.ViewModels
{
    public class CurrentUserVM
    {
        public string UserName { get; set; }
        public List<Bark> Barks { get; set; }
    }
}