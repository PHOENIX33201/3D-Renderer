using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace _3DRenderer
{


    public partial class Form1 : Form
    {
        private Bitmap buffer;
        private Graphics bufferGraphics;
        private readonly System.Windows.Forms.Timer repaintTimer;

        private const double FOCAL_LENGTH = 1000;
        private const int _INTERVAL = 10;
        private const int _DELAY = 1000;
        private int _timeLeft;

        private readonly Object[,,] _pieces = new Object[3, 3, 3];
        private Object _cube;
        private Quaternion[] _sideRotations = new Quaternion[]
        {
            Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0),  (float)(Math.PI / -2 / _DELAY * _INTERVAL)),
            Quaternion.CreateFromAxisAngle(new Vector3(0, -1, 0), (float)(Math.PI / -2 / _DELAY * _INTERVAL)),
            Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0),  (float)(Math.PI / -2 / _DELAY * _INTERVAL)),
            Quaternion.CreateFromAxisAngle(new Vector3(-1, 0, 0), (float)(Math.PI / -2 / _DELAY * _INTERVAL)),
            Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1),  (float)(Math.PI / -2 / _DELAY * _INTERVAL)),
            Quaternion.CreateFromAxisAngle(new Vector3(0, 0, -1), (float)(Math.PI / -2 / _DELAY * _INTERVAL)),
            Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0),  (float)(Math.PI /  2 / _DELAY * _INTERVAL)),
            Quaternion.CreateFromAxisAngle(new Vector3(0, -1, 0), (float)(Math.PI /  2 / _DELAY * _INTERVAL)),
            Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0),  (float)(Math.PI /  2 / _DELAY * _INTERVAL)),
            Quaternion.CreateFromAxisAngle(new Vector3(-1, 0, 0), (float)(Math.PI /  2 / _DELAY * _INTERVAL)),
            Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1),  (float)(Math.PI /  2 / _DELAY * _INTERVAL)),
            Quaternion.CreateFromAxisAngle(new Vector3(0, 0, -1), (float)(Math.PI /  2 / _DELAY * _INTERVAL)),

        };


        
        

        public static event Action OnRotate;
        public static event Action<Graphics, Rectangle, double, Quaternion> OnDraw;
        

        public Form1()
        {
            InitializeBuffer();
            InitializeComponent();

            DoubleBuffered = true;

            this.Paint += new PaintEventHandler(Form1_Paint);

            // Initialize and configure the Timer
            repaintTimer = new System.Windows.Forms.Timer();
            repaintTimer.Interval = _INTERVAL; // Set the interval in milliseconds (e.g., 10ms)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            int R = -30;
            int L = 30;
            int U = -30;
            int D = 30;
            int F = -30;
            int B = 30;
            int N = 0;

            Quaternion[] vertices;
            (int, int)[] edges;
            Quaternion pivot;
            Vector3 axisOfRotation;
            float angleInRadians;

            Quaternion[] V_EMPTY = new Quaternion[]
            {

            };

            (int, int)[] E_EMPTY = new (int, int)[]
            {

            };

            pivot = new Quaternion(0, 0, -500, 0);
            Quaternion P_EMPTY = new Quaternion(0, 0, 0, 0);
            axisOfRotation = Vector3.Normalize(new Vector3(1, 1, 1)); 
            angleInRadians = (float)Math.PI / 50 * 0;

            Object cube = new Object(Color.White, V_EMPTY, E_EMPTY, pivot, axisOfRotation, angleInRadians, true);

            angleInRadians = 0;

            edges = new (int, int)[]
            {
                (0, 1),
                (0, 2),
                (0, 3),
                (1, 2),
                (1, 3),
                (2, 3),
            };

            #region white
            vertices = new Quaternion[]
            {
                new Quaternion(15, -15, 15, 0),
                new Quaternion(15, -15, -15, 0),
                new Quaternion(-15, -15, 15, 0),
                new Quaternion(-15, -15, -15, 0),
            };

            ObjectCreator WHITE = new ObjectCreator(Color.White, vertices, edges, P_EMPTY, axisOfRotation, angleInRadians);
            #endregion

            #region blue
            vertices = new Quaternion[]
            {
                new Quaternion(15, 15, 15, 0),
                new Quaternion(15, -15, 15, 0),
                new Quaternion(-15, 15, 15, 0),
                new Quaternion(-15, -15, 15, 0),
            };

            ObjectCreator BLUE = new ObjectCreator(Color.Blue, vertices, edges, P_EMPTY, axisOfRotation, angleInRadians);
            #endregion

            #region orange
            vertices = new Quaternion[]
            {
                new Quaternion(15, 15, 15, 0),
                new Quaternion(15, -15, 15, 0),
                new Quaternion(15, 15, -15, 0),
                new Quaternion(15, -15, -15, 0),
            };

            ObjectCreator ORANGE = new ObjectCreator(Color.Orange, vertices, edges, P_EMPTY, axisOfRotation, angleInRadians);
            #endregion

            #region red
            vertices = new Quaternion[]
            {
                new Quaternion(-15, 15, 15, 0),
                new Quaternion(-15, -15, 15, 0),
                new Quaternion(-15, 15, -15, 0),
                new Quaternion(-15, -15, -15, 0),
            };

            ObjectCreator RED = new ObjectCreator(Color.Red, vertices, edges, P_EMPTY, axisOfRotation, angleInRadians);
            #endregion

            #region green
            vertices = new Quaternion[]
            {
                new Quaternion(15, 15, -15, 0),
                new Quaternion(15, -15, -15, 0),
                new Quaternion(-15, 15, -15, 0),
                new Quaternion(-15, -15, -15, 0),
            };

            ObjectCreator GREEN = new ObjectCreator(Color.Green, vertices, edges, P_EMPTY, axisOfRotation, angleInRadians);
            #endregion

            #region Yellow
            vertices = new Quaternion[]
            {
                new Quaternion(15, 15, 15, 0),
                new Quaternion(15, 15, -15, 0),
                new Quaternion(-15, 15, 15, 0),
                new Quaternion(-15, 15, -15, 0),
            };

            ObjectCreator YELLOW = new ObjectCreator(Color.Yellow, vertices, edges, P_EMPTY, axisOfRotation, angleInRadians);
            #endregion

            
            #region pieces
            ObjectCreator piece = new ObjectCreator(Color.White, V_EMPTY, E_EMPTY, pivot, axisOfRotation, angleInRadians);
            piece.pivot = new Quaternion(L, U, B, 0); Object p1  = new Object(piece); p1 .Add(WHITE ); p1 .Add(BLUE ); p1 .Add(ORANGE); cube.child_List.Add((p1 , true)); _pieces[0, 0, 0] = p1 ;
            piece.pivot = new Quaternion(N, U, B, 0); Object p2  = new Object(piece); p2 .Add(WHITE ); p2 .Add(BLUE );                  cube.child_List.Add((p2 , true)); _pieces[1, 0, 0] = p2 ;
            piece.pivot = new Quaternion(R, U, B, 0); Object p3  = new Object(piece); p3 .Add(WHITE ); p3 .Add(BLUE ); p3 .Add(RED   ); cube.child_List.Add((p3 , true)); _pieces[2, 0, 0] = p3 ;
            piece.pivot = new Quaternion(L, U, N, 0); Object p4  = new Object(piece); p4 .Add(WHITE );                 p4 .Add(ORANGE); cube.child_List.Add((p4 , true)); _pieces[0, 0, 1] = p4 ;
            piece.pivot = new Quaternion(N, U, N, 0); Object p5  = new Object(piece); p5 .Add(WHITE );                                  cube.child_List.Add((p5 , true)); _pieces[1, 0, 1] = p5 ;
            piece.pivot = new Quaternion(R, U, N, 0); Object p6  = new Object(piece); p6 .Add(WHITE );                 p6 .Add(RED   ); cube.child_List.Add((p6 , true)); _pieces[2, 0, 1] = p6 ;
            piece.pivot = new Quaternion(L, U, F, 0); Object p7  = new Object(piece); p7 .Add(WHITE ); p7 .Add(GREEN); p7 .Add(ORANGE); cube.child_List.Add((p7 , true)); _pieces[0, 0, 2] = p7 ;
            piece.pivot = new Quaternion(N, U, F, 0); Object p8  = new Object(piece); p8 .Add(WHITE ); p8 .Add(GREEN);                  cube.child_List.Add((p8 , true)); _pieces[1, 0, 2] = p8 ;
            piece.pivot = new Quaternion(R, U, F, 0); Object p9  = new Object(piece); p9 .Add(WHITE ); p9 .Add(GREEN); p9 .Add(RED   ); cube.child_List.Add((p9 , true)); _pieces[2, 0, 2] = p9 ;
            piece.pivot = new Quaternion(L, N, B, 0); Object p10 = new Object(piece);                  p10.Add(BLUE ); p10.Add(ORANGE); cube.child_List.Add((p10, true)); _pieces[0, 1, 0] = p10;
            piece.pivot = new Quaternion(N, N, B, 0); Object p11 = new Object(piece);                  p11.Add(BLUE );                  cube.child_List.Add((p11, true)); _pieces[1, 1, 0] = p11;
            piece.pivot = new Quaternion(R, N, B, 0); Object p12 = new Object(piece);                  p12.Add(BLUE ); p12.Add(RED   ); cube.child_List.Add((p12, true)); _pieces[2, 1, 0] = p12;
            piece.pivot = new Quaternion(L, N, N, 0); Object p13 = new Object(piece);                                  p13.Add(ORANGE); cube.child_List.Add((p13, true)); _pieces[0, 1, 1] = p13;
            piece.pivot = new Quaternion(N, N, N, 0); Object emO = new Object(piece);                                                                                     _pieces[1, 1, 1] = emO;
            piece.pivot = new Quaternion(R, N, N, 0); Object p14 = new Object(piece);                                  p14.Add(RED   ); cube.child_List.Add((p14, true)); _pieces[2, 1, 1] = p14;
            piece.pivot = new Quaternion(L, N, F, 0); Object p15 = new Object(piece);                  p15.Add(GREEN); p15.Add(ORANGE); cube.child_List.Add((p15, true)); _pieces[0, 1, 2] = p15;
            piece.pivot = new Quaternion(N, N, F, 0); Object p16 = new Object(piece);                  p16.Add(GREEN);                  cube.child_List.Add((p16, true)); _pieces[1, 1, 2] = p16;
            piece.pivot = new Quaternion(R, N, F, 0); Object p17 = new Object(piece);                  p17.Add(GREEN); p17.Add(RED   ); cube.child_List.Add((p17, true)); _pieces[2, 1, 2] = p17;
            piece.pivot = new Quaternion(L, D, B, 0); Object p18 = new Object(piece); p18.Add(YELLOW); p18.Add(BLUE ); p18.Add(ORANGE); cube.child_List.Add((p18, true)); _pieces[0, 2, 0] = p18;
            piece.pivot = new Quaternion(N, D, B, 0); Object p19 = new Object(piece); p19.Add(YELLOW); p19.Add(BLUE );                  cube.child_List.Add((p19, true)); _pieces[1, 2, 0] = p19;
            piece.pivot = new Quaternion(R, D, B, 0); Object p20 = new Object(piece); p20.Add(YELLOW); p20.Add(BLUE ); p20.Add(RED   ); cube.child_List.Add((p20, true)); _pieces[2, 2, 0] = p20;
            piece.pivot = new Quaternion(L, D, N, 0); Object p21 = new Object(piece); p21.Add(YELLOW);                 p21.Add(ORANGE); cube.child_List.Add((p21, true)); _pieces[0, 2, 1] = p21;
            piece.pivot = new Quaternion(N, D, N, 0); Object p22 = new Object(piece); p22.Add(YELLOW);                                  cube.child_List.Add((p22, true)); _pieces[1, 2, 1] = p22;
            piece.pivot = new Quaternion(R, D, N, 0); Object p23 = new Object(piece); p23.Add(YELLOW);                 p23.Add(RED   ); cube.child_List.Add((p23, true)); _pieces[2, 2, 1] = p23;
            piece.pivot = new Quaternion(L, D, F, 0); Object p24 = new Object(piece); p24.Add(YELLOW); p24.Add(GREEN); p24.Add(ORANGE); cube.child_List.Add((p24, true)); _pieces[0, 2, 2] = p24;
            piece.pivot = new Quaternion(N, D, F, 0); Object p25 = new Object(piece); p25.Add(YELLOW); p25.Add(GREEN);                  cube.child_List.Add((p25, true)); _pieces[1, 2, 2] = p25;
            piece.pivot = new Quaternion(R, D, F, 0); Object p26 = new Object(piece); p26.Add(YELLOW); p26.Add(GREEN); p26.Add(RED   ); cube.child_List.Add((p26, true)); _pieces[2, 2, 2] = p26;

            #endregion
            _cube = cube;
        }

        void RotateSide()
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= _INTERVAL;
                return;
            }

            _timeLeft = _DELAY-_INTERVAL;

            string rotations = "UDRLFBudrlfb";
            Random rg = new Random();
            int r = rg.Next(0, 12);
            char selectedRotation = rotations[r];
            Object tempCorner;
            Object tempEdge;
            Object piece;
            Quaternion Stationary = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), 0);

            if (_pieces[1, 0, 1].child_List.Count == 9) _pieces[1, 0, 1].child_List.RemoveRange(1, 8); _pieces[1, 0, 1].rotation = Stationary;
            if (_pieces[1, 2, 1].child_List.Count == 9) _pieces[1, 2, 1].child_List.RemoveRange(1, 8); _pieces[1, 2, 1].rotation = Stationary;
            if (_pieces[1, 1, 0].child_List.Count == 9) _pieces[1, 1, 0].child_List.RemoveRange(1, 8); _pieces[1, 1, 0].rotation = Stationary;
            if (_pieces[0, 1, 1].child_List.Count == 9) _pieces[0, 1, 1].child_List.RemoveRange(1, 8); _pieces[0, 1, 1].rotation = Stationary;
            if (_pieces[2, 1, 1].child_List.Count == 9) _pieces[2, 1, 1].child_List.RemoveRange(1, 8); _pieces[2, 1, 1].rotation = Stationary;
            if (_pieces[1, 1, 2].child_List.Count == 9) _pieces[1, 1, 2].child_List.RemoveRange(1, 8); _pieces[1, 1, 2].rotation = Stationary;

            switch (selectedRotation)
            {
                case 'U':
                    tempEdge = _pieces[1, 0, 0];
                    _pieces[1, 0, 0] = _pieces[0, 0, 1];
                    _pieces[0, 0, 1] = _pieces[1, 0, 2];
                    _pieces[1, 0, 2] = _pieces[2, 0, 1];
                    _pieces[2, 0, 1] = tempEdge;

                    tempCorner = _pieces[0, 0, 0];
                    _pieces[0, 0, 0] = _pieces[0, 0, 2];
                    _pieces[0, 0, 2] = _pieces[2, 0, 2];
                    _pieces[2, 0, 2] = _pieces[2, 0, 0];
                    _pieces[2, 0, 0] = tempCorner;

                    piece = _pieces[1, 0, 1];
                    piece.rotation = _sideRotations[r];

                    piece.child_List.Add((_pieces[0, 0, 0], false));
                    piece.child_List.Add((_pieces[1, 0, 0], false));
                    piece.child_List.Add((_pieces[2, 0, 0], false));
                    piece.child_List.Add((_pieces[0, 0, 1], false));
                    piece.child_List.Add((_pieces[2, 0, 1], false));
                    piece.child_List.Add((_pieces[0, 0, 2], false));
                    piece.child_List.Add((_pieces[1, 0, 2], false));
                    piece.child_List.Add((_pieces[2, 0, 2], false));

                    break;

                case 'u':
                    tempEdge = _pieces[1, 0, 0];
                    _pieces[1, 0, 0] = _pieces[2, 0, 1];
                    _pieces[2, 0, 1] = _pieces[1, 0, 2];
                    _pieces[1, 0, 2] = _pieces[0, 0, 1];
                    _pieces[0, 0, 1] = tempEdge;

                    tempCorner = _pieces[0, 0, 0];
                    _pieces[0, 0, 0] = _pieces[2, 0, 0];
                    _pieces[2, 0, 0] = _pieces[2, 0, 2];
                    _pieces[2, 0, 2] = _pieces[0, 0, 2];
                    _pieces[0, 0, 2] = tempCorner;

                    piece = _pieces[1, 0, 1];
                    piece.rotation = _sideRotations[r];

                    piece.child_List.Add((_pieces[0, 0, 0], false));
                    piece.child_List.Add((_pieces[1, 0, 0], false));
                    piece.child_List.Add((_pieces[2, 0, 0], false));
                    piece.child_List.Add((_pieces[0, 0, 1], false));
                    piece.child_List.Add((_pieces[2, 0, 1], false));
                    piece.child_List.Add((_pieces[0, 0, 2], false));
                    piece.child_List.Add((_pieces[1, 0, 2], false));
                    piece.child_List.Add((_pieces[2, 0, 2], false));

                    break;

                case 'D':
                    tempEdge = _pieces[1, 2, 0];
                    _pieces[1, 2, 0] = _pieces[2, 2, 1];
                    _pieces[2, 2, 1] = _pieces[1, 2, 2];
                    _pieces[1, 2, 2] = _pieces[0, 2, 1];
                    _pieces[0, 2, 1] = tempEdge;

                    tempCorner = _pieces[0, 2, 0];
                    _pieces[0, 2, 0] = _pieces[2, 2, 0];
                    _pieces[2, 2, 0] = _pieces[2, 2, 2];
                    _pieces[2, 2, 2] = _pieces[0, 2, 2];
                    _pieces[0, 2, 2] = tempCorner;

                    piece = _pieces[1, 2, 1];
                    piece.rotation = _sideRotations[r];

                    piece.child_List.Add((_pieces[0, 2, 0], false));
                    piece.child_List.Add((_pieces[1, 2, 0], false));
                    piece.child_List.Add((_pieces[2, 2, 0], false));
                    piece.child_List.Add((_pieces[0, 2, 1], false));
                    piece.child_List.Add((_pieces[2, 2, 1], false));
                    piece.child_List.Add((_pieces[0, 2, 2], false));
                    piece.child_List.Add((_pieces[1, 2, 2], false));
                    piece.child_List.Add((_pieces[2, 2, 2], false));

                    break;

                case 'd':
                    tempEdge = _pieces[1, 2, 0];
                    _pieces[1, 2, 0] = _pieces[0, 2, 1];
                    _pieces[0, 2, 1] = _pieces[1, 2, 2];
                    _pieces[1, 2, 2] = _pieces[2, 2, 1];
                    _pieces[2, 2, 1] = tempEdge;

                    tempCorner = _pieces[0, 2, 0];
                    _pieces[0, 2, 0] = _pieces[0, 2, 2];
                    _pieces[0, 2, 2] = _pieces[2, 2, 2];
                    _pieces[2, 2, 2] = _pieces[2, 2, 0];
                    _pieces[2, 2, 0] = tempCorner;

                    piece = _pieces[1, 2, 1];
                    piece.rotation = _sideRotations[r];

                    piece.child_List.Add((_pieces[0, 2, 0], false));
                    piece.child_List.Add((_pieces[1, 2, 0], false));
                    piece.child_List.Add((_pieces[2, 2, 0], false));
                    piece.child_List.Add((_pieces[0, 2, 1], false));
                    piece.child_List.Add((_pieces[2, 2, 1], false));
                    piece.child_List.Add((_pieces[0, 2, 2], false));
                    piece.child_List.Add((_pieces[1, 2, 2], false));
                    piece.child_List.Add((_pieces[2, 2, 2], false));

                    break;

                case 'B':
                    tempEdge = _pieces[1, 0, 0];
                    _pieces[1, 0, 0] = _pieces[2, 1, 0];
                    _pieces[2, 1, 0] = _pieces[1, 2, 0];
                    _pieces[1, 2, 0] = _pieces[0, 1, 0];
                    _pieces[0, 1, 0] = tempEdge;

                    tempCorner = _pieces[0, 0, 0];
                    _pieces[0, 0, 0] = _pieces[2, 0, 0];
                    _pieces[2, 0, 0] = _pieces[2, 2, 0];
                    _pieces[2, 2, 0] = _pieces[0, 2, 0];
                    _pieces[0, 2, 0] = tempCorner;

                    piece = _pieces[1, 1, 0];
                    piece.rotation = _sideRotations[r];

                    piece.child_List.Add((_pieces[0, 0, 0], false));
                    piece.child_List.Add((_pieces[0, 1, 0], false));
                    piece.child_List.Add((_pieces[0, 2, 0], false));
                    piece.child_List.Add((_pieces[1, 0, 0], false));
                    piece.child_List.Add((_pieces[1, 2, 0], false));
                    piece.child_List.Add((_pieces[2, 0, 0], false));
                    piece.child_List.Add((_pieces[2, 1, 0], false));
                    piece.child_List.Add((_pieces[2, 2, 0], false));

                    break;

                case 'b':
                    tempEdge = _pieces[1, 0, 0];
                    _pieces[1, 0, 0] = _pieces[0, 1, 0];
                    _pieces[0, 1, 0] = _pieces[1, 2, 0];
                    _pieces[1, 2, 0] = _pieces[2, 1, 0];
                    _pieces[2, 1, 0] = tempEdge;

                    tempCorner = _pieces[0, 0, 0];
                    _pieces[0, 0, 0] = _pieces[0, 2, 0];
                    _pieces[0, 2, 0] = _pieces[2, 2, 0];
                    _pieces[2, 2, 0] = _pieces[2, 0, 0];
                    _pieces[2, 0, 0] = tempCorner;

                    piece = _pieces[1, 1, 0];
                    piece.rotation = _sideRotations[r];

                    piece.child_List.Add((_pieces[0, 0, 0], false));
                    piece.child_List.Add((_pieces[0, 1, 0], false));
                    piece.child_List.Add((_pieces[0, 2, 0], false));
                    piece.child_List.Add((_pieces[1, 0, 0], false));
                    piece.child_List.Add((_pieces[1, 2, 0], false));
                    piece.child_List.Add((_pieces[2, 0, 0], false));
                    piece.child_List.Add((_pieces[2, 1, 0], false));
                    piece.child_List.Add((_pieces[2, 2, 0], false));

                    break;

                case 'F':
                    tempEdge = _pieces[1, 0, 2];
                    _pieces[1, 0, 2] = _pieces[0, 1, 2];
                    _pieces[0, 1, 2] = _pieces[1, 2, 2];
                    _pieces[1, 2, 2] = _pieces[2, 1, 2];
                    _pieces[2, 1, 2] = tempEdge;

                    tempCorner = _pieces[0, 0, 2];
                    _pieces[0, 0, 2] = _pieces[0, 2, 2];
                    _pieces[0, 2, 2] = _pieces[2, 2, 2];
                    _pieces[2, 2, 2] = _pieces[2, 0, 2];
                    _pieces[2, 0, 2] = tempCorner;

                    piece = _pieces[1, 1, 2];
                    piece.rotation = _sideRotations[r];

                    piece.child_List.Add((_pieces[0, 0, 2], false));
                    piece.child_List.Add((_pieces[0, 1, 2], false));
                    piece.child_List.Add((_pieces[0, 2, 2], false));
                    piece.child_List.Add((_pieces[1, 0, 2], false));
                    piece.child_List.Add((_pieces[1, 2, 2], false));
                    piece.child_List.Add((_pieces[2, 0, 2], false));
                    piece.child_List.Add((_pieces[2, 1, 2], false));
                    piece.child_List.Add((_pieces[2, 2, 2], false));

                    break;

                case 'f':
                    tempEdge = _pieces[1, 0, 2];
                    _pieces[1, 0, 2] = _pieces[2, 1, 2];
                    _pieces[2, 1, 2] = _pieces[1, 2, 2];
                    _pieces[1, 2, 2] = _pieces[0, 1, 2];
                    _pieces[0, 1, 2] = tempEdge;

                    tempCorner = _pieces[0, 0, 2];
                    _pieces[0, 0, 2] = _pieces[2, 0, 2];
                    _pieces[2, 0, 2] = _pieces[2, 2, 2];
                    _pieces[2, 2, 2] = _pieces[0, 2, 2];
                    _pieces[0, 2, 2] = tempCorner;

                    piece = _pieces[1, 1, 2];
                    piece.rotation = _sideRotations[r];

                    piece.child_List.Add((_pieces[0, 0, 2], false));
                    piece.child_List.Add((_pieces[0, 1, 2], false));
                    piece.child_List.Add((_pieces[0, 2, 2], false));
                    piece.child_List.Add((_pieces[1, 0, 2], false));
                    piece.child_List.Add((_pieces[1, 2, 2], false));
                    piece.child_List.Add((_pieces[2, 0, 2], false));
                    piece.child_List.Add((_pieces[2, 1, 2], false));
                    piece.child_List.Add((_pieces[2, 2, 2], false));

                    break;

                case 'L':
                    tempEdge = _pieces[0, 0, 1];
                    _pieces[0, 0, 1] = _pieces[0, 1, 0];
                    _pieces[0, 1, 0] = _pieces[0, 2, 1];
                    _pieces[0, 2, 1] = _pieces[0, 1, 2];
                    _pieces[0, 1, 2] = tempEdge;

                    tempCorner = _pieces[0, 0, 0];
                    _pieces[0, 0, 0] = _pieces[0, 2, 0];
                    _pieces[0, 2, 0] = _pieces[0, 2, 2];
                    _pieces[0, 2, 2] = _pieces[0, 0, 2];
                    _pieces[0, 0, 2] = tempCorner;

                    piece = _pieces[0, 1, 1];
                    piece.rotation = _sideRotations[r];

                    piece.child_List.Add((_pieces[0, 0, 0], false));
                    piece.child_List.Add((_pieces[0, 1, 0], false));
                    piece.child_List.Add((_pieces[0, 2, 0], false));
                    piece.child_List.Add((_pieces[0, 0, 1], false));
                    piece.child_List.Add((_pieces[0, 2, 1], false));
                    piece.child_List.Add((_pieces[0, 0, 2], false));
                    piece.child_List.Add((_pieces[0, 1, 2], false));
                    piece.child_List.Add((_pieces[0, 2, 2], false));

                    break;

                case 'l':
                    tempEdge = _pieces[0, 0, 1];
                    _pieces[0, 0, 1] = _pieces[0, 1, 2];
                    _pieces[0, 1, 2] = _pieces[0, 2, 1];
                    _pieces[0, 2, 1] = _pieces[0, 1, 0];
                    _pieces[0, 1, 0] = tempEdge;

                    tempCorner = _pieces[0, 0, 0];
                    _pieces[0, 0, 0] = _pieces[0, 0, 2];
                    _pieces[0, 0, 2] = _pieces[0, 2, 2];
                    _pieces[0, 2, 2] = _pieces[0, 2, 0];
                    _pieces[0, 2, 0] = tempCorner;

                    piece = _pieces[0, 1, 1];
                    piece.rotation = _sideRotations[r];

                    piece.child_List.Add((_pieces[0, 0, 0], false));
                    piece.child_List.Add((_pieces[0, 1, 0], false));
                    piece.child_List.Add((_pieces[0, 2, 0], false));
                    piece.child_List.Add((_pieces[0, 0, 1], false));
                    piece.child_List.Add((_pieces[0, 2, 1], false));
                    piece.child_List.Add((_pieces[0, 0, 2], false));
                    piece.child_List.Add((_pieces[0, 1, 2], false));
                    piece.child_List.Add((_pieces[0, 2, 2], false));

                    break;

                case 'r':
                    tempEdge = _pieces[2, 0, 1];
                    _pieces[2, 0, 1] = _pieces[2, 1, 0];
                    _pieces[2, 1, 0] = _pieces[2, 2, 1];
                    _pieces[2, 2, 1] = _pieces[2, 1, 2];
                    _pieces[2, 1, 2] = tempEdge;

                    tempCorner = _pieces[2, 0, 0];
                    _pieces[2, 0, 0] = _pieces[2, 2, 0];
                    _pieces[2, 2, 0] = _pieces[2, 2, 2];
                    _pieces[2, 2, 2] = _pieces[2, 0, 2];
                    _pieces[2, 0, 2] = tempCorner;

                    piece = _pieces[2, 1, 1];
                    piece.rotation = _sideRotations[r];

                    piece.child_List.Add((_pieces[2, 0, 0], false));
                    piece.child_List.Add((_pieces[2, 1, 0], false));
                    piece.child_List.Add((_pieces[2, 2, 0], false));
                    piece.child_List.Add((_pieces[2, 0, 1], false));
                    piece.child_List.Add((_pieces[2, 2, 1], false));
                    piece.child_List.Add((_pieces[2, 0, 2], false));
                    piece.child_List.Add((_pieces[2, 1, 2], false));
                    piece.child_List.Add((_pieces[2, 2, 2], false));

                    break;

                case 'R':
                    tempEdge = _pieces[2, 0, 1];
                    _pieces[2, 0, 1] = _pieces[2, 1, 2];
                    _pieces[2, 1, 2] = _pieces[2, 2, 1];
                    _pieces[2, 2, 1] = _pieces[2, 1, 0];
                    _pieces[2, 1, 0] = tempEdge;

                    tempCorner = _pieces[2, 0, 0];
                    _pieces[2, 0, 0] = _pieces[2, 0, 2];
                    _pieces[2, 0, 2] = _pieces[2, 2, 2];
                    _pieces[2, 2, 2] = _pieces[2, 2, 0];
                    _pieces[2, 2, 0] = tempCorner;

                    piece = _pieces[2, 1, 1];
                    piece.rotation = _sideRotations[r];

                    piece.child_List.Add((_pieces[2, 0, 0], false));
                    piece.child_List.Add((_pieces[2, 1, 0], false));
                    piece.child_List.Add((_pieces[2, 2, 0], false));
                    piece.child_List.Add((_pieces[2, 0, 1], false));
                    piece.child_List.Add((_pieces[2, 2, 1], false));
                    piece.child_List.Add((_pieces[2, 0, 2], false));
                    piece.child_List.Add((_pieces[2, 1, 2], false));
                    piece.child_List.Add((_pieces[2, 2, 2], false));

                    break;
            }
        }



        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; // Take a Graphics object from the PaintEventArgs

            RotateSide();

            OnRotate?.Invoke();
            
            Color BACKGROUND_COLOR = Color.Black; // Fill screen with black color
            using (Brush brush = new SolidBrush(BACKGROUND_COLOR))
            {
                bufferGraphics.FillRectangle(brush, this.ClientRectangle);// Fill the entire form with the specified color
            }

            OnDraw?.Invoke(bufferGraphics, this.ClientRectangle, FOCAL_LENGTH, new Quaternion(0, 0, 0, 0));

            g.DrawImage(buffer, 0, 0);

        }
    }
}