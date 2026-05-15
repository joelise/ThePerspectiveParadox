using UnityEngine;

public class MaskObject : MonoBehaviour
{
    public GameObject[] maskObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < maskObject.Length; i++)
        {
            maskObject[i].GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
