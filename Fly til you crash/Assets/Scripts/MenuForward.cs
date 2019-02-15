using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuForward : MonoBehaviour
{
    public Transform car;
    Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(1,2,10);
    }
}
