﻿using MentoriaSTI3.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UcClientes.xaml
    /// </summary>
    public partial class UcClientes : UserControl
    {
        private UcClienteViewModel UcClienteVm = new UcClienteViewModel();

        public UcClientes()
        {
            InitializeComponent();

            DataContext = UcClienteVm;
            UcClienteVm.ClientesAdicionados = new ObservableCollection<ClienteViewModel>();
            UcClienteVm.DataNascimento = new System.DateTime(1990, 1, 1);
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCliente())
            {
                return;
            }
            if (UcClienteVm.Alteracao)
            {
                AlterarCliente();
            }
            else
            {

                AdicionarCliente();
            }
            LimparCampos();

        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            var cliente = (sender as Button).Tag as ClienteViewModel;
            PreencherCampos(cliente);
        }

        private void PreencherCampos(ClienteViewModel cliente)
        {
            UcClienteVm.Nome = cliente.Nome;
            UcClienteVm.DataNascimento = cliente.DataNascimento;
            UcClienteVm.Cep = cliente.Cep;
            UcClienteVm.Endereco = cliente.Endereco;
            UcClienteVm.Cidade = cliente.Cidade;
            UcClienteVm.Alteracao = true;
        }

        private void AdicionarCliente()
        {
            var novoCliente = new ClienteViewModel
            {
                Nome = UcClienteVm.Nome,
                DataNascimento = UcClienteVm.DataNascimento,
                Cep = UcClienteVm.Cep,
                Endereco = UcClienteVm.Endereco,
                Cidade = UcClienteVm.Cidade
                
            };

            UcClienteVm.ClientesAdicionados.Add(novoCliente);
        }

        private void AlterarCliente()
        {
            //Desenvolver na aula de banco de dados
        }

        private void LimparCampos()
        {
            UcClienteVm.Nome = "";
            UcClienteVm.DataNascimento = new System.DateTime(1990, 1, 1);
            UcClienteVm.Cep = 0;
            UcClienteVm.Endereco = "";
            UcClienteVm.Cidade = "";
            UcClienteVm.Alteracao = false;

        }

        private bool ValidarCliente()
        {
            if (string.IsNullOrEmpty(UcClienteVm.Nome))
            {
                MessageBox.Show("O campo nome é obrigatório.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }
    }
}
