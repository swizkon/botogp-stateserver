using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotoGP.Domain.Services;
using BotoGP.stateserver.Controllers;
using BotoGP.stateserver.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BotoGP.Web.Pages;

public class PracticeModel : PageModel
{
    private readonly ICircuitRepository _circuitRepository;

    public PracticeModel(ICircuitRepository circuitRepository)
    {
        _circuitRepository = circuitRepository;
    }

    public string Message { get; set; }

    public BotoGP.stateserver.Models.Circuit Circuit { get; set; }

    public void OnGet(string id)
    {
        Circuit = new CircuitsController(_circuitRepository).Get(id);
        Message = id;
    }
}