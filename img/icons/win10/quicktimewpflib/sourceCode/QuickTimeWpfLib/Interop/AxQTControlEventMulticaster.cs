namespace AxQTOControlLib
{
    using QTOControlLib;
    using QTOLibrary;
    using System;
    using System.Runtime.InteropServices;

    [ClassInterface(ClassInterfaceType.None)]
    public class AxQTControlEventMulticaster : _IQTControlEvents
    {
        private AxQTControl parent;

        public AxQTControlEventMulticaster(AxQTControl parent)
        {
            this.parent = parent;
        }

        public virtual void Error(int errorCode, int origin)
        {
            _IQTControlEvents_ErrorEvent e = new _IQTControlEvents_ErrorEvent(errorCode, origin);
            this.parent.RaiseOnError(this.parent, e);
        }

        public virtual void MouseDown(short button, short shift, short x, short y)
        {
            _IQTControlEvents_MouseDownEvent e = new _IQTControlEvents_MouseDownEvent(button, shift, x, y);
            this.parent.RaiseOnMouseDownEvent(this.parent, e);
        }

        public virtual void MouseMove(short button, short shift, short x, short y)
        {
            _IQTControlEvents_MouseMoveEvent e = new _IQTControlEvents_MouseMoveEvent(button, shift, x, y);
            this.parent.RaiseOnMouseMoveEvent(this.parent, e);
        }

        public virtual void MouseUp(short button, short shift, short x, short y)
        {
            _IQTControlEvents_MouseUpEvent e = new _IQTControlEvents_MouseUpEvent(button, shift, x, y);
            this.parent.RaiseOnMouseUpEvent(this.parent, e);
        }

        public virtual void QTEvent(int eventClass, int eventID, int phase, QTEventObject eventObject, ref bool cancel)
        {
            _IQTControlEvents_QTEventEvent e = new _IQTControlEvents_QTEventEvent(eventClass, eventID, phase, eventObject, cancel);
            this.parent.RaiseOnQTEvent(this.parent, e);
            cancel = e.cancel;
        }

        public virtual void SizeChanged(int width, int height)
        {
            _IQTControlEvents_SizeChangedEvent e = new _IQTControlEvents_SizeChangedEvent(width, height);
            this.parent.RaiseOnSizeChangedEvent(this.parent, e);
        }

        public virtual void StatusUpdate(int statusCodeType, int statusCode, string statusMessage)
        {
            _IQTControlEvents_StatusUpdateEvent e = new _IQTControlEvents_StatusUpdateEvent(statusCodeType, statusCode, statusMessage);
            this.parent.RaiseOnStatusUpdate(this.parent, e);
        }
    }
}

