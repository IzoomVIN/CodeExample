using System;

namespace Services.Models
{
    public class GameData
    {
        public WorldBoards Boards { get; private set; }
        public GameState State { get; private set; }

        public GameData()
        {
            State = new GameState();
        }

        public void SetBoards(WorldBoards boards)
        {
            Boards = boards;
        }

        [Serializable]
        public readonly struct WorldBoards
        {
            public readonly float XLow, XUp;
            public readonly  float ZLow, ZUp;

            public WorldBoards(float xLow, float xUp, float zLow, float zUp)
            {
                XLow = xLow;
                XUp = xUp;
                ZLow = zLow;
                ZUp = zUp;
            }

        }
    }
}