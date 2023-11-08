using Microsoft.AspNetCore.Mvc;
using EspacioModels;
using EspacioRepositorios;

namespace tl2_tp09_2023_LucianoCV01.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private UsuarioRepository manejoDeUsuarios; // Utilizo la clase o la interfaz?
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        manejoDeUsuarios = new();
    }

    [HttpPost("api/usuario")] //Que devuelvo???
    public ActionResult<Usuario> AgregarUsuario(Usuario u)
    {
        manejoDeUsuarios.Create(u);
        return Ok(u);
    }
    [HttpGet]
    [Route("api/usuario")]
    public ActionResult<List<Usuario>> GetListadoUsuario()
    {
        var listaUsuarios = manejoDeUsuarios.GetAll();
        return Ok(listaUsuarios);
    }
    [HttpGet]
    [Route("api/usuario/{Id}")]
    public ActionResult<Usuario> GetUsuarioPorId(int Id)
    {
        var usuarioBuscado = manejoDeUsuarios.GetById(Id);
        return Ok(usuarioBuscado);
    }
    [HttpPut("api/tarea/{Id}/Nombre")] //Que devuelvo???
    public ActionResult<Usuario> ActualizarTarea(int Id, Usuario usuarioActualizado)
    {
        manejoDeUsuarios.Update(Id, usuarioActualizado);
        return Ok(usuarioActualizado);
    }
}