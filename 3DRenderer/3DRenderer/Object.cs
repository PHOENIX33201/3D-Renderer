using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace _3DRenderer
{
    internal struct ObjectCreator
    {
        public readonly Color color;
        public readonly Quaternion[] vertices;
        public readonly (int, int)[] connections;
        public Quaternion pivot;
        public readonly Quaternion rotation;

        internal ObjectCreator(Color color, Quaternion[] vertices, (int, int)[] connections, Quaternion pivot, Vector3 axisOfRotation, float angleInRadians)
        {
            this.color = color;
            this.vertices = vertices;
            this.connections = connections;
            this.pivot = pivot;
            this.rotation = Quaternion.CreateFromAxisAngle(axisOfRotation, angleInRadians / 2);
        }
    }

    internal sealed class Object
    {
        public static float speedInR;
        private readonly Color _color;
        private readonly Quaternion[] _vertices;
        private readonly (int, int)[] _connections;
        public Quaternion pivot;
        public Quaternion rotation;
        public readonly List<(Object, bool)> child_List;
        public bool rotatePropendicurally = false;

        internal Object(Color color, Quaternion[] vertices, (int, int)[] connections, Quaternion pivot, Vector3 axisOfRotation, float angleInRadians, bool render = false)
        {
            _color = color;
            _vertices = vertices.ToArray();
            _connections = connections;
            this.pivot = pivot;

            rotation = Quaternion.CreateFromAxisAngle(axisOfRotation, angleInRadians / 2);
            child_List = new List<(Object, bool)>();

            Form1.OnRotate += OnRotate;
            if (render)
                Form1.OnDraw += OnDraw;
        }

        internal Object(ObjectCreator obj, bool render = false)
        {
            _color = obj.color;
            _vertices = obj.vertices.ToArray();
            _connections = obj.connections;
            pivot = obj.pivot;

            rotation = obj.rotation;
            child_List = new List<(Object, bool)>();

            Form1.OnRotate += OnRotate;
            if (render)
                Form1.OnDraw += OnDraw;
        }

        public void Add(ObjectCreator obj, bool render = true)
        {
            child_List.Add((new Object(obj), render));
        }

        public void OnDraw(Graphics g, Rectangle rectangle, double focalLength, Quaternion offset)
        {
            int[][] projectedVertices = new int[_vertices.Length][];

            for (int i = 0; i < _vertices.Length; i++)
                projectedVertices[i] = GetVertexProjection(_vertices[i]);

            using (Pen pen = new Pen(_color, 1))
            {
                for (int i = 0; i < _connections.Length; i++)
                {
                    int[] startVertex = projectedVertices[_connections[i].Item1];
                    int[] endVertex = projectedVertices[_connections[i].Item2];
                    Point startPoint = new Point(startVertex[0], startVertex[1]);
                    Point endPoint = new Point(endVertex[0], endVertex[1]);
                    g.DrawLine(pen, startPoint, endPoint);
                }
                    
            }

            int[] GetVertexProjection(Quaternion vertex)
            {
                int[] projectedVertex = new int[2];

                double dividor = (focalLength + vertex.Z + pivot.Z + offset.Z);
                double x = (focalLength * (vertex.X + pivot.X + offset.X)) / dividor;
                double y = (focalLength * (vertex.Y + pivot.Y + offset.Y)) / dividor;

                projectedVertex[0] = (int)x + rectangle.Width / 2;
                projectedVertex[1] = (int)y + rectangle.Height / 2;

                return projectedVertex;
            }

            foreach ((Object, bool) o in child_List)
                if(o.Item2)
                    o.Item1.OnDraw(g, rectangle, focalLength, offset + this.pivot);
        }

        public void OnRotate()
        {


            if (rotatePropendicurally)
            {
                Vector3 axis = new Vector3(pivot.X, pivot.Y, pivot.Z);
                Vector3.Normalize(axis);
                rotation = Quaternion.CreateFromAxisAngle(axis, speedInR);
    
            }
                

            Quaternion conjugate = Quaternion.Conjugate(rotation);
            for (int i = 0; i < _vertices.Length; i++)
                _vertices[i] = rotation * _vertices[i] * conjugate;

            foreach ((Object, bool) o in child_List)
                o.Item1.RotateAsChild(rotation);
        }

        public void RotateAsChild(Quaternion rotation)
        {
            Quaternion conjugate = Quaternion.Conjugate(rotation);
            for (int i = 0; i < _vertices.Length; i++)
                _vertices[i] = rotation * _vertices[i] * conjugate;

            foreach ((Object, bool) o in child_List)
                o.Item1.RotateAsChild(rotation);

            pivot = rotation * pivot * conjugate;
        }
    }
}
