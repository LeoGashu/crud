using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using CadastroRepository.Interfaces;
using CrudBasico.Extensions;
using CrudBasico.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrudBasico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        readonly IPessoaRepository pessoaRepository;

        public PessoasController(IPessoaRepository pessoaRepository)
        {
            this.pessoaRepository = pessoaRepository;
        }

        // GET: api/<PessoasController>
        [HttpGet]
        public async Task<IEnumerable<PessoaViewModel>> Get([FromQuery] bool ativo = true, string nome = null)
        {
            return (await pessoaRepository.GetPessoasAsync(ativo, nome)).ToPessoaViewModels();
        }

        // GET api/<PessoasController>/5
        [HttpGet("{id}")]
        public async Task<PessoaViewModel> Get(Guid id)
        {
            return (await pessoaRepository.GetPessoaByIdAsync(id)).ToPessoaViewModel();
        }

        // POST api/<PessoasController>
        [HttpPost]
        public async Task<PessoaViewModel> Post([FromBody] PessoaViewModel pessoaViewModel)
        {
            if (pessoaViewModel.Id != Guid.Empty)
            {
                await pessoaRepository.UpdatePessoaAsync(pessoaViewModel);
            }
            else {
                pessoaViewModel.Id = Guid.NewGuid();
                await pessoaRepository.InsertPessoaAsync(new List<PessoaViewModel>() { pessoaViewModel });
            }

            return await Get(pessoaViewModel.Id);
        }

        // DELETE api/<PessoasController>/5
        [HttpDelete("{id}")]
        public async Task<int> Delete(Guid id)
        {
            return await pessoaRepository.DeletePessoaAsync(id);
        }
    }
}
