using SendGrid;
using SendGrid.Helpers.Mail;
using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactFormController : ControllerBase
    {
        private readonly ICommonRepository<ContactForm> _contactFormRepository;
        private readonly IEmailService _emailService; // Inject the email service

        public ContactFormController(ICommonRepository<ContactForm> contactFormRepository, IEmailService emailService)
        {
            _contactFormRepository = contactFormRepository;
            _emailService = emailService; // Initialize the email service
        }

        // Method for sending a new message (for visitors)
        [HttpPost]
        [AllowAnonymous]  // Anyone can send a message
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendMessage([FromBody] ContactForm contactForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Save the contact form message
                var newMessage = await _contactFormRepository.Insert(contactForm);
                return CreatedAtAction(nameof(GetMessageById), new { id = newMessage.MessageId }, newMessage);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Method for replying to a specific message (for Admin only)
        [HttpPost("reply/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReplyToMessage(int id, [FromBody] EmailReply reply)
        {
            try
            {
                var contactForm = await _contactFormRepository.GetDetails(id);
                if (contactForm == null)
                {
                    return NotFound("Message not found.");
                }

                // Send the reply using the email service
                var isSuccess = await _emailService.SendEmailAsync(contactForm.Email, reply.Subject, reply.Body);

                if (isSuccess)
                {
                    return Ok("Reply sent successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to send reply.");
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Method for getting all messages (for Admin only)
        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]  // Only Admins can access this
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ContactForm>>> GetAllMessages()
        {
            try
            {
                var messages = await _contactFormRepository.GetAll();
                if (messages == null || messages.Count == 0)
                {
                    return NotFound("No messages found.");
                }

                return Ok(messages);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Method for getting a specific message by ID (for Admin only)
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]  // Only Admins can access this
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ContactForm>> GetMessageById(int id)
        {
            try
            {
                var message = await _contactFormRepository.GetDetails(id);
                if (message == null)
                {
                    return NotFound("Message not found.");
                }

                // Mark the message as read
                if (!message.IsRead)
                {
                    message.IsRead = true;
                    await _contactFormRepository.Update(message);
                }

                return Ok(message);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("MarkAsRead/{id}")]
        [Authorize(Policy = "AdminPolicy")]  // Only Admins can update the read status
        public async Task<IActionResult> MarkAsRead(int id)
        {
            try
            {
                var message = await _contactFormRepository.GetDetails(id);
                if (message == null)
                {
                    return NotFound("Message not found.");
                }

                message.IsRead = true;
                await _contactFormRepository.Update(message);

                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Method to delete a message (Optional, for Admin only)
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]  // Only Admins can delete messages
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                var message = await _contactFormRepository.GetDetails(id);
                if (message == null)
                {
                    return NotFound("Message not found.");
                }

                await _contactFormRepository.Delete(id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Method to delete all messages (for Admin only)
        [HttpDelete("DeleteAll")]
        [Authorize(Policy = "AdminPolicy")]  // Only Admins can delete all messages
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAllMessages()
        {
            try
            {
                var messages = await _contactFormRepository.GetAll();
                if (messages == null || messages.Count == 0)
                {
                    return NotFound("No messages found to delete.");
                }

                foreach (var message in messages)
                {
                    await _contactFormRepository.Delete(message.MessageId);
                }

                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    // Class to model the reply email body
    public class EmailReply
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
