namespace AxQTOControlLib
{
    using QTOLibrary;
    using System;

    public class _IQTControlEvents_QTEventEvent
    {
        public bool cancel;
        public int eventClass;
        public int eventID;
        public QTEventObject eventObject;
        public int phase;

        public _IQTControlEvents_QTEventEvent(int eventClass, int eventID, int phase, QTEventObject eventObject, bool cancel)
        {
            this.eventClass = eventClass;
            this.eventID = eventID;
            this.phase = phase;
            this.eventObject = eventObject;
            this.cancel = cancel;
        }
    }
}

