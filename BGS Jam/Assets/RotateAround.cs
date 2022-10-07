using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    //[SerializeField] private GameObject target;
    [SerializeField] private float rotationSpeed;
    public void Rotate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);

    }
    private void Update()
    {
        Rotate();
    }
}
