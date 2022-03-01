using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public UI ui;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Debug.Log(hitInfo.collider.gameObject.name);

                if (hitInfo.collider.gameObject.name == ("Brick(Clone)"))
                {
                    Destroy(hitInfo.collider.gameObject);
                    ui.UpdateScore(100);
                }
                else if (hitInfo.collider.gameObject.name == ("QuestionBlock(Clone)"))
                {
                    ui.UpdateScore(1000);
                    ui.UpdateCoins();
                }
                float l = 25f;
                Debug.DrawLine(hitInfo.point + Vector3.left * l, hitInfo.point + Vector3.right * l, Color.green);
                Debug.DrawLine(hitInfo.point + Vector3.up * l, hitInfo.point + Vector3.down * l, Color.green);
            }
            else
            {
                Debug.Log("Nothing hit.");
            }
        }
    }
}
