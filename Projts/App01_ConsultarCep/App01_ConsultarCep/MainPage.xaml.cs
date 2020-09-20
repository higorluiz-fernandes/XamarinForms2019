using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCep.Servico.Modelo;
using App01_ConsultarCep.Servico;
using System.Net;

namespace App01_ConsultarCep
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            //TO DO - Validações
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if(end != null)
                    {
                        RESULTADO.Text = String.Format("Endereço: {2} - {3} - {0}/{1}", end.Localidade, end.Uf, end.Logradouro, end.Bairro);
                    }
                    else
                    {
                        DisplayAlert("Erro", "O endereço não foi encontrado para o CEP informado - " + cep, "OK");
                    }
                    
                }
                catch(Exception e)
                {
                    DisplayAlert("Erro Crítico", e.Message, "OK");
                }
                
            }
        }

        private bool isValidCEP(string cep)
        {
            Boolean valido = true;

            if(cep.Length != 8)
            {
                DisplayAlert("Erro", "CEP inválido! O CEP deve conter 8 dígitos.", "Ok!");

                valido = false;
            }

            int NovoCEP = 0;

            if(!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("Erro", "CEP inválido! O CEP deve ser composto apenas por números!", "Ok!");

                valido = false;
            }

            return valido;
        }
    }
}
