using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrecisionDealApi.Models;
using PrecisionDealApi.Services;

namespace PrecisionDealApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly ILogger<PropertiesController> _logger;
        private readonly UserContext _userContext;

        private readonly PropertyService _propertyService;

        public PropertiesController(ILogger<PropertiesController> logger, UserContext userContext, PropertyService propertyService)
        {
            _logger = logger;
            _userContext = userContext;
            _propertyService = propertyService;
        }

        [HttpPost]
        [Route("save")]
        public string Save(Property property)
        {
            return _propertyService.Create(property).Id;
        }

        [HttpPost]
        [Route("save/{id}")]
        public void Update(string id, Property property)
        {
            _propertyService.Update(property);
        }

        [HttpGet("{id}")]
        public Property Get(string id)
        {
            return _propertyService.Get(id);
        }

        [HttpGet("")]
        public List<Property> GetProperties()
        {
            return _propertyService.GetByUserId(_userContext.CurrentUser.Id);
        }
    }
}
