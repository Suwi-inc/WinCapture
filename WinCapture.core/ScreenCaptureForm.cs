namespace WinCapture.core
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="ScreenCaptureForm" />
    /// </summary>
    public partial class ScreenCaptureForm : Form
    {
        /// <summary>
        /// Defines the startPt
        /// </summary>
        private Point startPt;

        /// <summary>
        /// Defines the endPt
        /// </summary>
        private Point endPt;

        /// <summary>
        /// Defines the imageRect
        /// </summary>
        private Rectangle imageRect = Rectangle.Empty;

        /// <summary>
        /// Defines the buff
        /// </summary>
        private Bitmap? buff = null;

        /// <summary>
        /// Defines the flag, pressedFlag
        /// </summary>
        private bool flag, pressedFlag = false;

        /// <summary>
        /// Defines the X, Y, WIDTH, HEIGHT
        /// </summary>
        private int X = 0, Y = 0, WIDTH = 0, HEIGHT = 0;

        /// <summary>
        /// Defines the selectionColor
        /// </summary>
        private Color selectionColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenCaptureForm"/> class.
        /// </summary>
        /// <param name="selectionColor">The selectionColor<see cref="Color"/></param>
        internal ScreenCaptureForm(Color selectionColor = default)
        {
            if (selectionColor == default)
            {
                selectionColor = Color.Gray;
            }
            InitializeComponent();
            setForm();
        }

        /// <summary>
        /// The setSelectionColor
        /// </summary>
        /// <param name="selectionColor">The selectionColor<see cref="Color"/></param>
        internal void setSelectionColor(Color selectionColor)
        {
            this.selectionColor = selectionColor;
        }

        /// <summary>
        /// The setForm
        /// </summary>
        private void setForm()
        {
            startPt = new Point();
            endPt = new Point();

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0.2;

            this.MouseClick += new MouseEventHandler(MouseClicked);
            this.MouseDown += new MouseEventHandler(MousePressed);
            this.MouseMove += new MouseEventHandler(MouseMoved);
            this.MouseUp += new MouseEventHandler(MouseReleased);
        }

        /// <summary>
        /// Sets the SetSelectionColor
        /// </summary>
        /// <param name="selectionColor">The selectionColor<see cref="Color"/></param>
        internal void SetSelectionColor(Color selectionColor)
        {
            this.selectionColor = selectionColor;
        }

        /// <summary>
        /// The CaptureImage
        /// </summary>
        /// <returns>The <see cref="Bitmap?"/></returns>
        internal Bitmap? CaptureImage()
        {
            flag = false;
            this.Visible = true;

            while (!flag)
            {
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();
            }

            return buff;
        }

        /// <summary>
        /// The OnPaint
        /// </summary>
        /// <param name="e">The e<see cref="PaintEventArgs"/></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (pressedFlag)
            {
                SetValues();
                using (SolidBrush brush = new SolidBrush(selectionColor))
                {
                    e.Graphics.FillRectangle(brush, X, Y, WIDTH, HEIGHT);
                }
            }
        }

        /// <summary>
        /// The SetValues
        /// </summary>
        private void SetValues()
        {
            X = Math.Min(startPt.X, endPt.X);
            Y = Math.Min(startPt.Y, endPt.Y);
            WIDTH = Math.Abs(startPt.X - endPt.X);
            HEIGHT = Math.Abs(startPt.Y - endPt.Y);
        }

        /// <summary>
        /// The MousePressed
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="MouseEventArgs"/></param>
        private void MousePressed(object sender, MouseEventArgs e)
        {
            pressedFlag = true;
            startPt = e.Location;
        }

        /// <summary>
        /// The MouseReleased
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="MouseEventArgs"/></param>
        private void MouseReleased(object sender, MouseEventArgs e)
        {
            if (WIDTH == 0 || HEIGHT == 0)
            {
                flag = true;
                // this.Dispose();
                return;
            }

            this.Visible = false;
            imageRect = new Rectangle(X, Y, WIDTH, HEIGHT);
            try
            {
                buff = new Bitmap(imageRect.Width, imageRect.Height);
                using (Graphics g = Graphics.FromImage(buff))
                {
                    g.CopyFromScreen(imageRect.Location, Point.Empty, imageRect.Size);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            flag = true;
            pressedFlag = false;
        }

        /// <summary>
        /// The MouseMoved
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="MouseEventArgs"/></param>
        private void MouseMoved(object sender, MouseEventArgs e)
        {
            endPt = e.Location;
            this.Invalidate();
        }

        /// <summary>
        /// The MouseClicked
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="MouseEventArgs"/></param>
        private void MouseClicked(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// The IsImageCaptured
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsImageCaptured()
        {
            return buff != null;
        }

        /// <summary>
        /// The GetImage
        /// </summary>
        /// <returns>The <see cref="Bitmap"/></returns>
        public Bitmap GetImage()
        {
            return buff;
        }
    }
}
