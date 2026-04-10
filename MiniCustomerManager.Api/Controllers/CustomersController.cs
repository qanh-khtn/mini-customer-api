using Microsoft.AspNetCore.Mvc;
using MiniCustomerManager.Api.Services;

namespace MiniCustomerManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomersController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var customers = _customerService.GetAll().Select(c => new
        {
            c.Id,
            c.FullName,
            c.MembershipTier,
            c.JoinYear,
            c.RewardPoints,
            Status = _customerService.GetCustomerStatus(c.RewardPoints) 
        });

        return Ok(customers);
    }

    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        return Ok(_customerService.GetStats());
    }
}