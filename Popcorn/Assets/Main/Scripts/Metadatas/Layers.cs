namespace Popcorn.Metadatas
{

    public static class Layers
    {

        public enum LayerNames
        {
            Background,
            Default,
            Front
        }

        public enum OrdersInDefaultLayer
        {
            Background = 0,
            Person = 1,
            SceneBack = 2,
            SceneFront = 3,
            Front = 4,
            Max = 5
        }

    }
}
