using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<Vector2Int> cellLocations = new();
    
    public List<Vector2Int> GetCellLocations()
    {
        cellLocations = new(transform.childCount);
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector2Int pos = new Vector2Int(
                (int)transform.GetChild(i).position.x,
                (int)transform.GetChild(i).position.y);
            
            cellLocations.Add(pos);
        }
        return cellLocations;
    }
}
