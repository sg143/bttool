namespace AxQTOControlLib
{
    using System;

    public class _IQTControlEvents_SizeChangedEvent
    {
        public int height;
        public int width;

        public _IQTControlEvents_SizeChangedEvent(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}

