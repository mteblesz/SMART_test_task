using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TMAWarehouse.DTOs;
using TMAWarehouse.Services;

namespace TMAWarehouse.Pages.Coordinator
{
    public class EditItemModel : PageModel
    {

        private readonly IItemsService _itemsService;

        public EditItemModel(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }


        public ItemDto Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _itemsService.GetItem(id);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteItem()
        {
            await _itemsService.DeleteItem(Item.ItemId);

            return RedirectToPage("/Coordinator/ItemList");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _itemsService.UpdateItem(Item);

            return RedirectToPage("/Coordinator/ItemList");
        }
    }
}
