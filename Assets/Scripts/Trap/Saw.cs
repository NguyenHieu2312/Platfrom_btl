using UnityEngine;

public class Saw : MonoBehaviour
{
    //Can use anim to rotate
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float rotateDirection;

    void Update()
    {
        this.transform.Rotate(Vector3.forward * rotateDirection, rotateSpeed); 
    }
}
