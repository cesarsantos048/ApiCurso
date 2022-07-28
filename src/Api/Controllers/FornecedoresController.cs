using Api.Dto;
using Api.Extensions;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/fornecedores")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository,
                                      IEnderecoRepository enderecoRepository,
                                      IFornecedorService fornecedorService,
                                      INotificador notificador,
                                      IMapper mapper
                                      ) :base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
            
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<FornecedorDto>> ObterTodos()
        {
            var fornecedor = _mapper.Map<IEnumerable<FornecedorDto>>(await _fornecedorRepository.ObterTodos());

            return fornecedor;
        }

        [AllowAnonymous]
        [HttpGet("ativos")]
        public async Task<List<FornecedorDto>> ObterAtivos()
        {
            var fornecedor = _mapper.Map<List<FornecedorDto>>(await _fornecedorRepository.ObterAtivos());

            return fornecedor;
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorDto>> ObterPorId(Guid id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(id);

            if (fornecedor == null) return NotFound();

            return Ok(fornecedor);
        }

        [ClaimsAuthorize("Fornecedores", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<FornecedorDto>> Adicionar(FornecedorDto fornecedorDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDto);
            await _fornecedorService.Adicionar(fornecedor);

            return CustomResponse(fornecedorDto);
        }

        [ClaimsAuthorize("Fornecedores", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorDto>> Atualizar(Guid id, FornecedorDto fornecedorDto)
        {
            if (id != fornecedorDto.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(fornecedorDto);
            }
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedorDto));
            
            return CustomResponse(fornecedorDto);
        }

        [ClaimsAuthorize("Fornecedores", "Remover")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorDto>> Excluir(Guid id)
        {
            var fornecedorDto = await ObterFornecedorEndereco(id);
            if (fornecedorDto == null) return NotFound();

            await _fornecedorService.Remover(id);

            return CustomResponse(fornecedorDto);
        }

        [HttpGet("obter-endereco/{id:guid}")]
        public async Task<EnderecoDto> ObterEnderecoPorId(Guid id)
        {
            return _mapper.Map<EnderecoDto>(await _enderecoRepository.ObterPorId(id));
        }

        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<ActionResult<FornecedorDto>> AtualizarEndereco(Guid id, EnderecoDto enderecoDto)
        {
            if (id != enderecoDto.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(enderecoDto);
            }
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(enderecoDto));

            return CustomResponse(enderecoDto);
        }

        private async Task<FornecedorDto> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorDto>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }

        private async Task<FornecedorDto> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorDto>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}
