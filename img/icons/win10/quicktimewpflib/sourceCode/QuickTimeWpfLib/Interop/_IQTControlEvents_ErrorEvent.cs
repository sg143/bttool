namespace AxQTOControlLib
{
    using System;

    public class _IQTControlEvents_ErrorEvent
    {
        public int errorCode;
        public int origin;

        public _IQTControlEvents_ErrorEvent(int errorCode, int origin)
        {
            this.errorCode = errorCode;
            this.origin = origin;
        }
    }
}

