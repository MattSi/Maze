using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.propig.util.Graph
{
    /// <summary>
    /// Squire grid graph means a matrix of vertex
    /// e.g.
    /// p(0,0), p(0,1), p(0,2)
    /// p(1,0), p(1,1), p(1,2)
    /// p(2,0), p(2,1), p(2,2)
    /// </summary>
    public class SquireGridGraph : GraphL
    {
        private readonly int ROW;
        private readonly int COL;
        public SquireGridGraph(int row, int col, bool isDirected = true)
            : base(row * col, isDirected)
        {
            ROW = row;
            COL = col;
        }

        public int GetVertexNumber(int row, int col)
        {
            Debug.Assert(row >= 0 && row < ROW);
            Debug.Assert(col >= 0 && col < COL);

            return row * COL + col;
        }

        public void GetRowAndColFromVertex(int vertex, out int row, out int col)
        {
            Debug.Assert(vertex >= 0 && vertex < VerticesNum());
            row = vertex / COL;
            col = vertex % COL;
        }

        public void SetNeighbor(int row, int col, Direction direction, int weight)
        {
            switch (direction)
            {
                case Direction.East:
                    if (col + 1 >= COL)
                        return;
                    SetEdge(row * COL + col, row * COL + col + 1, weight);
                    break;
                case Direction.North:
                    if (row - 1 < 0)
                        return;
                    SetEdge(row * COL + col, (row - 1) * COL + col, weight);
                    break;
                case Direction.South:
                    if (row + 1 >= ROW)
                        return;
                    SetEdge(row * COL + col, (row + 1) * COL + col, weight);
                    break;
                case Direction.West:
                    if (col - 1 < 0)
                        return;
                    SetEdge(row * COL + col, row * COL + col - 1, weight);
                    break;
                default:
                    throw new InvalidCastException("Direction Error");
            }
        }

        public void RemoveNeighbor(int row, int col, Direction direction)
        {
            switch (direction)
            {
                case Direction.East:
                    if (col + 1 >= COL)
                        return;
                    DelEdge(row * COL + col, row * COL + col + 1);
                    break;
                case Direction.North:
                    if (row - 1 < 0)
                        return;
                    DelEdge(row * COL + col, (row - 1) * COL + col);
                    break;
                case Direction.South:
                    if (row + 1 >= ROW)
                        return;
                    DelEdge(row * COL + col, (row + 1) * COL + col);
                    break;
                case Direction.West:
                    if (col - 1 < 0)
                        return;
                    DelEdge(row * COL + col, row * COL + col - 1);
                    break;
                default:
                    throw new InvalidCastException("Direction Error");
            }
        }

    }

    public enum Direction
    {
        East,
        South,
        West,
        North
    }
}
