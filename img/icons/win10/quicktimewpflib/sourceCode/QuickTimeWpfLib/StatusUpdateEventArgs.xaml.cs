using System;

namespace QuickTimeWpfLib
{
	public class StatusUpdateEventArgs
		: EventArgs
	{
		internal StatusUpdateEventArgs(AxQTOControlLib._IQTControlEvents_StatusUpdateEvent statusUpdateEvent)
		{
			StatusCode =(StatusCode) statusUpdateEvent.statusCode;
			StatusCodeType = (StatusCodeType)statusUpdateEvent.statusCodeType;
			StatusMessage = statusUpdateEvent.statusMessage;
		}

		public StatusCode StatusCode { get; private set; }
		public StatusCodeType StatusCodeType { get; private set; }
		public string StatusMessage { get; private set; }
	}
}