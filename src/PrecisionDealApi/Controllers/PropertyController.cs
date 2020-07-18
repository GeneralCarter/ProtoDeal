using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using PrecisionDealApi.Models;
using PrecisionDealApi.Services;

namespace PrecisionDealApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly ILogger<PropertyController> _logger;
        private readonly UserContext _userContext;

        private readonly PropertyService _propertyService;

        public PropertyController(ILogger<PropertyController> logger, UserContext userContext, PropertyService propertyService)
        {
            _logger = logger;
            _userContext = userContext;
            _propertyService = propertyService;
        }

        [HttpPost]
        [Route("Save")]
        public string Save(Property property)
        {
            return _propertyService.Create(property).Id;
        }

        [HttpGet("{id}")]
        public Property Get(string id)
        {
            return _propertyService.Get(id);
        }
    }
}
