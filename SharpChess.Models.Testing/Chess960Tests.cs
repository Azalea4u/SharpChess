namespace SharpChess.Models.Testing
{
    using SharpChess;
    using SharpChess.Model;
    using static SharpChess.Model.Piece;

    [TestClass]
    public sealed class Chess960Tests
    {
        [TestMethod]
        public void Test_DefaultSetup()
        {
            Game.IsChess960 = false;
            Game.SetStartingPositions();

            Assert.AreEqual("ssssssss/ssssssss/8/8/8/8/SSSSSSSS/SSSSSSSS", Board.GetFenPosition(), "Classic setup is incorrect");
        }

        [TestMethod]
        public void Test_Chess960Setup()
        {
            Game.IsChess960 = true;
            Game.SetStartingPositions();

            Assert.IsTrue(Game.IsChess960, "Chess960 mode should be enabled");
        }

        [TestMethod]
        public void Test_Chess960BackRank()
        {
            int[] backRank = Game.GenerateChess960BackRank();

            Assert.AreEqual(2, backRank.Count(x => x == (int)Piece.PieceNames.Rook), "Chess960 must have exactly 2 rooks");
            Assert.AreEqual(1, backRank.Count(x => x == (int)Piece.PieceNames.King), "Chess960 must have exactly 1 king");
            Assert.AreEqual(2, backRank.Count(x => x == (int)Piece.PieceNames.Bishop), "Chess960 must have exactly 2 bishops");
        }

        [TestMethod]
        public void Test_CheckPiecesInChess960()
        {
            Game.IsChess960 = true;
            Game.SetStartingPositions();

            // default setup
            string[] defaultBackRank = { "Rook", "Knight", "Bishop", "Queen", "King", "Bishop", "Knight", "Rook" };

            for (int file = 0; file < 8; file++)
            {
                Piece piece = Board.GetPiece(file, 0); // White back rank
                if (piece != null && piece.Name.Equals(defaultBackRank[file]))
                {
                    Assert.Fail($"Chess960 mode: Default {piece.Name} found at file {file}, which is incorrect.");
                }
            }
        }

        [TestMethod]
        public void Test_PieceIdentifierLogic()
        {
            // Test the identifier logic for different pieces
            Piece.PieceIdentifierCodes identifier = Game.PlayerWhite.GetPieceIdentifier(Piece.PieceNames.Rook, 0);
            Assert.AreEqual(Piece.PieceIdentifierCodes.WhiteQueensRook, identifier, "Incorrect identifier for White's Queen's Rook.");
        }




    }
}