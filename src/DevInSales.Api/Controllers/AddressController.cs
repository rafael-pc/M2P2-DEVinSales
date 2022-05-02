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

        public AddressController(
            IAddressService addressService,
            IStateService stateService,
            ICityService cityService
        )
        {
            _addressService = addressService;
            _stateService = stateService;
            _cityService = cityService;
        }

        /// <summary>
        /// Buscar endereços.
        /// </summary>
        /// <remarks>
        /// Pesquisas opcionais: stateId, cityId, street, cep.
        /// <para>
        /// Exemplo de resposta:
        /// [
        ///   {
        ///     "street": "Rua Devin",
        ///     "number": "100",
        ///     "complement": "Apto. 101",
        ///     "cep": "95800000"
        ///     "city": {
        ///         "id": 1,
        ///         "name": "Jaraguá do Sul"
        ///     },
        ///     "state": {
        ///         "id": 1,
        ///         "name": "Santa Catarina"
        ///         "initials": "SC"
        ///   }
        /// ]
        /// </para>
        /// </remarks>
        /// <returns>Lista de endereços</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Pesquisa realizada com sucesso porém não retornou nenhum resultado</response>
        [HttpGet]
        public ActionResult GetAll(int? stateId, int? cityId, string? street, string? cep)
        {
            var addresses = _addressService.GetAll(stateId, cityId, street, cep);
            if (addresses == null || addresses.Count == 0)
                return NoContent();

            return Ok(addresses);
        }

        /// <summary>
        /// Cadastrar um endereço.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        /// {
        ///     "street": "Rua Devin",
        ///     "number": "100",
        ///     "complement": "Apto. 101",
        ///     "cep": "95800000",
        /// }
        /// </remarks>
        /// <param name="model">Dados do endereço</param>
        /// <returns>Id do endereço criado</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="201">Cadastrado com sucesso.</response>
        /// <response code="400">Bad Request, stateId informado é diferente do stateId da cidade cadastrada no banco de dados.</response>
        /// <response code="404">Not Found, estado não encontrado no stateId informado.</response>
        /// <response code="404">Not Found, cidade não encontrada no cityId informado.</response>
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

            var address = new Address(
                model.Street,
                model.Cep,
                model.Number,
                model.Complement,
                cityId
            );
            _addressService.Add(address);

            return CreatedAtAction(nameof(GetAll), new { stateId, cityId }, address.Id);
        }

        /// <summary>
        /// Deletar um endereço
        /// </summary>
        /// <response code="204">Endereço deletado com sucesso</response>
        /// <response code="400">Bad Request, não é possível deletar este endereço pois ele está na lista de entrega</response>
        /// <response code="404">Not Found, endereço não encontrado.</response>
        [HttpDelete("/address/{addressId}")]
        public ActionResult DeleteAddress(int addressId)
        {
            var address = _addressService.GetById(addressId);

            if (address == null)
                return NotFound();

            _addressService.Delete(address);

            return NoContent();
        }
    }
}
