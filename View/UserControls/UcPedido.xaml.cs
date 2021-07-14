using MentoriaSTI3.Business;
using MentoriaSTI3.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MentoriaSTI3.View.UserControls
{
    /// <summary>
    /// Interaction logic for UcPedido.xaml
    /// </summary>
    public partial class UcPedido : UserControl
    {
        private UcPedidoViewModel UcPedidoVm = new UcPedidoViewModel();

        public UcPedido()
        {
            InitializeComponent();
            InicializarOperacao();
        }

        private void CmbProduto_DropDownClosed(object sender, EventArgs e)
        {
            if(sender is ComboBox cmb && cmb.SelectedItem is ProdutoViewModel produto)
            {
                UcPedidoVm.ValorUnit = produto.Valor;
            }
        }

        private void btnAdicionarItem_Click(object sender, RoutedEventArgs e)
        {
            AdicionarItem();
            LimparCampo();
        }

        private void BtnFinalizarPedido_Click(object sender, RoutedEventArgs e)
        {
            FinalizarPedido();
        }

        private void InicializarOperacao()
        {
            DataContext = UcPedidoVm;

            UcPedidoVm.ListaClientes = new ObservableCollection<ClienteViewModel> ( new ClienteBusiness().Listar());
            UcPedidoVm.ListaProduto = new ObservableCollection<ProdutoViewModel>( new ProdutoBusiness().Listar());
            UcPedidoVm.ListaPagamentos = new ObservableCollection<string>
            {
                "Dinheiro",
                "Boleto",
                "Cartão de Crédito",
                "Cartão de Débito",
                "PIX",
            };

            UcPedidoVm.Quantidade = 1;
            UcPedidoVm.ItensAdicionados = new ObservableCollection<UcPedidoItemViewModel>();
        }

        private void AdicionarItem()
        {
            var produtoSelecionado = CmbProduto.SelectedItem as ProdutoViewModel;

            var itemVM = new UcPedidoItemViewModel
            {
                Nome = produtoSelecionado.Nome,
                Quantidade = UcPedidoVm.Quantidade,
                ValorUnit = UcPedidoVm.ValorUnit,
                ValorTotalItem = UcPedidoVm.Quantidade * UcPedidoVm.ValorUnit,
                ProdutoId = produtoSelecionado.Id
                
            };

            UcPedidoVm.ItensAdicionados.Add(itemVM);

            UcPedidoVm.ValorTotalPedido = UcPedidoVm.ItensAdicionados.Sum(i => i.ValorTotalItem);

            LimparCamposProduto();

        }


        private void LimparCamposProduto()
        {
            UcPedidoVm.Quantidade = 1;
            CmbProduto.SelectedItem = null;
            UcPedidoVm.ValorUnit = 0;
        }

        private void LimparTodosCampos()
        {
            UcPedidoVm.ItensAdicionados = new ObservableCollection<UcPedidoItemViewModel>();
            UcPedidoVm.ValorTotalPedido = 0;
            CmbCliente.SelectedItem = null;
            CmbFormaPagamento.SelectedItem = null;

            LimparCamposProduto();
        }
        private void FinalizarPedido()
        {
            var clienteSelecionado = CmbCliente.SelectedItem as ClienteViewModel;
            var formaPagamentoSelecionado = CmbFormaPagamento.SelectedItem as string;
            var pedidoViewModel = new PedidoViewModel
            {
                ClienteId = clienteSelecionado.Id,
                FormaPagamento = formaPagamentoSelecionado,
                Valor = UcPedidoVm.ValorTotalPedido,
                ItensPedido = UcPedidoVm.ItensAdicionados.Select(x => new ItensPedidoViewModel
                {
                    ProdutoId = x.ProdutoId,
                    Quantidade = x.Quantidade,
                    Valor = x.ValorTotalItem

                }).ToList()
            };

            new PedidoBusiness().Adicionar(pedidoViewModel);
            MessageBox.Show("Pedido realizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            LimparTodosCampos();
        }

        private void LimparCampo()
        {
            UcPedidoVm.Quantidade = 1;

        }
    }
}
