using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.Utils;

namespace ConversaoDistancias.Especificacoes
{
    public class TelaConversaoDistancias
    {
        private Browser _browser;
        private IWebDriver _driver;

        public TelaConversaoDistancias(Browser browser)
        {
            _browser = browser;

            string caminhoDriver = null;
            if (browser == Browser.Firefox)
            {
                caminhoDriver =
                    ConfigurationManager.AppSettings["CaminhoDriverFirefox"];
            }
            else if (browser == Browser.Chrome)
            {
                caminhoDriver =
                    ConfigurationManager.AppSettings["CaminhoDriverChrome"];
            }
            else if (browser == Browser.Edge)
            {
                caminhoDriver =
                    ConfigurationManager.AppSettings["CaminhoDriverEdge"];
            }

            _driver = WebDriverFactory.CreateWebDriver(
                browser, caminhoDriver);
        }
        public void CarregarPagina()
        {
            _driver.LoadPage(
                TimeSpan.FromSeconds(5),
                ConfigurationManager.AppSettings["UrlTelaConversaoDistancias"]);
        }

        public void PreencherDistanciaMilhas(double valor)
        {
            _driver.SetText(
                By.Name("DistanciaMilhas"),
                valor.ToString());
        }

        public void ProcessarConversao()
        {
            _driver.Submit(By.Id("btnConverter"));

            WebDriverWait wait = new WebDriverWait(
                _driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => d.FindElement(By.Id("DistanciaKm")) != null);
        }

        public double ObterDistanciaKm()
        {
            return Convert.ToDouble(
                _driver.GetText(By.Id("DistanciaKm")));
        }

        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}