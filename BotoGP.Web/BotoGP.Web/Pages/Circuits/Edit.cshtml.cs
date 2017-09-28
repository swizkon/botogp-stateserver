using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

using BotoGP.stateserver.Controllers;

namespace BotoGP.Web.Pages.Circuits
{
    public class EditModel : PageModel
	{
		public string Message { get; set; }

		public BotoGP.stateserver.Models.Circuit Circuit { get; set; }

        public void OnGet(string id)
        {
            Circuit = new CircuitsController().Get(id);
            Message = "Your application description page ." + id;
        }
    }
}
