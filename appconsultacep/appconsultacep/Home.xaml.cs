using appconsultacep.Services;
using System;
using Xamarin.Forms;

namespace appconsultacep
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
        }

        private async void btnSearch_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(entryCep.Text))
            {
                await DisplayAlert("Ops!", "Preencher o cep sem traços", "Ok");
                return;
            }

            var cepEntry = entryCep.Text.Trim();

            try
            {

                var cepIsvalid = IsValidCep(cepEntry);

                if (cepIsvalid == true)
                {
                    var result = CepServices.SearchCep(cepEntry);
                    lblresultStreet.Text = result.logradouro;
                    lblresultDistrict.Text = result.bairro;
                    lblresultCity.Text = result.localidade;
                    lblresultState.Text = result.uf;
                    lblresultDate.Text = string.Format(DateTime.Now.ToString("dd/MM/yyyy á H:mm"));
                }
                else
                {
                    await DisplayAlert("Ops!", "O endereço não foi encontrado pelo cep consultado: " + cepEntry, "Ok");
                }

            }
            catch (Exception)
            {
                await DisplayAlert("Aviso", "Falha ao procurar o cep :(", "Ok");
            }

        }

        private bool IsValidCep(string cep)
        {
            var isValid = true;

            try
            {
                if (cep.Length < 8)
                {
                    DisplayAlert("Ops!", "o cep deve ter o minimo de 8 caracteres", "Ok");
                    isValid = false;
                }
                return isValid;

            }
            catch (Exception e)
            {

                DisplayAlert("Erro ao procurar o cep", e.Message, "Ok");
                Loading.IsVisible = false;
            }

            return isValid;

        }
    }
}
