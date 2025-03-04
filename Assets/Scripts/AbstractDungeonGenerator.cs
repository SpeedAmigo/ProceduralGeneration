using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected TilemapVisualizer visualizer;
    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;
    [SerializeField] protected string seedString;
    [SerializeField] protected int seed;

    public void GenerateDungeon()
    {
        visualizer.Clear();
        if (seedString != null)
        {
            SeedManager.SetSeed(seedString);
        }
        else
        Debug.LogWarning("No seed specified.");
        seed = SeedManager.Seed;
        Random.InitState(SeedManager.Seed);
        RunProceduralGeneration();
    }

    public void GenerateDungeonNewSeed()
    {
        SeedManager.SetRandomSeed();
        seedString = SeedManager.SeedString;
        seed = SeedManager.Seed;
        Random.InitState(SeedManager.Seed);
        visualizer.Clear();
        RunProceduralGeneration();
    }

    public void ClearMap()
    {
        visualizer.Clear();
    }

    protected abstract void RunProceduralGeneration();
}
