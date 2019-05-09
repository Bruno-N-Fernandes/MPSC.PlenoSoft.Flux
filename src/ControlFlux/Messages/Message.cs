using System;
using System.Runtime.Serialization;

namespace MPSC.PlenoSoft.ControlFlux.Messages
{
	[Serializable, DataContract]
	public class Message
	{
		public Guid Id { get; } = Guid.NewGuid();
		public DateTime Created { get; } = DateTime.UtcNow;
		public MessageType Type { get; set; }
		public String Description { get; set; }
		public String FullDescription { get; set; }

		public override String ToString()
		{
			return
				"Id = " + Id
				+ ", Created = " + Created
				+ ", Type = " + Type
				+ ", Description = " + Description 
				+ ", FullDescription = " + FullDescription
			;
		}
	}
}