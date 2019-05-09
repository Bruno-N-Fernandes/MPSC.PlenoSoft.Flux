using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.PlenoSoft.ControlFlux.Containers;
using MPSC.PlenoSoft.ControlFlux.Core;
using System;

namespace MPSC.PlenoSoft.ControlFlux.Tests.Core
{
	[TestClass]
	public class TestingFlux
	{
		[TestMethod]
		public void WhenCenario1_StatusTrue()
		{
			Flux.To("Obter Informacoes da pessoa", out var fluxArg)
				.Do("Obter Pessoa Por CPF", p => { })
				.Do("Obter Vendas da PessoaId", p => { })
				.Do("Verificar se tá tudo ok", p => { })
			;

			Assert.IsTrue(fluxArg.Status);
		}

		[TestMethod]
		public void WhenCenario2_StatusFalse()
		{
			Flux.To("Obter Informacoes da pessoa", out var fluxArg)
				.Do("Obter Pessoa Por CPF", ObterPessoaPorCPF)
				.Do("Obter Vendas da PessoaId", ObterVendasDaPessoaId)
				.Do("With Lambda Expression To Action", fa => { /* Do Any Thing */ })
				.Do("With Lambda Expression To Func<T>", fa => 1 /* Return Value */)
				.Do("Verificar se tá tudo ok", VerificarSeTaTudoOk)
			;
			Assert.IsFalse(fluxArg.Status);
		}

		private void ObterPessoaPorCPF(FluxArg fluxArg)
		{
			Flux.With(fluxArg, "Obter Informacoes da pessoa2")
				.Do("Obter Vendas da PessoaId2", ObterVendasDaPessoaId2)
				.Do("Verificar se tá tudo ok2", VerificarSeTaTudoOk)
			;
		}

		private void ObterVendasDaPessoaId2(FluxArg fluxArg)
		{
			fluxArg.AddValidation("Pessoa não encontrada");
		}

		private Int64 ObterVendasDaPessoaId(FluxArg fluxArg)
		{
			return 4L;
		}

		private void VerificarSeTaTudoOk(FluxArg fluxArg)
		{
		}
	}
}