using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Windows.Forms.VisualStyles;



namespace _3DRendererV3
{
    public partial class Form1 : Form
    {
        private Bitmap buffer;
        private Graphics bufferGraphics;
        private readonly System.Windows.Forms.Timer repaintTimer;
        private const float FOCAL_LENGTH = 10000;
        private const int INTERVAL = 10;
        private const float TIME_SCALE = 0.01f;
        internal static event Action Rotate;
        internal static event Action<Graphics, Rectangle, float> Render;
        private const int DISPLAY = 1;

        public static List<(Point, Point, float, Color)> lines = new List<(Point, Point, float, Color)>();

        #region initialization
        public Form1()
        {
            InitializeBuffer();
            InitializeComponent();

            DoubleBuffered = true;

            this.Paint += new PaintEventHandler(Form1_Paint);

            // Initialize and configure the Timer
            repaintTimer = new System.Windows.Forms.Timer();
            repaintTimer.Interval = INTERVAL; // Set the interval in milliseconds (e.g., 10ms)
            repaintTimer.Tick += RepaintTimer_Tick;

            // Start the Timer
            repaintTimer.Start();
        }

        private void InitializeBuffer()
        {
            buffer = new Bitmap(ClientSize.Width, ClientSize.Height);
            bufferGraphics = Graphics.FromImage(buffer);
            bufferGraphics.Clear(Color.Black); // Clear the buffer to a background color
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (buffer != null)
            {
                buffer.Dispose();
                bufferGraphics.Dispose();
            }

            InitializeBuffer();
            Refresh(); // Redraw the form
        }

        private void RepaintTimer_Tick(object sender, EventArgs e)
        {
            // Trigger a repaint of the form
            this.Invalidate();
        }
        #endregion

        #region Displays
        private void Shapes()
        {
            Vector3 pivot = Vector3.Zero;
            Vector3[] vertices;
            (int, int)[] edges;
            Quaternion rotation;


            vertices = new Vector3[]
            {
                new Vector3( 30,  30,  30),
                new Vector3(-30,  30,  30),
                new Vector3(-30,  30, -30),
                new Vector3( 30,  30, -30),

                new Vector3( 30, -30,  30),
                new Vector3(-30, -30,  30),
                new Vector3(-30, -30, -30),
                new Vector3( 30, -30, -30),
            };

            edges = new (int, int)[]
            {
                (0, 1), (4, 5), (0, 4),
                (1, 2), (5, 6), (1, 5),
                (2, 3), (6, 7), (2, 6),
                (3, 0), (7, 4), (3, 7),
            };

            pivot = new Vector3(-100, 0, -7000);
            rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0.7f), 2f * TIME_SCALE);
            new Object(pivot, vertices, edges, Color.Green, rotation);


            vertices = new Vector3[]
            {
                new Vector3( 30, -30,  30),
                new Vector3(-30, -30,  30),
                new Vector3(-30, -30, -30),
                new Vector3( 30, -30, -30),

                new Vector3(  0,  30,   0),
            };

            edges = new (int, int)[]
            {
                (0, 1), (0, 4),
                (1, 2), (1, 4),
                (2, 3), (2, 4),
                (3, 0), (3, 4),
            };

            pivot = new Vector3(100, 0, -7000);
            rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), 2f * TIME_SCALE);
            new Object(pivot, vertices, edges, Color.Purple, rotation).Rotate(Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), (float)Math.PI / 2));
        }

        private void SolarSystem()
        {
            Vector3 pivot = Vector3.Zero;
            Vector3[] vertices;
            (int, int)[] edges;
            Quaternion rotation;


            vertices = new Vector3[]
            {
                new Vector3( 30,  30,  30),
                new Vector3(-30,  30,  30),
                new Vector3(-30,  30, -30),
                new Vector3( 30,  30, -30),

                new Vector3( 30, -30,  30),
                new Vector3(-30, -30,  30),
                new Vector3(-30, -30, -30),
                new Vector3( 30, -30, -30),
            };

            edges = new (int, int)[]
            {
                (0, 1), (4, 5), (0, 4),
                (1, 2), (5, 6), (1, 5),
                (2, 3), (6, 7), (2, 6),
                (3, 0), (7, 4), (3, 7),
            };

            pivot = new Vector3(0, 0, -7000);
            rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 1f), 1.997f * TIME_SCALE);
            new Object(pivot, vertices, edges, Color.Yellow, rotation);

            rotation = Quaternion.CreateFromAxisAngle(new Vector3(-1, -1, -1), 10f / 365f * 3f * TIME_SCALE);
            Object earthPivot = new Object(pivot, null, null, Color.Black, rotation);

            vertices = new Vector3[]
            {
                new Vector3( 10,  10,  10),
                new Vector3(-10,  10,  10),
                new Vector3(-10,  10, -10),
                new Vector3( 10,  10, -10),

                new Vector3( 10, -10,  10),
                new Vector3(-10, -10,  10),
                new Vector3(-10, -10, -10),
                new Vector3( 10, -10, -10),
            };

            pivot = new Vector3(100, 100, -100);
            rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 1, 1), 2f * TIME_SCALE);
            Object earth = new Object(pivot, vertices, edges, Color.Blue, rotation, earthPivot);

            rotation = Quaternion.CreateFromAxisAngle(new Vector3(-1, -1, -1), 2f / (26f / 27f) * TIME_SCALE);
            Object moonPivot = new Object(null, null, null, Color.Black, rotation, earth);

            vertices = new Vector3[]
            {
                new Vector3( 5,  5,  5),
                new Vector3(-5,  5,  5),
                new Vector3(-5,  5, -5),
                new Vector3( 5,  5, -5),

                new Vector3( 5, -5,  5),
                new Vector3(-5, -5,  5),
                new Vector3(-5, -5, -5),
                new Vector3( 5, -5, -5),
            };

            pivot = new Vector3(20, 20, 20);
            rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 1, 1), 0);
            new Object(pivot, vertices, edges, Color.White, rotation, moonPivot);
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            switch (DISPLAY)
            {
                case 0:
                    Shapes();
                    break;

                case 1:
                    SolarSystem();
                    break;
            }

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Size = new Size(workingArea.Width, workingArea.Height);
            this.Location = new Point(workingArea.Left, workingArea.Top);
        }

        private void RenderOnScreen(Graphics g)
        {
            Color BACKGROUND_COLOR = Color.Black;
            using (Brush brush = new SolidBrush(BACKGROUND_COLOR))
                bufferGraphics.FillRectangle(brush, this.ClientRectangle);

            lines.Clear();
            Render?.Invoke(bufferGraphics, this.ClientRectangle, FOCAL_LENGTH);
            lines = lines.OrderByDescending(l => l.Item3).ToList();

            using (Pen pen = new Pen(Color.Black, 3))
            {
                foreach ((Point, Point, float, Color) l in lines)
                {
                    pen.Color = l.Item4;
                    bufferGraphics.DrawLine(pen, l.Item1, l.Item2);
                }
            }

            g.DrawImage(buffer, 0, 0);
        }

        private void Movement()
        {
            Rotate?.Invoke();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; // Take a Graphics object from the PaintEventArgs

            Movement();

            RenderOnScreen(g);
        }
    }
}
