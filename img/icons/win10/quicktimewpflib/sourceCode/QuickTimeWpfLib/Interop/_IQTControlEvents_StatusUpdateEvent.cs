namespace AxQTOControlLib
{
    using System;

    public class _IQTControlEvents_StatusUpdateEvent
    {
        public int statusCode;
        public int statusCodeType;
        public string statusMessage;

        public _IQTControlEvents_StatusUpdateEvent(int statusCodeType, int statusCode, string statusMessage)
        {
            this.statusCodeType = statusCodeType;
            this.statusCode = statusCode;
            this.statusMessage = statusMessage;
        }
    }
}

