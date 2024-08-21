using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationApp.Models;
using NotificationApp.SignalR;
using System.Diagnostics;

namespace NotificationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IHubContext<ChatHub> _chatHubContext;

        public HomeController(ILogger<HomeController> logger, IHubContext<NotificationHub> notificationHubContext, IHubContext<ChatHub> chatHubContext)
        {
            _logger = logger;
            _notificationHubContext = notificationHubContext;
            _chatHubContext = chatHubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification(string message, string type)
        {
            await _notificationHubContext.Clients.All.SendAsync("ReceiveMessage", message, type);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string userId, string message)
        {
            await _chatHubContext.Clients.All.SendAsync("ReceiveMessage", userId, message);
            return Ok();
        }
    }
}
