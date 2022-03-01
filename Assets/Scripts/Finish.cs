using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private float proximityThreshold = 10f;

    private bool playerWon = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.up);
        RaycastHit hitInfo;

        //Debug.DrawRay(transform.position, Vector3.up * proximityThreshold, Color.red);
        if (Physics.Raycast(ray, out hitInfo, proximityThreshold))
        {
            if (hitInfo.collider.gameObject.tag == ("Player"))
            {
                if (!playerWon)
                {
                    Debug.Log("You win");
                    playerWon = true;
                }
            }
        }   
    }
}
