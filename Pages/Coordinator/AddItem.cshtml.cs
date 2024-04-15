using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;
using TMAWarehouse.Services;

namespace TMAWarehouse.Pages.Coordinator
{
    public class AddItemModel : PageModel
    {
        private readonly IItemsService _itemsService;
        private readonly IEnumsService _enumsService;

        public AddItemModel(IItemsService itemsService, IEnumsService enumsService)
        {
            _itemsService = itemsService;
            _enumsService = enumsService;
        }

        public List<MeasurementUnitDto> MeasurementUnits { get; private set; } = [];
        public List<ItemGroupDto> ItemGroups { get; private set; } = [];
        public List<ItemStatusDto> ItemStatuses { get; private set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            MeasurementUnits = await _enumsService.GetMeasurementUnits();
            ItemGroups = await _enumsService.GetItemGroups();
            ItemStatuses = await _enumsService.GetItemStatuses();

            return Page();
        }


        [BindProperty]
        public AddItemDto Item { get; set; } = null!;
        [BindProperty]
        public IFormFile? PhotoFile { get; set; }

        public async Task<IActionResult> OnPostAddItemAsync()
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
            await _itemsService.AddItem(Item);

            return RedirectToPage("/Coordinator/ItemList");
        }
    }
}
