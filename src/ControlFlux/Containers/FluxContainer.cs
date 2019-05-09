using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.PlenoSoft.ControlFlux.Containers
{
	public class FluxContainer
	{
		private readonly List<Object> _objects = new List<Object>();

		public void AddObject(Object obj)
		{
			_objects.Add(obj);
		}

		public IEnumerable<Object> Get() { return _objects.ToArray(); }

		public IEnumerable<TObject> Get<TObject>()
		{
			return _objects.OfType<TObject>();
		}
	}
}