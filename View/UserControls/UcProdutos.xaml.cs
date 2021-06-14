using MentoriaSTI3.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UcProdutos.xaml
    /// </summary>
    public partial class UcProdutos : UserControl
    {

        private UcProdutoViewModel UcProdutoVm = new UcProdutoViewModel();

        public UcProdutos()
        {
            InitializeComponent();

            DataContext = UcProdutoVm;

            UcProdutoVm.ProdutosAdicionados = new ObservableCollection<ProdutoViewModel>();

            /*  Teste
             UcProdutoVm.ProdutosAdicionados = new ObservableCollection<ProdutoViewModel>;
             {
                 new ProdutoViewModel {Nome = "Tênis", Valor = 10 },
                 new ProdutoViewModel {Nome = "Camiseta", Valor = 10 },
                 new ProdutoViewModel {Nome = "Shorts", Valor = 10 }
             };*/
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidarProduto()) return;

            if (UcProdutoVm.Alteracao)
            {
                AlterarProduto();
            }
            else
            {

                AdicionarProduto();
            }
                LimparCampos();

        }
        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            var produto = (sender as Button).Tag as ProdutoViewModel;
            
            /* Foi criado um metodo - PreencherCampos e movido
            UcProdutoVm.Nome = produto.Nome;
            UcProdutoVm.Valor = produto.Valor;

            UcProdutoVm.Alteracao = true;*/

            PreencherCampos(produto);

        }


        private void txtValor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PreencherCampos(ProdutoViewModel produto)
        {
            UcProdutoVm.Nome = produto.Nome;
            UcProdutoVm.Valor = produto.Valor;

            UcProdutoVm.Alteracao = true;
        }

        private void AdicionarProduto()
        {
            var novoProduto = new ProdutoViewModel
            {
                Nome = UcProdutoVm.Nome,
                Valor = UcProdutoVm.Valor,

            };
            UcProdutoVm.ProdutosAdicionados.Add(novoProduto);
        }

        private void AlterarProduto()
        {
            //sera desenvolvido na aula de banco de dados
        }

        private void LimparCampos()
        {
            UcProdutoVm.Nome = "";
            UcProdutoVm.Valor = 0;
            UcProdutoVm.Alteracao = false;
        }

        private bool ValidarProduto()
        {
            if (string.IsNullOrEmpty(UcProdutoVm.Nome))
            {
                MessageBox.Show("O campo nome é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

    }
}
