using Hatodikhet_T5PM9K.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hatodikhet_T5PM9K
{
    public partial class Form1 : Form
    {
        private BindingList<Entities.RateData> Rates = new BindingList<Entities.RateData>();
        public Form1()
        {
            InitializeComponent();
            Webszolgaltatashivas();
            dataGridView1.DataSource = Rates;
        }
        private void Webszolgaltatashivas()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
        }
    }
}
