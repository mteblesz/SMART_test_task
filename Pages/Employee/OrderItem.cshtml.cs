using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;
using TMAWarehouse.Services;

namespace TMAWarehouse.Pages.Employee;

public class OrderItemModel : PageModel
{
    private readonly IRequestsService _requestsService;
    public OrderItemModel(IRequestsService requestsService)
    {
        _requestsService = requestsService;
    }

    [BindProperty]
    [Required]
    [StringLength(255, ErrorMessage = "Employee name cannot exceed {1} characters.")]
    public string EmployeeName { get; set; } = "";

    [BindProperty]
    public AddRequestDto Item { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Item = await _requestsService.GetItemInfo(id);

        return Page();
    }

    public async Task<IActionResult> OnPostSubmitOrderAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        try
        {
            await _requestsService.OrderItem(Item, EmployeeName); 
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("Item", ex.Message);
            return Page();
        }

        return RedirectToPage("/Employee/RequestCreated");

    }
}
