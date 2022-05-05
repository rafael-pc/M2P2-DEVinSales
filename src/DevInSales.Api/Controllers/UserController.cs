using DevInSales.Core.Entities;
using DevInSales.EFCoreApi.Api.DTOs.Request;
using DevInSales.EFCoreApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> ObterUsers(string? nome, string? DataMin, string? DataMax)
        {
        
        var users = _userService.ObterUsers(nome, DataMin, DataMax);
            if (users == null || users.Count == 0)
                return NoContent();

        var ListaDto = users.Select(user => UserResponse.ConverterParaEntidade(user)).ToList();

            return Ok(users);
        }

        [HttpPost]
        public ActionResult CriarUser(User user)
        {
            var id = _userService.CriarUser(user);

            if(id == 0){
                return BadRequest();
            }

            return CreatedAtAction(nameof(ObterUserPorId),new {id = id}, id);
        }
    }
}