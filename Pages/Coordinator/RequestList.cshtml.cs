using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMAWarehouse.DTOs;
using TMAWarehouse.Services;

namespace TMAWarehouse.Pages.Coordinator
{
    public class RequestListModel : PageModel
    {
        public List<RequestDto> Requests { get; set; } = null!;

        private readonly IRequestsService _requestsService;

        public RequestListModel(IRequestsService requestsService)
        {
            _requestsService = requestsService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Requests = await _requestsService.GetRequests();

            return Page();
        }
        public async Task<IActionResult> OnPostConfirmAsync(int id)
        {
            // Repopulate
            Requests = await _requestsService.GetRequests();
            try
            {
                await _requestsService.ConfirmRequest(id);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Request", ex.Message);
                Requests = await _requestsService.GetRequests();
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectAsync(int id)
        {
            // Repopulate
            Requests = await _requestsService.GetRequests();
            try
            {
                await _requestsService.RejectRequest(id);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Request", ex.Message);
                return Page();
            }
            return RedirectToPage();
        }
    }
}
