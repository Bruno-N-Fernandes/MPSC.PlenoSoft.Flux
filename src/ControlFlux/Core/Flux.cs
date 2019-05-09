using MPSC.PlenoSoft.ControlFlux.Containers;
using System;

namespace MPSC.PlenoSoft.ControlFlux.Core
{
	public class Flux : IFluxDo
	{
		private readonly FluxArg FluxArg;

		public static IFluxDo To(String description, out FluxArg fluxArg) { return new Flux(description, out fluxArg); }
		public static IFluxDo With(FluxArg fluxArg, String description) { return new Flux(description, fluxArg); }

		private Flux(String description, out FluxArg fluxArg)
		{
			FluxArg = fluxArg = new FluxArg();
			FluxArg.AddTrack(description);
		}

		private Flux(String description, FluxArg fluxArg)
		{
			FluxArg = fluxArg ?? new FluxArg();
			FluxArg.AddTrack(description);
		}

		public IFluxDo Do<T>(String description, Func<FluxArg, T> step)
		{
			return Do(description, fluxArg =>
			{
				var result = step.Invoke(FluxArg);
				fluxArg.AddObject(result);
			});
		}

		public IFluxDo Do(String description, Action<FluxArg> step)
		{
			FluxArg.AddTrack(description);

			if (FluxArg.Status)
			{
				try
				{
					step?.Invoke(FluxArg);
				}
				catch (Exception exception)
				{
					FluxArg.AddException(exception);
					throw;
				}
			}

			return this;
		}
	}
}