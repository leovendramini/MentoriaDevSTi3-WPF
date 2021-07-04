using MentoriaDevSTI3.Data.Context;
using MentoriaDevSTI3.Data.Entidades;
using MentoriaSTI3.View.UserControls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MentoriaSTI3.View
{
    /// <summary>
    /// Interaction logic for Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();
            AplicarMigration(); // Não recomendado, pois causa problemas de performance apenas um teste de vericação das migrações do banco
        }



        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            InicializarUc(sender);
        }

        private void AplicarMigration()
        {
            using var context = new MentoriaDevSTI3Context();
            context.AplicarMigration();
        }


        private void InicializarUc(object sender)
        {
            if(sender is Button btn)
            {
                switch (btn.Name)
                {
                    case nameof(BtnProdutos):
                        Conteudo.Content = new UcProdutos();
                        break;

                    case nameof(BtnClientes):
                        Conteudo.Content = new UcClientes();
                        break;

                    case nameof(BtnPedidos):
                        Conteudo.Content = new UcPedido();
                        break;
                    default:
                        break;
                }
            }
        }

        //private void Testes()
        //{
        //    using var context = new MentoriaDevSTI3Context();

        //    //context.Database.EnsureCreated(); //Code-first

        //    //context.Clientes.Add(new Clientes {
        //    //    Nome = "Leo",
        //    //    Cep = "17203550",
        //    //    Cidade = "Jau",
        //    //    DataNascimento = DateTime.Now,
        //    //    Endereco = "Rua J",
        //    //});

        //    var clientes = context.Clientes.First(x => x.Id == 1);
        //    context.Clientes.Remove(clientes);
        //    context.SaveChanges();

        //}
    }
}
