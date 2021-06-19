using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OhmCalculatorApi.Abstractions;
using OhmCalculatorApi.Models;

namespace OhmCalculatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorSelectorConfigurationsController : ControllerBase
    {
        private readonly IOhmCalculatorUnitOfWork unitOfWork;

        public ColorSelectorConfigurationsController(IOhmCalculatorUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the colors list for each selector (dropdown) displayed in the Front
        /// End application.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IEnumerable<ColorSelectorConfiguration> Get()
        {
            return unitOfWork.ColorSelectorConfigurationsRepository.Get(includeProperties: "Colors");
        }
    }
}