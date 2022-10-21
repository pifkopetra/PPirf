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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace Hatodikhet_T5PM9K
{
    public partial class Form1 : Form
    {
        private BindingList<Entities.RateData> Rates = new BindingList<Entities.RateData>();
        private BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();
            //GetCurrencies();
            //XMLFeldolgozas2();
            RefreshData();
        }

        private void RefreshData()
        {
            Rates.Clear();
            Webszolgaltatashivas();
            dataGridView1.DataSource = Rates;
            //comboBox1.DataSource = Currencies;
            XMLFeldolgozas();
            Megjelenites();
        }

        private string Webszolgaltatashivas()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = (comboBox1.SelectedItem).ToString(),
                startDate = (dateTimePicker1.Value).ToString(),
                endDate = (dateTimePicker2.Value).ToString()
            };

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

            return result;
        }

        private void XMLFeldolgozas()
        {
            var xml = new XmlDocument();
            xml.LoadXml(Webszolgaltatashivas());
            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new Entities.RateData();
                Rates.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));
                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                {
                    rate.Value = value / unit;
                }
            }
        }

        private void Megjelenites()
        {
            chartRateData.DataSource = Rates;
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        /*private string GetCurrencies()
        {
            var mnbService2 = new MNBArfolyamServiceSoapClient();
            var request2 = new GetCurrenciesRequestBody();

            var response2 = mnbService2.GetCurrencies(request2);
            var result2 = response2.GetCurrenciesResult;

            return result2;
        }

        private void XMLFeldolgozas2()
        {
            var xml2 = new XmlDocument();
            xml2.LoadXml(GetCurrencies());
        }*/
    }
}
