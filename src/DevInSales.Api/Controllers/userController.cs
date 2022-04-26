using DevInSales.EFCoreApi.Entities;
using DevInSales.EFCoreApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.EFCoreApi.Api.Controllers
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
        public ActionResult<List<User>> ObterUsers(string? titulo)
        {
            var users = _userService.ObterUsers(titulo);
            if (users == null || users.Count == 0)
                return NoContent();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> ObterUserPorId(int id)
        {
            var user = _userService.ObterPorId(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public ActionResult CriarUser(User user)
        {
            var id = _userService.CriarUser(user);
            return CreatedAtAction(nameof(ObterUserPorId), new { Id = id }, id);
        }

        [HttpPut("{id}")]
        public ActionResult AtualizarUser(int id, User user)
        {
            var userOriginal = _userService.ObterPorId(id);
            if (user == null)
                return NotFound();

            _userService.AtualizarLivro(userOriginal, user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult ExcluirUser(int id)
        {
            try
            {
                _userService.RemoverUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("usuario não existe"))
                    return NotFound();
                
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }
    }
}