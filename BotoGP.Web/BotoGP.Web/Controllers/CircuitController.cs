using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotoGP.stateserver.Models;
using BotoGP.stateserver.Repos;
using BotoGP.Web;
using Microsoft.AspNetCore.Mvc;

namespace BotoGP.stateserver.Controllers
{
    [Route("graphics/[controller]")]
    public class CircuitController : Controller
    {
        // GET api/values/5
        [HttpGet("{id}/svg")]
        public FileContentResult Svg(string id)
        {
            string path = "";
            var c = new CircuitsController().Get(id);
            if(c != null)
            {
                path = "M" + string.Join(" L", c.Map.CheckPoints.Select(p => $"{p.x},{p.y}")) + " z";
            }

            var data = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<!DOCTYPE svg PUBLIC ""-//W3C//DTD SVG 1.1//EN"" ""http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd"">
<svg xmlns=""http://www.w3.org/2000/svg"" width=""150px"" height=""100px"" viewBox=""0 0 150 100"" preserveAspectRatio=""xMidYMid meet"">
    <title>Circuit</title>
    <g id=""main"">
        <path stroke=""#666666"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""20"" fill=""transparent"" d=""" + path + @""" />
        <path stroke=""#999999"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""15"" fill=""transparent"" d=""" + path + @""" />
    </g>
</svg>";
            return new FileContentResult(System.Text.Encoding.UTF8.GetBytes(data), "image/svg+xml");
        }
    }
}
