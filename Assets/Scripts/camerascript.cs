using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    
    public float movementSpeed;
    public float lerpDrag;
    public float rotationSpeed;
    void LateUpdate()
    {
        MoveCamera();
        RotateCamera();
    }

    void MoveCamera()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical)*movementSpeed;
        movement = transform.TransformDirection(movement);
        transform.position = Vector3.Lerp(transform.position, transform.position+movement, lerpDrag * Time.deltaTime);

    }
    void RotateCamera()
    {
        if (Input.GetKey(KeyCode.E)){
            transform.RotateAround(Vector3.up, rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(Vector3.up, -rotationSpeed);
        }
    }
}
