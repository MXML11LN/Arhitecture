namespace CodeBase.Services.Random
{
    class RandomService : IRandomService
    {
        public int Next(int min, int max) => UnityEngine.Random.Range(min, max);
    }
}