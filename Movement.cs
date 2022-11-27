using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float motorThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    Rigidbody myRigidBody;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            myRigidBody.AddRelativeForce(Vector3.up * motorThrust * Time.deltaTime) ;
        }
    }
    //void ProcessRotation()
    //{
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        Debug.Log("Press A");
    //    }
    //    else if (Input.GetKey(KeyCode.D))
    //    {
    //        Debug.Log("Press D");
    //    }
    //}
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(rotationThisFrame * Time.deltaTime * Vector3.forward);
    }
}
