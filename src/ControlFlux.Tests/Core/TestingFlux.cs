using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.PlenoSoft.ControlFlux.Core;
using MPSC.PlenoSoft.ControlFlux.Parameters;
using System;
using System.Collections.Generic;

namespace MPSC.PlenoSoft.ControlFlux.Tests.Core
{
	[TestClass]
	public class TestingFlux
	{
		[TestMethod]
		public void WhenCenario1_StatusTrue()
		{
			Flux.To(out var fluxArg, "Make Any Thing")
				.Do("With Lambda Expression To Action", fa => { /* Do Any Thing */ })
				.Do("And Add  Integer Number Parameter", fa => fa.AddParam("IntegerNumber", 1))
				.If("And Test Integer Number Parameter", fa => fa.Params.IntegerNumber == 1)
				.Do("And Add  DateTime Value Parameter", fa => fa.AddParam("DateTimeValue", DateTime.Today))
				.If("And Test DateTime Value Parameter", fa => fa.Params.DateTimeValue == DateTime.Today)
				.Do("And Add  a String Value Parameter", fa => fa.AddParam("StringValue", "AEIOU"))
				.If("And Test a String Value Parameter", fa => fa.Params.StringValue == "AEIOU")
				.Do("Return Success Message To Client", fa => fa.AddInformation("Ok"))
			;

			Assert.IsTrue(fluxArg.Status);
			Assert.AreEqual(1, fluxArg.Params.IntegerNumber);
			Assert.AreEqual(DateTime.Today, fluxArg.Params.DateTimeValue);
			Assert.AreEqual("AEIOU", fluxArg.Params.StringValue);
		}

		[TestMethod]
		public void WhenCenario2_StatusFalse()
		{
			Flux.To(out var fluxArg, "Obter Informacoes da pessoa")
				.Do("Informe o CPF", fa => fa.Params.CPF = 123456)
				.Do("Obter Pessoa Por CPF", ObterPessoaPorCPF)
				.Do("Obter Dependente Da Pessoa", ObterDependente)
				.Do("Obter Vendas da PessoaId", ObterVendasDaPessoaId)
				.Do("Aborta Missão", fa => fa.AddValidation("Aborta Missão"))
				.Do("Verificar se tá tudo ok", VerificarSeTaTudoOk)
			;

			Flux.With(fluxArg, "continue")
				.Do("Nothing", fa => fa.AddParam("IntValue", 11))
			;

			Assert.IsFalse(fluxArg.Status);
			Console.WriteLine(String.Join("\r\n", fluxArg.Messages));
		}

		private void ObterPessoaPorCPF(FluxArg fluxArg)
		{
			var cpf = (Int64)fluxArg.Params.CPF;
			fluxArg.Params.Titular = new Pessoa(cpf);
		}

		private void ObterDependente(FluxArg fluxArg)
		{
			var titular = fluxArg.Params.Titular as Pessoa;
			fluxArg.Params.Dependente = new Pessoa();
			fluxArg.Params.Dependente2 = new Pessoa();
		}

		private void ObterVendasDaPessoaId(FluxArg fluxArg)
		{
			var titular = fluxArg.Params.Titular as Pessoa;
			fluxArg.Params.Vendas = new[] { new Venda(), new Venda() };
		}

		private void VerificarSeTaTudoOk(FluxArg fluxArg)
		{
			var titular = fluxArg.Params.Titular as Pessoa;
			var dependente = fluxArg.Params.Dependente as Pessoa;
			var vendas = fluxArg.Params.Vendas as IEnumerable<Venda>;
		}
	}

	public class Pessoa
	{
		private long cpf;

		public Pessoa(long cpf = 0)
		{
			this.cpf = cpf;
		}
	}
	public class Venda { }
}