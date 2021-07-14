using MentoriaDevSTI3.Data.Context;
using MentoriaDevSTI3.Data.Entidades;
using MentoriaSTI3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MentoriaSTI3.Business
{
   
    public class PedidoBusiness
    {
         private readonly MentoriaDevSTI3Context _context;

         public PedidoBusiness()
        {
            _context = new MentoriaDevSTI3Context();
        }

        public void Adicionar(PedidoViewModel pedidoViewModel)
        {
            _context.Pedidos.Add(new Pedido
            {
                ClienteId = pedidoViewModel.ClienteId,
                FormaPagamento = pedidoViewModel.FormaPagamento,
                Valor = pedidoViewModel.Valor,
                ItemPedido = pedidoViewModel.ItensPedido.Select(s => new ItemPedido
                {
                    ProdutoId = s.ProdutoId,
                    Quantidade = s.Quantidade,
                    Valor = s.Valor
                }).ToList()
        });
            _context.SaveChanges();
        }
    }
}
