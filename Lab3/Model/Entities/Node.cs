namespace Lab3.Model;

using System; 
using System.Collections.Generic;

partial class DecisionTree
{
    public class Node
    {
        public readonly Position CurrentState;
        public readonly short I, J;
        public readonly int DepthLevel;
        public readonly bool IsTerminal;
        public int Value;
        public List<Node>? PossibleNextMoves;
        private readonly Game _game;

        public Node(Position previousState, short i, short j, int depthLvl, Game gm)
        {
            DepthLevel = depthLvl;
            bool playerMove = DepthLevel % 2 == 0;
            _game = gm;
            CurrentState = _game.GetNewPosition(i, j, previousState, playerMove);
            I = i;
            J = j;
            
            Value = playerMove ? int.MinValue/2 : int.MaxValue/2;
            if (CurrentState.Value() is Int32.MaxValue/2 or Int32.MinValue/2)
            {
                IsTerminal = true;
                Value = CurrentState.Value();
            }

            InitializeNewNodes();
        }

        public void InitializeNewNodes()
        {
            if (IsTerminal) return;
            if (DepthLevel <= _depthLvlCtr)
            {
                if (PossibleNextMoves != null)
                    foreach (Node n in PossibleNextMoves)
                        n.InitializeNewNodes();
                else PossibleNextMoves = GetPossibleMoves();
            }
            else Value = (int)CurrentState.Value();
        }

        private List<Node> GetPossibleMoves()
        {
            (short i, short j) = DepthLevel % 2 == 0 ? (CurrentState.PlayerCoordinates) : CurrentState.EnemiesCoordinates[0];
            Cell currentCell = _game.Field[i, j];
            List<Node> moves = new List<Node>();
            Cell[] adjacent = GetAdjacentCells(currentCell);
            foreach (Cell cell in adjacent)
            {
                moves.Add(new Node(CurrentState, cell.Y, cell.X, DepthLevel+1, _game));
            }
            return moves;
        }

        private Cell[] GetAdjacentCells(Cell cell)
        {
            byte counter = 0;
            Cell[] adjacentCells = new Cell[8];
            int[] yIndexDeltas = {-1, 0, 1, 0};
            int[] xIndexDeltas = {0, 1, 0, -1};
            bool[] isNotWall = new bool[4];
            for (byte i = 0; i < 4; i++)
            {
                isNotWall[i] = TryAddCell((short)(cell.Y + yIndexDeltas[i]), (short)(cell.X + xIndexDeltas[i]), ref adjacentCells,
                    ref counter);
            }
        
            if (_game.Adjacency == AdjacencyType.EdgesAndCorners)
            {
                yIndexDeltas = new[] { -1, 1, 1, -1 };
                xIndexDeltas = new[] { 1, 1, -1, -1 };
                for (byte i = 0; i < 4; i++)
                {
                    if (isNotWall[i] && isNotWall[(i+1)%4])
                        TryAddCell((short)(cell.Y + yIndexDeltas[i]), (short)(cell.X + xIndexDeltas[i]), ref adjacentCells, ref counter);
                }
            }

            return adjacentCells[..counter];
        }

        private bool TryAddCell(short y, short x, ref Cell[] adjacent, ref byte counter)
        {
            if (y > -1 && y < _game.Field.Height && x > -1 && x < _game.Field.Width)
            {
                Cell cell = _game.Field[y, x];
                if (!(cell.IsPassed || cell.IsWall))
                {
                    adjacent[counter] = cell;
                    counter++;
                }

                return !cell.IsWall;
            }
            return false;
        }
    }
}
