namespace AxQTOControlLib
{
    using System;

    public class _IQTControlEvents_MouseMoveEvent
    {
        public short button;
        public short shift;
        public short x;
        public short y;

        public _IQTControlEvents_MouseMoveEvent(short button, short shift, short x, short y)
        {
            this.button = button;
            this.shift = shift;
            this.x = x;
            this.y = y;
        }
    }
}

