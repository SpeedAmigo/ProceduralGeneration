using UnityEngine;

public class Debugger : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 worldPosition = transform.TransformPoint(transform.GetChild(i).localPosition);
            Debug.Log(worldPosition);
        }
    }
}
