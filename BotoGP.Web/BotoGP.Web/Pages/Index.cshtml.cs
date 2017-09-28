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
    public class IndexModel : PageModel
	{
		public string Message { get; set; }
		public IEnumerable<Circuit> Circuits { get; set; }

		public void OnGet()
		{
			Message = System.Guid.NewGuid().ToString();
			Circuits = new CircuitsController().Get();
		}
    }
}
