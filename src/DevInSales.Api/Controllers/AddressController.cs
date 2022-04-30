using DevInSales.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet]
        public ActionResult GetAll(int? stateId, int? cityId, string? street, string? cep)
        {
            var addresses = _addressService.GetAll(stateId, cityId, street, cep);
            if (addresses == null || addresses.Count == 0)
                return NoContent();
            
            return Ok(addresses);
        }
    }
}