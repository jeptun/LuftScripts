using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] float motorThrust = 1000f;
    [SerializeField] float rightThrust = 300f;
    [SerializeField] float leftThrust = 300f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip mainEngineOff;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem gasRefStation;
    [SerializeField] Text UIGas;

    //Fuel-----------------------------------------------------
    //
    [SerializeField] float Gas = 100.0f;
    [SerializeField] float MaxGas = 100.0f;
    [SerializeField] int playerScore = 0;
    private float gasPlus = 0.010f;

    //public GameObject fuelStation;
    //---------------------------------------------------------
   
    private const float GasDecreasePerFrame = 1.0f;
    
    //---------------------------------------------------------
    Rigidbody myRigidBody;
    AudioSource mySoundSource;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        mySoundSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Fuel":
                Debug.Log("Fuel");
                break;
            case "Gold":
                Debug.Log("gold");
                break;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.CompareTag("Player"))
        {
            gasRefStation.Play();
        }
        
        switch (collider.gameObject.layer)
        {
            case 10:
                Debug.Log("Wind");
                //minusMotorThrust();
                break;
            case 6:
                Debug.Log("Default");
                //plusMotorThrust();
                break;

        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            gasRefStation.Stop();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            // pokud ano, zvýší se stav paliva pozvolna
            if (Gas < MaxGas)
            {
                Gas += gasPlus;
                UIGas.text = $"FUEL: {(int)Gas % 1000:0}";
                gasRefStation.Play();
            }
            
        }
    }
    void FixedUpdate()
    {
        bool isFlying = Input.GetKey(KeyCode.Space);
        bool isFlyingLeft = Input.GetKey(KeyCode.A);
        bool isFlyingRight = Input.GetKey(KeyCode.D);

        if (Gas != 0)
        {
            mySoundSource.UnPause();
            ProcessThrust();
            SidewaysMovement();
        }
        else if (Gas == 0)
        {
            mySoundSource.Pause();
            mainEngineParticle.Stop();
        }

        ProcessRotation();   

        if (isFlying || isFlyingLeft || isFlyingRight)
        {
            Gas = Mathf.Clamp(Gas - (GasDecreasePerFrame * Time.deltaTime), 0.0f, MaxGas);
        }

        UIGas.text = $"FUEL: {(int)Gas % 1000:0}";
    }
    private void ProcessThrust()
    {
      
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }  
    }
    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            ApplyRotation(-rotationThrust);
        }
       
    }
    private void SidewaysMovement()
    {
      
        if (Input.GetKey(KeyCode.A))
        {
            LeftTrhusting();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RightThrustin();
        }
    }
    private void StartThrusting()
    {
        myRigidBody.AddRelativeForce(Vector3.up * motorThrust * Time.fixedDeltaTime);

        if (!mySoundSource.isPlaying)
        {
            mainEngineParticle.Play();
             mySoundSource.PlayOneShot(mainEngine);
        }
    }
    private void LeftTrhusting()
    {
       
        myRigidBody.AddRelativeForce(Vector3.left * leftThrust * Time.fixedDeltaTime);

        if (!mySoundSource.isPlaying)
        {
            mainEngineParticle.Play();
            mySoundSource.PlayOneShot(mainEngine);
        }
    }
    private void RightThrustin()
    {

        myRigidBody.AddRelativeForce(Vector3.right * rightThrust * Time.fixedDeltaTime);

        if (!mySoundSource.isPlaying)
        {
            mainEngineParticle.Play();
            mySoundSource.PlayOneShot(mainEngine);
        }
    }
    private void StopThrusting()
    {
        mainEngineParticle.Stop();
        mySoundSource.Stop();
    }
    public void ApplyRotation(float rotationThisFrame)
    {
        myRigidBody.freezeRotation = true;      // umozni manualni rotaci
        transform.Rotate(rotationThisFrame * Time.deltaTime * Vector3.forward);
        myRigidBody.freezeRotation = false; // zakaze manualni rotaci
    }
}
