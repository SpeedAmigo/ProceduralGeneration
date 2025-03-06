using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkGenerator
{
    [SerializeField]
    private int corridorLenght = 14;
    [SerializeField]
    private int corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomPercent = 0.8f;
    
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new();
        HashSet<Vector2Int> potentialRoomPos = new();
        
        CreateCorridors(floorPositions, potentialRoomPos);
        
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPos);
        
        floorPositions.UnionWith(roomPositions);
        
        visualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, visualizer);
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPos)
    {
        HashSet<Vector2Int> roomPositions = new();
        int roomsToCreateCount = Mathf.RoundToInt(potentialRoomPos.Count * roomPercent);

        // this generates random room placement outside of seed
        //List<Vector2Int> roomsToCreate = potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomsToCreateCount).ToList();
        
        List<Vector2Int> roomsToCreate = potentialRoomPos.OrderBy(x => UnityEngine.Random.value).Take(roomsToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(dungeonSO, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }
    
    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPos)
    {
        var currentPositon = startPosition;
        potentialRoomPos.Add(currentPositon);

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPositon, corridorLenght);
            currentPositon = corridor[corridor.Count - 1];
            potentialRoomPos.Add(currentPositon);
            floorPositions.UnionWith(corridor);
        }
    }
}
