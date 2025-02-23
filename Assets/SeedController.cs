using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SeedController : MonoBehaviour
{
    private List<GameObject> boxList = new();
    private float[] noiseValues;
    private GameObject[] boxes;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private string seedString;
    [SerializeField] private int seed;

    private SeedManager seedManager = new SeedManager();

    private void Start()
    {
        seedManager.SetRandomSeed();
        seed = seedManager.Seed;
        inputField.text = seedManager.SeedString;
    }
    
    public void InputSeed()
    {
        seedString = inputField.text;
        seedManager.SetSeed(seedString);
        seed = seedManager.Seed;
    }
    
    public void GenerateNumbers()
    {
        Random.InitState(seedManager.Seed);
        noiseValues = new float[10];
        
        Debug.Log($"Input seed: {seedManager.SeedString}, World seed: {seedManager.Seed}");

        for (int i = 0; i < noiseValues.Length; i++)
        {
            noiseValues[i] = Random.value * 10;
            Debug.Log((int)noiseValues[i]);
        } 
    }

    public void CreateBoxes()
    {
        if (boxList.Count > 0)
        {
            foreach (GameObject box in boxList)
            {
                Destroy(box);
            }
            boxList.Clear();
        }
        
        Random.InitState(seedManager.Seed);
        boxes = new GameObject[5];

        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            boxes[i].transform.position = new Vector3(
                Random.Range(-5, 5),
                Random.Range(-5, 5),
                0);
            boxList.Add(boxes[i]);
            Debug.Log(boxes[i].transform.position);
        }
    }
}
