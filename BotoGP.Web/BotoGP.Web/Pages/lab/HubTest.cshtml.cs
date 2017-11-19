using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotoGP.stateserver.Controllers;
using BotoGP.stateserver.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BotoGP.Web.Pages
{
    public class HubTestModel : PageModel
    {
		public string Message { get; set; }

        public string CircuitId { get; set; }

        public void OnGet(string id)
        {
            CircuitId = id;
            Message = id;
        }
    }
}
