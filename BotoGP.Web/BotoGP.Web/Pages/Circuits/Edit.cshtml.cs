using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

using BotoGP.stateserver.Controllers;
using BotoGP.Domain.Services;

namespace BotoGP.Web.Pages.Circuits;

public class EditModel : PageModel
{
    public string Message { get; set; }

    private readonly ICircuitRepository _circuitRepository;

    public BotoGP.stateserver.Models.Circuit Circuit { get; set; }

    public EditModel(ICircuitRepository circuitRepository)
    {
        _circuitRepository = circuitRepository;
    }

    public void OnGet(string id)
    {
        Circuit = new CircuitsController(_circuitRepository).Get(id);
        Message = "Your application description page ." + id;
    }
}