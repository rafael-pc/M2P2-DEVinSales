using DevInSales.Api.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AddressController : ControllerBase
  {
    private readonly IAddressService _addressService;
    private readonly IStateService _stateService;
    private readonly ICityService _cityService;

    public AddressController(IAddressService addressService, IStateService stateService, ICityService cityService)
    {
      _addressService = addressService;
      _stateService = stateService;
      _cityService = cityService;
    }

    [HttpGet]
    public ActionResult GetAll(int? stateId, int? cityId, string? street, string? cep)
    {
      var addresses = _addressService.GetAll(stateId, cityId, street, cep);
      if (addresses == null || addresses.Count == 0)
        return NoContent();

      return Ok(addresses);
    }

    [HttpPost("/state/{stateId}/city/{cityId}/address")]
    public ActionResult AddAddress(int stateId, int cityId, AddAddress model)
    {
      var state = _stateService.GetById(stateId);
      if (state == null)
        return NotFound();

      var city = _cityService.GetById(cityId);
      if (city == null)
        return NotFound();

      if (city.State.Id != stateId)
        return BadRequest();

      var address = new Address(model.Street, model.Cep, model.Number, model.Complement, cityId);
      _addressService.Add(address);

      return CreatedAtAction(nameof(GetAll), new { stateId, cityId }, address.Id);
    }

    [HttpPatch("{addressId}")]
    public ActionResult UpdateAddress(int addressId, UpdateAddress model)
    {
      if (model.Street == null && model.Cep == null && model.Number == null && model.Complement == null)
        return BadRequest();

      var address = _addressService.GetById(addressId);
      if (address == null)
        return NotFound();

      if (model.Street != null)
        address.Street = model.Street;

      if (model.Cep != null)
        address.Cep = model.Cep;

      if (model.Number != null)
        address.Number = model.Number.Value;

      if (model.Complement != null)
        address.Complement = model.Complement;

      _addressService.Update(address);
      return NoContent();
    }
  }
}