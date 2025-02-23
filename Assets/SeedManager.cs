using UnityEngine;

public class SeedManager
{
    private int _seed;
    private string _seedString;

    public int Seed
    {
        get { return _seed; }
    }

    public string SeedString
    {
        get { return _seedString; }
    }
    
    public void SetSeed(string input)
    {
        _seedString = input;
        ConvertStringToInt(input);
    }

    public void SetRandomSeed()
    {
        string seedString = "";
        for (int i = 0; i < 10; i++)
        {
            seedString += Random.Range(0, 10).ToString();
        }
        _seedString = seedString;
        
        ConvertStringToInt(seedString);
    }
    
    
    private void ConvertStringToInt(string input)
    {
        long hash = 0;
        foreach (char c in input)
        {
            hash = (hash * 31 + c) % 10000000000L; // Keep within 10 digits
        } 
        _seed = (int)(hash % int.MaxValue); // Ensure it's within int max value
    }
}
