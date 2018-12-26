using System;

namespace QuickTimeWpfLib
{
	public class ErrorEventArgs
		:EventArgs
	{
		internal ErrorEventArgs( AxQTOControlLib._IQTControlEvents_ErrorEvent e)
		{
			ErrorCode = e.errorCode;
			Origin = (ErrorEventOrigin)e.origin;
		}

		public int ErrorCode { get; private set; }
		public ErrorEventOrigin Origin { get; private set; }
	}
}