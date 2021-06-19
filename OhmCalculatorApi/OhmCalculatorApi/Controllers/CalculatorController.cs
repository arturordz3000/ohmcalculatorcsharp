using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OhmCalculatorApi.Abstractions;

namespace OhmCalculatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly IOhmCalculatorService<string> calculatorService;

        public CalculatorController(IOhmCalculatorService<string> calculatorService)
        {
            this.calculatorService = calculatorService;
        }

        /// <summary>
        /// Computes the value of a resistor in Ohms based on the given color ids.
        /// </summary>
        /// <param name="firstId">Color id for the first band.</param>
        /// <param name="secondId">Color id for the second band.</param>
        /// <param name="multiplierId">Color id for the third (multiplier) band.</param>
        /// <param name="toleranceId">Color id for the fourth (tolerance) band.</param>
        /// <returns>A <see cref="string"/> indicating the final ohm value.</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult Calculate(int firstId, int secondId, int multiplierId, int toleranceId)
        {
            return new JsonResult(new { result = calculatorService.Calculate(firstId, secondId, multiplierId, toleranceId) });
        }
    }
}