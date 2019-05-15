using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TODOList.BLL.DTO;
using TODOList.BLL.Interfaces;
using TODOList.BLL.Services;

namespace TODOList.WEB.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;
        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmail([FromBody]EmailDTO email)//async Task<IActionResult> SendEmailAsync([FromBody]EmailDTO email)
        {
            //await _emailService.SendEmailAsync(email);
            _emailService.SendmailWithIcsAttachment(email);
            
            return Ok();
        }
    }
}