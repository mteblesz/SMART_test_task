
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMAWarehouse.DTOs;
using TMAWarehouse.Services;

namespace TMAWarehouse.Pages;
public class ItemListsModel : PageModel
{
    public List<ItemDto> Items { get; set; } = null!;

    private readonly IItemsService _itemsService;

    public ItemListsModel(IItemsService itemsService)
    {
        _itemsService = itemsService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Items = await _itemsService.GetItems();

        return Page();
    }

}
