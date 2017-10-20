using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Selenium.Utils;

namespace ConversaoDistancias.Especificacoes
{
    [Binding]
    public sealed class ConvDistanciasStepDefinition
    {
        private double _valorMilhas;
        private double _resultadoKm;
        private double _metros;
        private string _nomeBrowser;

        [Given("que estou utilizando o (.*)")]
        public void ObterBrowser(string nomeBrowser)
        {
            _nomeBrowser = nomeBrowser;
        }

        [Given("que foi informada uma distância de (.*) milha\\(s\\)")]
        public void ObterDistanciaMilhas(double milhas)
        {
            _valorMilhas = milhas;
        }

        [When("for solicitada a conversão deste valor")]
        public void ProcessarConversaoAltura()
        {
            Browser browser;
            if (_nomeBrowser == "Firefox")
                browser = Browser.Firefox;
            else if (_nomeBrowser == "Chrome")
                browser = Browser.Chrome;
            else if (_nomeBrowser == "Edge")
                browser = Browser.Edge;
            else
                throw new Exception("Browser inválido!");

            TelaConversaoDistancias tela =
                new TelaConversaoDistancias(browser);
            tela.CarregarPagina();
            tela.PreencherDistanciaMilhas(_valorMilhas);
            tela.ProcessarConversao();
            _resultadoKm = tela.ObterDistanciaKm();
            tela.Fechar();
        }

        [Then("o valor equivalente será de (.*) km\\(s\\)")]
        public void VerificarCalculoConversao(double valorKm)
        {
            Assert.AreEqual(valorKm, _resultadoKm);
        }
    }
}