using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRayCast : MonoBehaviour
{
    public float proximityThreshold = 10f;
    public float parachuteDrag = 5f;

    private float startingDrag = 0f;

    void Start()
    {
        Rigidbody rbody = GetComponent<Rigidbody>();
        startingDrag = rbody.drag;

        StartCoroutine(UpdatePickingRaycast());
    }

    //pause every frame
    IEnumerator UpdatePickingRaycast()
    {
        while (true)
        {
            Debug.Log($"Current frame number {Time.frameCount}");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                float l = 5f;
                Debug.DrawLine(hitInfo.point + Vector3.left * l, hitInfo.point + Vector3.right * l, Color.green);
                Debug.DrawLine(hitInfo.point + Vector3.up * l, hitInfo.point + Vector3.down * l, Color.green);
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        Rigidbody rbody = GetComponent<Rigidbody>();
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, proximityThreshold))
        {
            Debug.DrawRay(transform.position, Vector3.down * proximityThreshold, Color.red);
        
            rbody.drag = parachuteDrag;
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * proximityThreshold, Color.blue);
            rbody.drag = startingDrag;
        }
    }
}
