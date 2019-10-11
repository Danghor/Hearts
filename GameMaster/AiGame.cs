using ArtificialIntelligence;

namespace GameMaster
{
    public static class AiGame
    {
        public static Game NewAiGame(IPlayer humanPlayer)
        {
            return new Game(new AiPlayer(), new AiPlayer(), new AiPlayer(), humanPlayer);
        }
    }
}
