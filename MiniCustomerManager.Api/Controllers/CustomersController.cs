using Microsoft.AspNetCore.Mvc;
using MiniCustomerManager.Api.Models;
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
    public IActionResult GetCustomers(
        [FromQuery] string? search, 
        [FromQuery] string? sortBy = "id", 
        [FromQuery] bool isDesc = false, 
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 4) // Mặc định 4 người/trang để dễ test
    {
        // Kiểm tra dữ liệu đầu vào (tránh nhập số âm)
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 2;

        var result = _customerService.GetCustomers(search, sortBy, isDesc, page, pageSize);
        return Ok(result);
    }

    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        return Ok(_customerService.GetStats());
    }
}