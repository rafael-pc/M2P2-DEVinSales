using DevInSales.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Api.Controllers
{
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;

        public CityController(IStateService stateService, ICityService cityService)
        {
            _stateService = stateService;
            _cityService = cityService;
        }

        [HttpGet("/api/State/{stateId}/city")]
        public ActionResult GetCityByStateId(int stateId, string? name)
        {
            var state = _stateService.GetById(stateId);
            if (state == null)
                return NotFound();

            var citiesList = _cityService.GetAll(stateId, name);
            if (citiesList == null || citiesList.Count == 0)
                return NoContent();

            return Ok(citiesList);
        }
        [HttpGet("/api/State/{stateId}/city/{cityId}")]
        public ActionResult GetCityById(int stateId, int cityId)
        {
            var state = _stateService.GetById(stateId);
            if (state == null)
                return NotFound();

            var city = _cityService.GetById(cityId);
            if (city == null)
                return NotFound();

            if (state.Id != city.State.Id)
                return BadRequest();

            return Ok(city);
        }
    }
}
