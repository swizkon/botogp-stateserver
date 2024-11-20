using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotoGP.stateserver.Models;
//using BotoGP.stateserver.Repos;
using BotoGP.Web;
//using BotoGP.Projections;
using Microsoft.AspNetCore.Mvc;

using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace BotoGP.Web.Controllers;

[Route("api/[controller]")]
public class EnvironmentController : Controller
{
	public IConfiguration Config { get; }

	public EnvironmentController(IConfiguration config)
	{
		Config = config;
	}


	// GET: api/values
	[HttpGet]
	public IEnumerable<string> Get()
	{
		return new[]
		{
			"Config keys: " + string.Join(", ", Config.AsEnumerable().Select(x => x.Key)),
			"AppSettings keys: " + string.Join(", ", Config.GetSection("AppSettings").AsEnumerable().Select(x => x.Key))
		};
	}
}