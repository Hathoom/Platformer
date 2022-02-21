using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVAnimation : MonoBehaviour
{

    private float yval = 0f;
    private float second = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        second += Time.deltaTime;
        if (second >= 1)
        {
            second = 0f;
            yval += 0.2f;
            Material mat = GetComponent<MeshRenderer>().material;
            mat.mainTextureOffset = mat.mainTextureOffset + new Vector2(0f, yval);
        }
        
    }
}
