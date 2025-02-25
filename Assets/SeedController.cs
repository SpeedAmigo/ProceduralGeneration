using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Random = UnityEngine.Random;
using DelaunatorSharp;
using Unity.VisualScripting;

public class SeedController : MonoBehaviour
{
    public List<GameObject> roomPrefabs;
    public int maxRooms;
    public Vector2Int mapSize;
    
    [SerializeField] private TMP_InputField inputField;
    
    private List<Vector2> roomPositions = new();
    private List<GameObject> boxList = new();
    private HashSet<Vector2> occupiedCells = new();
    private Delaunator delaunator;

    private bool validPos;
    
    public void InputSeed()
    {
        SeedManager.SetSeed(inputField.text);
    }
    
    public void GenerateRooms()
    {
        if (boxList.Count > 0)
        {
            foreach (GameObject box in boxList)
            {
                Destroy(box);
            }
            boxList.Clear();
        }
        
        Random.InitState(SeedManager.Seed);
        
        for (int i = 0; i < maxRooms; i++)
        {
            int flipIndex = Random.Range(0, 1000) % 2 == 0 ? 90 : 0;
            int index = Random.Range(0, roomPrefabs.Count);
            
            GameObject room = Instantiate(roomPrefabs[index], Vector2.zero, Quaternion.identity);
            RoomController roomScript = room.GetComponent<RoomController>();
            
            Vector2Int position;
            do
            {
                validPos = true;
                
                position = new Vector2Int(
                    Random.Range(-mapSize.x, mapSize.x),
                    Random.Range(-mapSize.y, mapSize.y));
                
                foreach (Vector2 localPos in roomScript.GetCellLocations())
                {
                    Vector2 worldPos = (Vector2)position + localPos;
                    if (occupiedCells.Contains(worldPos))
                    {
                        validPos = false;
                        break;
                    }
                }
                
            } while (!validPos);

            room.transform.position = new Vector2(position.x, position.y);
            room.transform.eulerAngles = new Vector3(0, 0, flipIndex);
            
            
            Debug.Log(room.transform.position);
            boxList.Add(room);
            roomPositions.Add(new Vector2(room.transform.position.x, room.transform.position.y));
            
            foreach (Vector2 pos in roomScript.GetCellLocations())
            {
                occupiedCells.Add(room.transform.position);
                occupiedCells.Add(room.transform.TransformPoint(pos));
            }
        }
        CreateConnections();
        foreach (var value in occupiedCells)
        {
            Debug.Log(value);
        }
    }

    private void CreateConnections()
    {
        List<IPoint> coords = new();
        foreach (var point in roomPositions)
        {
            coords.Add(new Point(point.x, point.y));
        }
        
        delaunator = new Delaunator(coords.ToArray());

        for (int i = 0; i < delaunator.Triangles.Length; i+=3)
        {
            int a = delaunator.Triangles[i];
            int b = delaunator.Triangles[i + 1];
            int c = delaunator.Triangles[i + 2];
            
            Debug.Log($"Triangle: {roomPositions[a]}, {roomPositions[b]}, {roomPositions[c]}");
        }
    }
    
    private void OnDrawGizmos()
    {
        if (delaunator == null) return;
        
        Gizmos.color = Color.yellow;
        for (int i = 0; i < delaunator.Triangles.Length; i += 3)
        {
            int a = delaunator.Triangles[i];
            int b = delaunator.Triangles[i + 1];
            int c = delaunator.Triangles[i + 2];
            
            Gizmos.DrawLine(roomPositions[a], roomPositions[b]);
            Gizmos.DrawLine(roomPositions[b], roomPositions[c]);
            Gizmos.DrawLine(roomPositions[c], roomPositions[a]);
        }
    }
    
    private void Start()
    {
        SeedManager.SetRandomSeed();
        inputField.text = SeedManager.SeedString;
    }
}
