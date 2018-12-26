using System.Windows.Media;

namespace AxQTOControlLib
{
    using QTOControlLib;
    using QTOLibrary;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [DefaultEvent("QTEvent"), DesignTimeVisible(true), Clsid("{24ba3caf-4be8-4aec-a7c8-6f47d5684602}")]
    public class AxQTControl : AxHost
    {
        private AxHost.ConnectionPointCookie cookie;
        private AxQTControlEventMulticaster eventMulticaster;
        private IQTControl ocx;

        public event AxQTOControlLib._IQTControlEvents_ErrorEventHandler Error;

        public event AxQTOControlLib._IQTControlEvents_MouseDownEventHandler MouseDownEvent;

        public event AxQTOControlLib._IQTControlEvents_MouseMoveEventHandler MouseMoveEvent;

        public event AxQTOControlLib._IQTControlEvents_MouseUpEventHandler MouseUpEvent;

        public event AxQTOControlLib._IQTControlEvents_QTEventEventHandler QTEvent;

        public event AxQTOControlLib._IQTControlEvents_SizeChangedEventHandler SizeChangedEvent;

        public event AxQTOControlLib._IQTControlEvents_StatusUpdateEventHandler StatusUpdate;

        public AxQTControl() : base("24ba3caf-4be8-4aec-a7c8-6f47d5684602")
        {
            base.SetAboutBoxDelegate(new AxHost.AboutBoxDelegate(this.ShowAboutBox));
        }

        public virtual void _get_DataRef(out int pDataRef, out int pDataRefType)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("_get_DataRef", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx._get_DataRef(out pDataRef, out pDataRefType);
        }

        public virtual void _put_DataRef(int inDataRef, int inDataRefType)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("_put_DataRef", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object[] parameters = new object[] { inDataRef, inDataRefType, Missing.Value };
            System.Type type = typeof(IQTControl);
            type.GetMethod("_put_DataRef").Invoke(this.ocx, parameters);
        }

        public virtual void _put_DataRef(int inDataRef, int inDataRefType, object inNewMovieFlags)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("_put_DataRef", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx._put_DataRef(inDataRef, inDataRefType, inNewMovieFlags);
        }

        protected override void AttachInterfaces()
        {
            try
            {
                this.ocx = (IQTControl) base.GetOcx();
            }
            catch (Exception)
            {
            }
        }

        public virtual void CreateNewMovie()
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("CreateNewMovie", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object[] parameters = new object[] { Missing.Value };
            System.Type type = typeof(IQTControl);
            type.GetMethod("CreateNewMovie").Invoke(this.ocx, parameters);
        }

        public virtual void CreateNewMovie(object movieIsActive)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("CreateNewMovie", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx.CreateNewMovie(movieIsActive);
        }

        public virtual void CreateNewMovieFromImages(string bstrFirstFilePath, float rate, bool rateIsFramesPerSecond)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("CreateNewMovieFromImages", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx.CreateNewMovieFromImages(bstrFirstFilePath, rate, rateIsFramesPerSecond);
        }

        protected override void CreateSink()
        {
            try
            {
                this.eventMulticaster = new AxQTControlEventMulticaster(this);
                this.cookie = new AxHost.ConnectionPointCookie(this.ocx, this.eventMulticaster, typeof(_IQTControlEvents));
            }
            catch (Exception)
            {
            }
        }

        protected override void DetachSink()
        {
            try
            {
                this.cookie.Disconnect();
            }
            catch (Exception)
            {
            }
        }

        public virtual object get__Property(int propertyClass, int propertyID)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("get__Property", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            return this.ocx.get__Property(propertyClass, propertyID);
        }

        public virtual bool get_IsQuickTimeAvailable(int howToCheck)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("get_IsQuickTimeAvailable", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            return this.ocx.get_IsQuickTimeAvailable(howToCheck);
        }

        public virtual void MovieResizingLock()
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("MovieResizingLock", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx.MovieResizingLock();
        }

        public virtual void MovieResizingUnlock()
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("MovieResizingUnlock", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx.MovieResizingUnlock();
        }

        public virtual int QuickTimeInitialize()
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("QuickTimeInitialize", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object[] parameters = new object[] { Missing.Value, Missing.Value };
            System.Type type = typeof(IQTControl);
            return (int) type.GetMethod("QuickTimeInitialize").Invoke(this.ocx, parameters);
        }

        public virtual int QuickTimeInitialize(object initOptions, object initFlags)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("QuickTimeInitialize", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            return this.ocx.QuickTimeInitialize(initOptions, initFlags);
        }

        public virtual void QuickTimeTerminate()
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("QuickTimeTerminate", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx.QuickTimeTerminate();
        }

        internal void RaiseOnError(object sender, _IQTControlEvents_ErrorEvent e)
        {
            if (this.Error != null)
            {
                this.Error(sender, e);
            }
        }

        internal void RaiseOnMouseDownEvent(object sender, _IQTControlEvents_MouseDownEvent e)
        {
            if (this.MouseDownEvent != null)
            {
                this.MouseDownEvent(sender, e);
            }
        }

        internal void RaiseOnMouseMoveEvent(object sender, _IQTControlEvents_MouseMoveEvent e)
        {
            if (this.MouseMoveEvent != null)
            {
                this.MouseMoveEvent(sender, e);
            }
        }

        internal void RaiseOnMouseUpEvent(object sender, _IQTControlEvents_MouseUpEvent e)
        {
            if (this.MouseUpEvent != null)
            {
                this.MouseUpEvent(sender, e);
            }
        }

        internal void RaiseOnQTEvent(object sender, _IQTControlEvents_QTEventEvent e)
        {
            if (this.QTEvent != null)
            {
                this.QTEvent(sender, e);
            }
        }

        internal void RaiseOnSizeChangedEvent(object sender, _IQTControlEvents_SizeChangedEvent e)
        {
            if (this.SizeChangedEvent != null)
            {
                this.SizeChangedEvent(sender, e);
            }
        }

        internal void RaiseOnStatusUpdate(object sender, _IQTControlEvents_StatusUpdateEvent e)
        {
            if (this.StatusUpdate != null)
            {
                this.StatusUpdate(sender, e);
            }
        }

        public virtual void set__Property(int propertyClass, int propertyID, object pVal)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("set__Property", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx.set__Property(propertyClass, propertyID, pVal);
        }

        public virtual void SetScale(float x)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("SetScale", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object[] parameters = new object[] { x, Missing.Value };
            System.Type type = typeof(IQTControl);
            type.GetMethod("SetScale").Invoke(this.ocx, parameters);
        }

        public virtual void SetScale(float x, object y)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("SetScale", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx.SetScale(x, y);
        }

        public virtual void SetSizing(QTSizingModeEnum sizingOption, bool forceSizeUpdate)
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("SetSizing", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx.SetSizing(sizingOption, forceSizeUpdate);
        }

        public virtual void ShowAboutBox()
        {
            if (this.ocx == null)
            {
                throw new AxHost.InvalidActiveXStateException("ShowAboutBox", AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            this.ocx.ShowAboutBox();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DispId(11)]
        public virtual int _MovieControllerHandle
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("_MovieControllerHandle", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx._MovieControllerHandle;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DispId(10)]
        public virtual int _MovieHandle
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("_MovieHandle", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx._MovieHandle;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("_MovieHandle", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx._MovieHandle = value;
            }
        }

        [DispId(0x1003), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string AutoPlay
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("AutoPlay", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.AutoPlay;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("AutoPlay", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.AutoPlay = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DispId(-501), ComAliasName("System.UInt32"), Browsable(true)]
        public override Color BackColor
        {
            get
            {
                if ((this.ocx != null) && base.PropsValid())
                {
                    return AxHost.GetColorFromOleColor(this.ocx.BackColor);
                }
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                if (this.ocx != null)
                {
                    this.ocx.BackColor = AxHost.GetOleColorFromColor(value);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DispId(0x22)]
        public virtual string BaseURL
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("BaseURL", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.BaseURL;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("BaseURL", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.BaseURL = value;
            }
        }

        [ComAliasName("System.UInt32"), DispId(-503), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Color BorderColor
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("BorderColor", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return AxHost.GetColorFromOleColor(this.ocx.BorderColor);
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("BorderColor", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.BorderColor = AxHost.GetOleColorFromColor(value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DispId(-504)]
        public virtual BorderStylesEnum BorderStyle
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("BorderStyle", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.BorderStyle;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("BorderStyle", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.BorderStyle = value;
            }
        }

        [DispId(6), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int ErrorCode
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("ErrorCode", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.ErrorCode;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DispId(0x13)]
        public virtual int ErrorHandling
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("ErrorHandling", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.ErrorHandling;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("ErrorHandling", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.ErrorHandling = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DispId(0x34)]
        public virtual string FileName
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FileName", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.FileName;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FileName", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.FileName = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DispId(0x12)]
        public virtual bool FullScreen
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FullScreen", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.FullScreen;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FullScreen", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.FullScreen = value;
            }
        }

        [DispId(0x18), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int FullScreenEndKeyCode
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FullScreenEndKeyCode", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.FullScreenEndKeyCode;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FullScreenEndKeyCode", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.FullScreenEndKeyCode = value;
            }
        }

        [DispId(0x17), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int FullScreenFlags
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FullScreenFlags", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.FullScreenFlags;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FullScreenFlags", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.FullScreenFlags = value;
            }
        }

        [DispId(0x19), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int FullScreenHWND
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FullScreenHWND", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.FullScreenHWND;
            }
        }

        [DispId(50), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int FullScreenMonitorNumber
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FullScreenMonitorNumber", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.FullScreenMonitorNumber;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FullScreenMonitorNumber", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.FullScreenMonitorNumber = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DispId(0x33)]
        public virtual QTFullScreenSizingModeEnum FullScreenSizing
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FullScreenSizing", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.FullScreenSizing;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("FullScreenSizing", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.FullScreenSizing = value;
            }
        }

        [ComAliasName("System.Int32"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), DispId(-515)]
        public virtual int hWnd
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("hWnd", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.hWnd;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DispId(4)]
        public virtual QTMovie Movie
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("Movie", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.Movie;
            }
        }

        [DispId(12), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool MovieControllerVisible
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("MovieControllerVisible", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.MovieControllerVisible;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("MovieControllerVisible", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.MovieControllerVisible = value;
            }
        }

        [DispId(0x31), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int NewMovieFlags
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("NewMovieFlags", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.NewMovieFlags;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("NewMovieFlags", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.NewMovieFlags = value;
            }
        }

        [DispId(5), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual QTQuickTime QuickTime
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("QuickTime", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.QuickTime;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DispId(9)]
        public virtual int QuickTimeVersion
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("QuickTimeVersion", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.QuickTimeVersion;
            }
        }

        [DispId(13), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual float ScaleX
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("ScaleX", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.ScaleX;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("ScaleX", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.ScaleX = value;
            }
        }

        [DispId(14), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual float ScaleY
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("ScaleY", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.ScaleY;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("ScaleY", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.ScaleY = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DispId(0x15)]
        public virtual QTSizingModeEnum Sizing
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("Sizing", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.Sizing;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("Sizing", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.Sizing = value;
            }
        }

        [DispId(3), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string URL
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("URL", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.URL;
            }
            set
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("URL", AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.URL = value;
            }
        }

        [DispId(0x16), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string Version
        {
            get
            {
                if (this.ocx == null)
                {
                    throw new AxHost.InvalidActiveXStateException("Version", AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.Version;
            }
        }
    }
}

