using MPSC.PlenoSoft.ControlFlux.Messages;
using MPSC.PlenoSoft.ControlFlux.Utils;
using System;
using System.Collections.Generic;

namespace MPSC.PlenoSoft.ControlFlux.Containers
{
	public class FluxArg : FluxContainer
	{
		private readonly List<Message> _messages = new List<Message>();

		public Boolean Status { get; private set; } = true;
		public IEnumerable<Message> Messages { get { return _messages.ToArray(); } }

		public void AddTrack(string description)
		{
			Add(new Message { Description = description, FullDescription = (Status ? "Sim - " : "Não - ") + description, Type = MessageType.Track });
		}

		public void AddValidation(string description, string fullDescription = null)
		{
			Add(new Message { Description = description, FullDescription = fullDescription ?? description, Type = MessageType.Validation });
		}

		public Exception AddException(Exception exception)
		{
			Add(new Message { Description = exception.Message, FullDescription = exception.Message, Type = MessageType.Exception });
			return exception;
		}

		public void Add(Message message)
		{
			_messages.Add(message);
			Status &= !message.Type.In(MessageType.Exception, MessageType.Validation);
		}
	}
}