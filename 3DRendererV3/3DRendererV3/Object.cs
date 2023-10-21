using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace _3DRendererV3
{
    internal sealed class Object
    {
        public Quaternion pivot;
        private readonly Quaternion[] _vertices;
        private readonly (int, int)[] _edges;
        private readonly Color _color;
        private Quaternion _constantRotation;
        private Object _parent;
        private Quaternion _rotation;

        internal Object(Quaternion pivot, Quaternion[] vertices, (int, int)[] edges, Color color, Quaternion rotation, Object parent = null)
        {
            this.pivot = pivot;
            _vertices = vertices;
            _edges = edges;
            _color = color;
            _parent = parent;
            _constantRotation = rotation;
            _rotation = Quaternion.Identity;

            Form1.Rotate += OnRotate;
            Form1.Render += OnRender;
        }

        internal Object(Vector3? pivot, Vector3[] vertices, (int, int)[] edges, Color color, Quaternion rotation, Object parent = null)
        {
            this.pivot = pivot != null ? new Quaternion(pivot.Value.X, -pivot.Value.Y, pivot.Value.Z, 0) : new Quaternion(0, 0, 0, 0);
            _vertices = vertices != null ? new Quaternion[vertices.Length] : new Quaternion[0]; if(vertices != null) for(int i = 0; i < vertices.Length; i++) _vertices[i] = new Quaternion(vertices[i].X, -vertices[i].Y, -vertices[i].Z, 0);
            _edges = edges != null ? edges : new (int, int)[0];
            _color = color;
            _parent = parent;
            _constantRotation = rotation;
            _rotation = Quaternion.Identity;

            Form1.Rotate += OnRotate;
            Form1.Render += OnRender;
        }

        internal Object Copy()
        {
            return new Object(pivot, _vertices, _edges, _color, _constantRotation, _parent);
        }

        internal void RemoveEvents()
        {
            Form1.Rotate -= OnRotate;
            Form1.Render -= OnRender;
        }

        internal Quaternion GetParentedPostition(Quaternion vertex)
        {
            Quaternion q = _rotation * vertex * Quaternion.Conjugate(_rotation);
            q += pivot;
            q = _parent?.GetParentedPostition(q) ?? q;
            return q;
        }

        internal Quaternion GetGlobalRotation()
        {
            Quaternion q = _parent?.GetGlobalRotation() ?? Quaternion.Identity;
            return _rotation * q;
        }

        internal void SetParent(Object parent)
        {
            if (parent == _parent) return;
            if (parent == this) return;

            pivot = _parent?.GetParentedPostition(pivot) ?? pivot;
            _rotation = GetGlobalRotation();

            _parent = parent;
            if (parent == null) return;

            pivot -= _parent?.GetParentedPostition(new Quaternion(0, 0, 0, 0)) ?? new Quaternion(0, 0, 0, 0);
            Quaternion q = _parent?.GetGlobalRotation() ?? Quaternion.Identity;
            pivot = Quaternion.Conjugate(q) * pivot * q;
            _rotation /= q;
        }

        internal void SetRotation(Quaternion rotation)
        {
            _constantRotation = rotation;
        }

        internal void SetParentRelative(Object parent)
        {
            if (parent == null) return;
            if (parent == _parent) return;
            if (parent == this) return;

            _parent = parent;

            Quaternion q = _parent?.GetGlobalRotation() ?? Quaternion.Identity;
            pivot = Quaternion.Conjugate(q) * pivot * q;
            _rotation /= q;
        }

        internal void Rotate(Quaternion rotation)
        {
            _rotation *= rotation;
        }

        private void OnRotate()
        {
            _rotation = Quaternion.Conjugate(_constantRotation) * _rotation / _constantRotation;
        }

        private void OnRender(Graphics bufferGraphics, Rectangle rectangle, float focalLength)
        {
            (int, int, float)[] projectedVertices = new (int, int, float)[_vertices.Length];
            for (int i = 0; i < _vertices.Length; i++)
                projectedVertices[i] = CalculatePosition(_vertices[i]);

            /*(int, int, float) projectedPivot = CalculatePosition(new Quaternion(0, 0, 0, 0));
            int size = 6;
            Brush brush = new SolidBrush(_color);
            bufferGraphics.FillEllipse(brush, projectedPivot.Item1 - size / 2, projectedPivot.Item2 - size / 2, size, size);*/

            using (Pen pen = new Pen(_color, 1))
            {
                foreach ((int, int) e in _edges)
                {
                    (int, int, float) d1 = projectedVertices[e.Item1];
                    (int, int, float) d2 = projectedVertices[e.Item2];
                    Point p1 = new Point(d1.Item1, d1.Item2);
                    Point p2 = new Point(d2.Item1, d2.Item2);
                    float renderOrder = (d1.Item3 + d2.Item3) / 2;
                    Form1.lines.Add((p1, p2, renderOrder, _color));
                }
            }

            (int, int, float) CalculatePosition(Quaternion vertex)
            {
                Quaternion q = GetParentedPostition(vertex);
                float dividor = q.Z + focalLength;
                float x = (focalLength * q.X / dividor) + rectangle.Width / 2;
                float y = (focalLength * q.Y / dividor) + rectangle.Height / 2;

                return ((int)x, (int)y, q.Z);
            }
        }
    }
}
