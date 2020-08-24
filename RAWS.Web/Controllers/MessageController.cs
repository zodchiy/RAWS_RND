using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RAWS.Web.Services;

namespace RAWS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IHubContext<MessageHub> _myHub;
        private DataCollector _collector;
        public MessageController(IHubContext<MessageHub> myhub, DataCollector collector)
        {
            _myHub = myhub;
            _collector = collector;
        }

        public IActionResult Get()
        {
            var DataPusher = new DataPusher(() => _myHub.Clients.All.SendAsync("binance_quotas", _collector.Get()));
            return Ok();
        }
    }
}
