using System.Collections.Generic;

namespace Chess
{
    public class TPlayer
    {
        public List<TPiece> Pieces = new List<TPiece>();
        public TBoard Board;
        public bool IsComputer;
        public TPlayer Enemy
        {
            get
            {
                if (this == Board.WhitePlayer)
                    return Board.BlackPlayer;
                else
                    return Board.WhitePlayer;
            }
        }
        public TPiece PieceAtCell(TCell cell)
        {
            foreach (var piece in Pieces)
            {
                if (piece.Cell == cell)
                    return piece;
            }
            return null;
        }
        public List<TMove> GetAllMoves()
        {
            var moves = new List<TMove>();
            foreach (var piece in Pieces)
            {
                moves.AddRange(piece.GetMoves());
            }
            return moves;
        }

        public static int SearchDepth = 4;
        public TMove BestMove;
        public int MaxiMin(int depth)
        {
            if (depth == 0)
                return Board.Evaluate();

            var allMoves = GetAllMoves();
            var max = -int.MaxValue;

            foreach (var move in allMoves)
            {
                move.Make();
                var score = Enemy.MiniMax(depth - 1);
                if (score > max)
                {
                    if (depth == SearchDepth)
                    {
                        BestMove = move;
                    }
                    max = score;
                }
                move.UnMake();
            }
            return max;
        }
        public int MiniMax(int depth)
        {
            if (depth == 0)
                return Board.Evaluate();

            var allMoves = GetAllMoves();
            var min = int.MaxValue;

            foreach (var move in allMoves)
            {
                move.Make();
                var score = Enemy.MiniMax(depth - 1);
                if (score < min)
                {
                    if (depth == SearchDepth)
                    {
                        BestMove = move;
                    }
                    min = score;
                }
                move.UnMake();
            }
            return min;
        }
    }
}