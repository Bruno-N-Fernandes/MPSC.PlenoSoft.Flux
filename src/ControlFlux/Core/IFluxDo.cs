using MPSC.PlenoSoft.ControlFlux.Containers;
using System;

namespace MPSC.PlenoSoft.ControlFlux.Core
{
	public interface IFluxDo
	{
		IFluxDo Do<T>(String description, Func<FluxArg, T> step);
		IFluxDo Do(String description, Action<FluxArg> step);
	}
}