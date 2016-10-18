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
        private readonly int _row;
        private readonly int _col;
        public SquireGridGraph(int row, int col, bool isDirected = true)
            : base(row * col, isDirected)
        {
            _row = row;
            _col = col;
        }

        public int GetVertexNumber(int row, int col)
        {
            Debug.Assert(row >= 0 && row < _row);
            Debug.Assert(col >= 0 && col < _col);

            return row * _col + col;
        }

        public void GetRowAndColFromVertex(int vertex, out int row, out int col)
        {
            Debug.Assert(vertex >= 0 && vertex < VerticesNum());
            row = vertex / _col;
            col = vertex % _col;
        }

        public void SetNeighbor(int row, int col, Direction direction, int weight)
        {
            switch (direction)
            {
                case Direction.East:
                    if (col + 1 >= _col)
                        return;
                    SetEdge(row * _col + col, row * _col + col + 1, weight);
                    break;
                case Direction.North:
                    if (row - 1 < 0)
                        return;
                    SetEdge(row * _col + col, (row - 1) * _col + col, weight);
                    break;
                case Direction.South:
                    if (row + 1 >= _row)
                        return;
                    SetEdge(row * _col + col, (row + 1) * _col + col, weight);
                    break;
                case Direction.West:
                    if (col - 1 < 0)
                        return;
                    SetEdge(row * _col + col, row * _col + col - 1, weight);
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
                    if (col + 1 >= _col)
                        return;
                    DelEdge(row * _col + col, row * _col + col + 1);
                    break;
                case Direction.North:
                    if (row - 1 < 0)
                        return;
                    DelEdge(row * _col + col, (row - 1) * _col + col);
                    break;
                case Direction.South:
                    if (row + 1 >= _row)
                        return;
                    DelEdge(row * _col + col, (row + 1) * _col + col);
                    break;
                case Direction.West:
                    if (col - 1 < 0)
                        return;
                    DelEdge(row * _col + col, row * _col + col - 1);
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
