using System;
using System.Linq;

namespace MPSC.PlenoSoft.ControlFlux.Utils
{
	public static class CollectionExtension
	{
		public static Boolean In<T>(this T self, params T[] args)
		{
			return args.Contains(self);
		}
	}
}