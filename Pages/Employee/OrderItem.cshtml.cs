using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;
using TMAWarehouse.Services;

namespace TMAWarehouse.Pages.Employee;

public class OrderItemModel : PageModel
{

    private readonly IItemsService _itemsService;
    private readonly IRequestsService _requestsService;
    public OrderItemModel(IItemsService itemsService, IRequestsService requestsService)
    {
        _itemsService = itemsService;
        _requestsService = requestsService;
    }

    [BindProperty]
    public RequestItemDto Item { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Item = await _itemsService.GetItemToOrder(id);

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
            await _requestsService.OrderItem(Item, "empty name"); // TODO replace string with employee name
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("Item", ex.Message);
            return Page();
        }

        return RedirectToPage("/Employee/RequestCreated");

    }
}
