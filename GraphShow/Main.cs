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
        private int margin = 2;
        private int radius = 8;
        private int lastRow = -1, lastCol = -1;
        private int ROW = 8;
        private int COL = 15;
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

        private void panel1_Paint(object sender, PaintEventArgs e)
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
    }
}
