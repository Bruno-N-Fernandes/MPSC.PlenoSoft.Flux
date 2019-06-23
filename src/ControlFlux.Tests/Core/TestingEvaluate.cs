using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MPSC.PlenoSoft.ControlFlux.Tests.Core
{
	[TestClass]
	public class TestingEvaluate
	{
		[TestMethod]
		public void Evaluate()
		{
			var result = Resolve("(1 + (2 + 3)) + (4 + (5 + 6))");
			Assert.AreEqual(21, result);
		}

		public Decimal Resolve(String formula)
		{
			formula = $"({formula})";
			var end = formula.IndexOf(')');
			while (end > 0)
			{
				var tmpFormula = formula.Substring(0, end + 1);
				var start = tmpFormula.LastIndexOf('(');
				var exp = tmpFormula.Substring(start);
				var value = Eval(exp);
				formula = formula.Replace(exp, value.ToString("0.00", en_US));
				end = formula.IndexOf(')');
			}

			return Convert.ToDecimal(formula, en_US); ;
		}

		public Decimal Eval(String formula)
		{
			var operadores = "/*-+";
			var operacao = formula.Replace("(", "").Replace(")", "");

			foreach (var operador in operadores)
			{
				if (operacao.Contains(operador))
					return Calcular(operacao, operador);
			}

			return Decimal.Zero;
		}

		static readonly IFormatProvider en_US = new CultureInfo("en-US");

		static readonly Dictionary<Char, Func<Decimal, Decimal, Decimal>> operacao = new Dictionary<char, Func<decimal, decimal, decimal>>
		{
			{'+', (v1, v2) => v1 + v2 },
			{'-', (v1, v2) => v1 - v2 },
			{'*', (v1, v2) => v1 * v2 },
			{'/', (v1, v2) => v1 / v2 },
		};

		private Decimal Calcular(string expressaoMatematica, char operador)
		{
			var elementos = expressaoMatematica.Split(operador);
			var v1 = Convert.ToDecimal(elementos[0], en_US);
			var v2 = Convert.ToDecimal(elementos[1], en_US);
			if (operacao.TryGetValue(operador, out var func))
				return func.Invoke(v1, v2);
			return v1;
		}

		[TestMethod]
		public void Evaluate2()
		{
			//var result1 = FormulaEvaluator.Evaluate("(2*(2*((10.00/1.2)+1)))+(8*(1*((10.00/1.2)+1)))+((2*8)+((2*((10.00/1.2)*2+4))-8)+1)");
			//Assert.AreEqual(162.3333333333330, result1);
		}
	}
}