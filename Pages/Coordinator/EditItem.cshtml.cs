using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;
using TMAWarehouse.Services;

namespace TMAWarehouse.Pages.Coordinator
{
    public class EditItemModel : PageModel
    {
        private readonly IItemsService _itemsService;
        private readonly IEnumsService _enumsService;

        public EditItemModel(IItemsService itemsService, IEnumsService enumsService)
        {
            _itemsService = itemsService;
            _enumsService = enumsService;
        }

        public List<MeasurementUnitDto> MeasurementUnits { get; private set; } = [];
        public List<ItemGroupDto> ItemGroups { get; private set; } = [];
        public List<ItemStatusDto> ItemStatuses { get; private set; } = [];

        [BindProperty]
        public EditItemDto Item { get; set; } = null!;

        [BindProperty]
        public IFormFile? PhotoFile { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            MeasurementUnits = await _enumsService.GetMeasurementUnits();
            ItemGroups = await _enumsService.GetItemGroups();
            ItemStatuses = await _enumsService.GetItemStatuses();

            Item = await _itemsService.GetItemToEdit(id);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteItem()
        {
            try
            {
                await _itemsService.DeleteItem(Item.ItemId);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Deletion", ex.Message);
                return Page();
            }

            return RedirectToPage("/Coordinator/ItemList");
        }

        public async Task<IActionResult> OnPost()
        {
            if (PhotoFile != null && PhotoFile.Length > 0)
            {
                if (PhotoFile.ContentType != "image/jpeg")
                    ModelState.AddModelError("PhotoFile", "Invalid file type. Only JPEG files are allowed.");

                if (PhotoFile.Length > 1000000)
                    ModelState.AddModelError("PhotoFile", "The photo size must be less than 1 MB.");
            }

            if (!ModelState.IsValid)
            {
                // Repopulate lists 
                MeasurementUnits = await _enumsService.GetMeasurementUnits();
                ItemGroups = await _enumsService.GetItemGroups();
                ItemStatuses = await _enumsService.GetItemStatuses();

                return Page();
            }

            if (PhotoFile != null && PhotoFile.Length > 0)
            {
                var memoryStream = new MemoryStream();
                await PhotoFile.CopyToAsync(memoryStream);
                Item.PhotoBinary = memoryStream.ToArray();
            }

            await _itemsService.UpdateItem(Item);

            return RedirectToPage("/Coordinator/ItemList");
        }
    }
}
