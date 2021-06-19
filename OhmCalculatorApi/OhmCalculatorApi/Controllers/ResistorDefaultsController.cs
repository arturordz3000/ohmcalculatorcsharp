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
    public class ResistorDefaultsController : ControllerBase
    {
        private readonly IOhmCalculatorUnitOfWork unitOfWork;

        public ResistorDefaultsController(IOhmCalculatorUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<ResistorDefault> Get()
        {
            return unitOfWork.ResistorDefaultsRepository.Get(includeProperties: "Color");
        }
    }
}