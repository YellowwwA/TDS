using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMove : MonoBehaviour
{
    public float speed = 1.0f;
    Vector3 moveDir = new Vector3Int(1, 0, 0);
    void Update()
    {
        transform.position += moveDir * speed * Time.deltaTime;

        //카메라 플레이어 따라 이동
        Camera.main.transform.position = new Vector3(transform.position.x + 5.0f, transform.position.y + 2.0f, -10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Stop"))
        {
            speed = 0.0f;
        }
    }

}
