using DevInSales.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet]
        public ActionResult GetAll(string? name)
        {
            var statesList = _stateService.GetAll(name);
            if (statesList == null || statesList.Count == 0)
                return NoContent();

            return Ok(statesList);
        }
        [HttpGet("{stateId}")]
        public ActionResult GetByStateId(int stateId)
        {
            var state = _stateService.GetByStateId(stateId);

            if (state == null)
                return NotFound();

            return Ok(state);
        }

        [HttpGet("{stateId}/city")]
        public ActionResult GetCityByStateId(int stateId, string? name)
        {
            var state = _stateService.GetByStateId(stateId);
            if (state == null)
                return NotFound();

            var citiesList = _stateService.GetCityByStateId(stateId, name);
            if (citiesList == null || citiesList.Count == 0)
                return NoContent();

            return Ok(citiesList);
        }
    }
}
