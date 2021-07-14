using MentoriaDevSTI3.Data.Context;
using MentoriaDevSTI3.Data.Entidades;
using MentoriaSTI3.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MentoriaSTI3.Business
{
    public class ClienteBusiness
    {
        private readonly MentoriaDevSTI3Context _context;

        public ClienteBusiness()
        {
            _context = new MentoriaDevSTI3Context();
        }

        public void Adicionar(ClienteViewModel clienteViewModel)
        {
            _context.Clientes.Add(new Clientes
            {
                Nome = clienteViewModel.Nome,
                DataNascimento = clienteViewModel.DataNascimento,
                Endereco = clienteViewModel.Endereco,
                Cidade = clienteViewModel.Cidade,
                Cep = clienteViewModel.Cep,
            });

            _context.SaveChanges();

        }

        public void Alterar(ClienteViewModel clienteViewModel)
        {
            var cliente = _context.Clientes.Find(clienteViewModel.Id);

            cliente.Nome = clienteViewModel.Nome;
            cliente.DataNascimento = clienteViewModel.DataNascimento;
            cliente.Endereco = clienteViewModel.Endereco;
            cliente.Cidade = clienteViewModel.Cidade;
            cliente.Cep = clienteViewModel.Cep;

            _context.SaveChanges();
        }

        public void Remover(long id)
        {
            var cliente = _context.Clientes.First(x => x.Id == id);

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
        }

        public List<ClienteViewModel> Listar()
        {
            return _context.Clientes.AsNoTracking()
                .Select(s => new ClienteViewModel
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    DataNascimento = s.DataNascimento,
                    Endereco = s.Endereco,
                    Cidade = s.Cidade,
                    Cep = s.Cep
                }).ToList()
                .OrderBy(x => x.Nome).ToList();


        }
    }
}
