using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using com.propig.util.Graph;

namespace GraphShow
{
    public partial class Main : Form
    {
        SquireGridGraph _graph;
        private const int Radius = 9;
        private const int Row = 50;
        private const int Col = 80;
        private const int _margin = 30;
        private AStar _aStar;
        private IList<int> _result;

        public Main()
        {
            InitializeComponent();
            _graph = null;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Direction getRandomDirection()
        {
            var r = new Random(Environment.TickCount);
            int d = r.Next(0, 4);
            var result = Enum.Parse(typeof(Direction), d.ToString(CultureInfo.InvariantCulture));
            return (Direction)result;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_result.Count == 0)
            {
                timer1.Stop();
                Toggle();
                return;
            }
            int row, col;
            var bm = (Bitmap) pictureBox1.Image;
            Graphics gg = Graphics.FromImage(bm);
            Brush brush = new SolidBrush(Color.Blue);
            int v = _result[_result.Count - 1];
            _result.RemoveAt(_result.Count-1);
            _graph.GetRowAndColFromVertex(v, out row, out col);
            gg.FillRectangle(brush, _margin + col*Radius + 3, _margin + row*Radius + 3, 3, 3);
            pictureBox1.Image = bm;
            pictureBox1.Refresh();

            brush.Dispose();
            gg.Dispose();
        }


        private void genMazeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _graph = new SquireGridGraph(Row, Col, false);
            var ds = new DisjSet(_graph.VerticesNum());
            var r = new Random(Environment.TickCount);
            while (ds.Find(0) != ds.Find(_graph.VerticesNum() - 1))
            {
                int v1 = r.Next(0, _graph.VerticesNum());
                if (_graph.GetDegree(v1) >= 3)
                    continue;
                Direction direction = getRandomDirection();
                int v2 = _graph.GetNeighborVertex(v1, direction);
                if (v2 != -1)
                {
                    _graph.SetEdge(v1, v2, 1);
                    ds.UnionSets(v1, v2);
                }
            }

            var pen = new Pen(Color.Brown, 1);
            Brush brush = new SolidBrush(Color.Blue);
            var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics gg = Graphics.FromImage(bitmap);
            for (int i = 0; i < _graph.VerticesNum(); i++)
            {
                int row, col;
                _graph.GetRowAndColFromVertex(i, out row, out col);
                int v2 = _graph.GetNeighborVertex(i, Direction.West);
                if (v2 == -1 || !_graph.IsConnected(i, v2))
                {
                    // Draw Line
                    gg.DrawLine(pen, _margin + col * Radius, _margin + row * Radius,
                        _margin + col * Radius, _margin + row * Radius + Radius);
                }

                v2 = _graph.GetNeighborVertex(i, Direction.North);
                if (v2 == -1 || !_graph.IsConnected(i, v2))
                {
                    // Draw Line
                    gg.DrawLine(pen, _margin + col * Radius, _margin + row * Radius,
                        _margin + col * Radius + Radius, _margin + row * Radius);
                }

                v2 = _graph.GetNeighborVertex(i, Direction.East);
                if (v2 == -1 || !_graph.IsConnected(i, v2))
                {
                    // Draw Line
                    gg.DrawLine(pen, _margin + col * Radius + Radius, _margin + row * Radius,
                        _margin + col * Radius + Radius, _margin + row * Radius + Radius);
                }

                v2 = _graph.GetNeighborVertex(i, Direction.South);
                if (v2 == -1 || !_graph.IsConnected(i, v2))
                {
                    // Draw Line
                    gg.DrawLine(pen, _margin + col * Radius, _margin + row * Radius + Radius,
                        _margin + col * Radius + Radius, _margin + row * Radius + Radius);
                }
            }
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
            pen.Dispose();
            brush.Dispose();
            gg.Dispose();
        }

        private void astarTravelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                if (_graph == null)
                    return;
                Toggle();
                _aStar = new AStar(_graph);
                if (_aStar.Travel(0, _graph.VerticesNum() - 1))
                {
                    _result = _aStar.reconstruct_path(_graph.VerticesNum() - 1);
                    timer1.Start();
                }
            }
            else
            {
                timer1.Stop();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }


        private void Toggle()
        {
            genMazeToolStripMenuItem.Enabled = !genMazeToolStripMenuItem.Enabled;
            astarTravelToolStripMenuItem.Enabled = !astarTravelToolStripMenuItem.Enabled;
        }
    }
}
