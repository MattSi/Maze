using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.propig.util.Graph;
using System.Threading;

namespace GraphShow
{
    public partial class Main : Form
    {
        SquireGridGraph graph;
        private Queue<int> visit = new Queue<int>();
        private int margin = 20;
        private int radius = 9;
        private int lastRow = -1, lastCol = -1;
        private int ROW = 40;
        private int COL = 80;
        private Graphics g;

        public Main()
        {
            InitializeComponent();
            graph = null;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.panelMain.Size.Height.ToString() + " : " + this.panelMain.Size.Width.ToString());

            int width = (this.panelMain.Size.Width - 2 * margin) / COL;
            int height = (this.panelMain.Size.Height - 2 * margin) / ROW;
            graph = new SquireGridGraph(ROW, COL, false);

            g = this.panelMain.CreateGraphics();
            g.Clear(this.panelMain.BackColor);
            Pen pen = new Pen(Color.Blue, 1);
            Brush brush = new SolidBrush(Color.Blue);

            for (int i = 0; i < graph.VerticesNum(); i++)
            {
                int row, col;
                int row2, col2;
                graph.GetRowAndColFromVertex(i, out row, out col);
               // g.FillRectangle(brush, margin + col * width, margin + row * height, radius, radius);
                g.FillEllipse(brush, margin + col * width, margin + row * height, radius, radius);


                if (graph.SetNeighbor(row, col, Direction.East, 1))
                {
                    graph.GetNeighborVertex(row, col, Direction.East, out row2, out col2);
                    g.DrawLine(pen, margin + col * width + radius / 2, margin + row * height + radius / 2,
                        margin + col2 * width + radius / 2, margin + row2 * height + radius / 2);
                }

                if (graph.SetNeighbor(row, col, Direction.South, 1))
                {
                    graph.GetNeighborVertex(row, col, Direction.South, out row2, out col2);
                    g.DrawLine(pen, margin + col * width + radius / 2, margin + row * height + radius / 2,
                        margin + col2 * width + radius / 2, margin + row2 * height + radius / 2);
                }
                if (graph.SetNeighbor(row, col, Direction.West, 1))
                {
                    graph.GetNeighborVertex(row, col, Direction.West, out row2, out col2);
                    g.DrawLine(pen, margin + col * width + radius / 2, margin + row * height + radius / 2,
                        margin + col2 * width + radius / 2, margin + row2 * height + radius / 2);
                }
                if (graph.SetNeighbor(row, col, Direction.North, 1))
                {
                    graph.GetNeighborVertex(row, col, Direction.North, out row2, out col2);
                    g.DrawLine(pen, margin + col * width + radius / 2, margin + row * height + radius / 2,
                        margin + col2 * width + radius / 2, margin + row2 * height + radius / 2);
                }
            }

            Random r = new Random(Environment.TickCount);
            ITravel dfs = new Bfs(graph, preVisit);
            dfs.Travel(r.Next(graph.VerticesNum()));
            pen.Dispose();
            brush.Dispose();
            g.Dispose();
        }

        private Direction getRandomDirection()
        {
            Random r = new Random(Environment.TickCount);
            int d = r.Next(0, 4);
            var result = Enum.Parse(typeof(Direction), d.ToString());

            return (Direction)result;
        }

        private void btnGen2_Click(object sender, EventArgs e)
        {

            graph = new SquireGridGraph(ROW, COL, false);
            DisjSet ds = new DisjSet(graph.VerticesNum());
            Random r = new Random(Environment.TickCount);
            while (ds.Find(0) != ds.Find(graph.VerticesNum() - 1))
            {
                int v1 = r.Next(0, graph.VerticesNum());
                if (graph.GetDegree(v1) >= 3)
                    continue;
                Direction direction = getRandomDirection();
                int v2 = graph.GetNeighborVertex(v1, direction);
                if (v2 != -1)
                {
                    graph.SetEdge(v1, v2, 1);
                    ds.UnionSets(v1, v2);
                }
            }

            Pen pen = new Pen(Color.Brown, 1);
            Brush brush = new SolidBrush(Color.Blue);

           
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics gg = Graphics.FromImage(bitmap);
            for(int i=0; i < graph.VerticesNum(); i++)
            {
                int row, col;
                graph.GetRowAndColFromVertex(i, out row, out col);
                int v2 = graph.GetNeighborVertex(i, Direction.West);
                if (v2 == -1 || !graph.IsConnected(i, v2))
                {
                    // Draw Line
                    gg.DrawLine(pen, margin + col*radius, margin + row*radius,
                        margin + col*radius, margin + row*radius + radius);
                }

                v2 = graph.GetNeighborVertex(i, Direction.North);
                if (v2 == -1 || !graph.IsConnected(i, v2))
                {                    
                    // Draw Line
                    gg.DrawLine(pen, margin + col*radius, margin + row*radius,
                        margin + col*radius + radius, margin + row*radius);
                }

                v2 = graph.GetNeighborVertex(i, Direction.East);
                if (v2 == -1 || !graph.IsConnected(i, v2))
                {
                    // Draw Line
                    gg.DrawLine(pen, margin + col*radius + radius, margin + row*radius,
                        margin + col*radius + radius, margin + row*radius + radius);
                }

                v2 = graph.GetNeighborVertex(i, Direction.South);
                if (v2 == -1 || !graph.IsConnected(i, v2))
                {
                    // Draw Line
                    gg.DrawLine(pen, margin + col*radius, margin + row*radius + radius,
                        margin + col*radius + radius, margin + row*radius + radius);
                }
                
                //gg.DrawRectangle(pen, margin + col * radius, margin + row * radius, radius, radius);
            }
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
            //Random r = new Random(Environment.TickCount);
            //ITravel dfs = new Dfs(graph, preVisit2);
            //dfs.Travel(r.Next(graph.VerticesNum()));
            pen.Dispose();
            brush.Dispose();
            gg.Dispose();
        }

        private bool preVisit2(Graph graph, int v)
        {

            visit.Enqueue(v);
            //int row, col;
            //int width = (this.panelMain.Size.Width - 2 * margin) / COL;
            //int height = (this.panelMain.Size.Height - 2 * margin) / ROW;
            //((SquireGridGraph)graph).GetRowAndColFromVertex(v, out row, out col);

            //Pen pen = new Pen(Color.Red, 1);
            //Brush brush = new SolidBrush(Color.Red);
            //int v1 = ((SquireGridGraph)graph).GetVertexNumber(row, col);
            //int v2 = 0;
            //g.FillRectangle(brush, margin + col * radius, margin + row * radius, radius, radius);
            ////if (lastRow == -1)
            ////{
            ////    ;
            ////}
            ////else
            ////{
            ////    //v2 = ((SquireGridGraph)graph).GetVertexNumber(lastRow, lastCol);
            ////    //if (graph.IsConnected(v1, v2))
            ////    //{
            ////    //    g.DrawLine(pen, margin + col * width + radius / 2, margin + row * height + radius / 2,
            ////    //                margin + lastCol * width + radius / 2, margin + lastRow * height + radius / 2);
            ////    //}
            ////    //g.DrawLine(pen, margin + col * width + radius / 2, margin + row * height + radius / 2,
            ////    //                margin + lastCol * width + radius / 2, margin + lastRow * height + radius / 2);
            ////}
            //lastRow = row;
            //lastCol = col;
            //pen.Dispose();
            //brush.Dispose();
            //Thread.Sleep(10);
            return true;
        }

        private bool preVisit(Graph graph, int v)
        {
            int row, col;
            int width = (this.panelMain.Size.Width - 2 * margin) / COL;
            int height = (this.panelMain.Size.Height - 2 * margin) / ROW;
            ((SquireGridGraph)graph).GetRowAndColFromVertex(v, out row, out col);

            Pen pen = new Pen(Color.Red, 1);
            Brush brush = new SolidBrush(Color.Red);
            int v1 = ((SquireGridGraph)graph).GetVertexNumber(row, col);
            int v2 = 0;
            g.FillRectangle(brush, margin + col * width, margin + row * height, radius, radius);
            if (lastRow == -1)
            {
                ;
            }
            else
            {
                //v2 = ((SquireGridGraph)graph).GetVertexNumber(lastRow, lastCol);
                //if (graph.IsConnected(v1, v2))
                //{
                //    g.DrawLine(pen, margin + col * width + radius / 2, margin + row * height + radius / 2,
                //                margin + lastCol * width + radius / 2, margin + lastRow * height + radius / 2);
                //}
                g.DrawLine(pen, margin + col * width + radius / 2, margin + row * height + radius / 2,
                                margin + lastCol * width + radius / 2, margin + lastRow * height + radius / 2);
            }
            lastRow = row;
            lastCol = col;
            pen.Dispose();
            brush.Dispose();
            Thread.Sleep(20);
            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (visit.Count == 0)
            {
                timer1.Stop();
                return;
            }
            int row, col;
            Bitmap bm = (Bitmap) pictureBox1.Image;
            Graphics gg = Graphics.FromImage(bm);
            Brush brush = new SolidBrush(Color.Blue);
            int v = visit.Dequeue();
            graph.GetRowAndColFromVertex(v, out row, out col);
            gg.FillRectangle(brush, margin + col * radius, margin + row * radius, radius, radius);
            pictureBox1.Image = bm;
            pictureBox1.Refresh();

            brush.Dispose();
            gg.Dispose();
        }

        private void btnTravel_Click(object sender, EventArgs e)
        {
            visit.Clear();
            
            if (!timer1.Enabled)
            {
                ITravel dfs = new Dfs(graph, preVisit2);
                dfs.Travel(0);
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }
    }
}
