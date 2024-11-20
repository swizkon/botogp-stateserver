using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotoGP.Domain.Services;
using BotoGP.stateserver.Controllers;
using BotoGP.stateserver.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BotoGP.Web.Pages.Circuits;

public class IndexModel : PageModel
{
    public string Message { get; set; }
    public IEnumerable<Circuit> Circuits { get; set; }

    private readonly ICircuitRepository _circuitRepository;

    public IndexModel(ICircuitRepository circuitRepository)
    {
        _circuitRepository = circuitRepository;
    }

    public void OnGet()
    {
        Message = System.Guid.NewGuid().ToString();
        Circuits = new CircuitsController(_circuitRepository).Get();
    }
}