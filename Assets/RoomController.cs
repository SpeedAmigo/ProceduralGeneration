using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<Vector2> cellLocations = new();
    
    public List<Vector2> GetCellLocations()
    {
        cellLocations = new(transform.childCount);
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector2 pos = new Vector2(
                transform.GetChild(i).localPosition.x,
                transform.GetChild(i).localPosition.y);
            
            cellLocations.Add(pos);
        }
        return cellLocations;
    }
}
