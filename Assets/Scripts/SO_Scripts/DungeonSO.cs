using UnityEngine;

[CreateAssetMenu(fileName = "DungeonSO", menuName = "Scriptable Objects/DungeonSO")]
public class DungeonSO : ScriptableObject
{
    public int iterations = 10;
    public int walkLength = 10;
    public bool startRandomlyEachIteration = true;
}
