using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BotoGP.Web.Pages.Circuits
{
    public class IndexModel : PageModel
    {

		public string Message { get; set; }

        public void OnGet()
        {
            Message = System.Guid.NewGuid().ToString();

        }
    }
}
