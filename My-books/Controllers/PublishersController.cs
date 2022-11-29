using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_books.ActionResults;
using My_books.Data.Models;
using My_books.Data.Services;
using My_books.Data.ViewModels;
using My_books.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublisherService _publisherService;
        public PublishersController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var publisherCreated = _publisherService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), publisherCreated);
            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher Name : {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("get-publisher-books-and-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var publisher = _publisherService.GetPublisherData(id);
            return Ok(publisher);
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public CustomActionResult GetPublisherById(int id)
        {
            //throw new Exception("This is an exception that will be handled by middleware");

            var publisher = _publisherService.GetPublisherById(id);
            if (publisher != null)
            {
                //return Ok(publisher);
                var _resonseObject = new CustomActionResultVM() { Publisher = publisher };
                return new CustomActionResult(_resonseObject);
                //return publisher;
            }
            else
            {
                //return NotFound();
                var _resonseObject = new CustomActionResultVM() { Exception = new Exception("This is coming from PublishersController")};
                return new CustomActionResult(_resonseObject);
            }
            
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisher(int id)
        {
             _publisherService.DeletePublisherById(id);
            return Ok();
        }
    }
}
