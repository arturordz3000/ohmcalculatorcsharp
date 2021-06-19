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
    public class ColorSelectorConfigurationController : ControllerBase
    {
        private readonly IOhmCalculatorUnitOfWork unitOfWork;

        public ColorSelectorConfigurationController(IOhmCalculatorUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<ColorSelectorConfiguration> Get()
        {
            var configurations = unitOfWork.ColorSelectorConfigurationsRepository.Get(includeProperties: "Colors");
            return configurations;
        }
    }
}