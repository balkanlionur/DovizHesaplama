using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Döviz_Bürosu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        float para1, para2, tutar;
 
        string para,parabirimi1,parabirimi2,arapara;

        public void parabirimi(string veri )
        {
            string sonuc = " ";
            switch (veri)
            {
                case "TÜRK LİRASI": sonuc = "TL"; break;
                case "ABD DOLARI": sonuc = "USD"; break;
                case "EURO": sonuc = "EUR"; break;
                case "AVUSTRALYA DOLARI": sonuc = "AUD"; break;
                case "DANİMARKA KRONU": sonuc = "DKK"; break;
                case "İNGİLİZ STERLİNİ": sonuc = "GBP"; break;
                case "İSVİÇRE FRANGI": sonuc = "CHF"; break;
                case "İSVEÇ KRONU": sonuc = "SEK"; break;
                case "KANADA DOLARI": sonuc = "CAD"; break;
                case "KUVEYT DİNARI": sonuc = "KWD"; break;
                case "NORVEÇ KRONU": sonuc = "NOK"; break;
                case "S.ARABİSTAN RİYALİ": sonuc = "SAR"; break;
                case "BULGAR LEVASI": sonuc = "BGN"; break;
                case "RUMEN LEYİ": sonuc = "RON"; break;
                case "RUS RUBLESİ": sonuc = "RUB"; break;
                case "ÇİN YUANI": sonuc = "CNY"; break;
            }
            
            para = sonuc;
            
        }
      
        
        private void button1_Click(object sender, EventArgs e)
        {
            string bugun = "http://www.tcmb.gov.tr/kurlar/today.xml";
            var xmldoc = new XmlDocument();
            xmldoc.Load(bugun);
            if ((comboBox1.Text == "TÜRK LİRASI") && (comboBox2.Text == "TÜRK LİRASI"))
            {
                tutar = float.Parse(textBox1.Text);
                txtsonuc.Text = tutar.ToString();
            }
            else if (comboBox2.Text == "TÜRK LİRASI")
            {
                parabirimi(comboBox1.Text);
                parabirimi1 = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='" + para + "']/ForexBuying").InnerXml;
                parabirimi1 = parabirimi1.Replace(".", ",");
                parabirimi1 = parabirimi1.Substring(0, parabirimi1.Length - 1);
                tutar = float.Parse(textBox1.Text);
                para1 = float.Parse(parabirimi1) * tutar;
                txtsonuc.Text = para1.ToString();
            }
            else if(comboBox1.Text == "TÜRK LİRASI")
            {
                parabirimi(comboBox2.Text);
                string parabirimi2 = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='" + para + "']/ForexBuying").InnerXml;
                parabirimi2 = parabirimi2.Replace(".", ",");

                parabirimi2 = parabirimi2.Substring(0, parabirimi2.Length - 1);
                tutar = float.Parse(textBox1.Text);
                para2 = tutar / float.Parse(parabirimi2);

                txtsonuc.Text = para2.ToString();
            }
            
            else
            {
                //tl cevirme
                parabirimi(comboBox1.Text);
                parabirimi1 = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='" + para + "']/ForexBuying").InnerXml;
                parabirimi1 = parabirimi1.Replace(".", ",");
                parabirimi1 = parabirimi1.Substring(0, parabirimi1.Length - 1);

                tutar = float.Parse(textBox1.Text);
                para1 = float.Parse(parabirimi1) * tutar;
                
                // Tl den istenilene
                parabirimi(comboBox2.Text);
                string parabirimi2 = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='" + para + "']/ForexBuying").InnerXml;
                parabirimi2 = parabirimi2.Replace(".", ",");

                parabirimi2 = parabirimi2.Substring(0, parabirimi2.Length - 1);
                para2 = para1 / float.Parse(parabirimi2);

                txtsonuc.Text = para2.ToString();

            }            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
        }
        
    }
}
