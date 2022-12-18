using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    //Movement-------------------------------------------------
    [SerializeField] float motorThrust = 1000f;
    [SerializeField] float rightThrust = 300f;
    [SerializeField] float leftThrust = 300f;
    [SerializeField] float rotationThrust = 1f;
   // [SerializeField] AudioClip motorThrustSound;
    //[SerializeField] AudioClip motorThrustSoundOff;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem gasRefStation;
    [SerializeField] Text UIGas;
    //Fuel-----------------------------------------------------
    [SerializeField] float Gas = 100.0f;
    [SerializeField] float MaxGas = 100.0f;
    [Range(-1, 1)]
    [SerializeField] float gasPlus = 0.010f;
    private const float GasDecreasePerFrame = 1.0f;
    //---------------------------------------------------------
    private Rigidbody myRigidBody;

    [SerializeField] AudioSource mySoundSource;
    //TODO optimalizovat zvuk. novou tridou. 
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        mySoundSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
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
      bool keyFlyingUp = Input.GetKey(KeyCode.Space);
      bool keyFlyingLeft = Input.GetKey(KeyCode.A);
      bool keyFlyingRight = Input.GetKey(KeyCode.D);
        if (Gas != 0)
        {
            mySoundSource.UnPause();
            ProcessThrust();
        }
        else if (Gas == 0)
        {
            mySoundSource.Pause();
            mainEngineParticle.Stop();
        }

        ProcessRotation();   

        if (keyFlyingUp || keyFlyingLeft || keyFlyingRight)
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
        else if (Input.GetKey(KeyCode.A))
        {
            LeftTrhusting();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RightThrustin();
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
    private void StartThrusting()
    {
        myRigidBody.AddRelativeForce(Vector3.up * motorThrust * Time.fixedDeltaTime);

        if (!mySoundSource.isPlaying)
        {
            mainEngineParticle.Play();
            mySoundSource.pitch = Random.Range(.8f, 1.2f);
            mySoundSource.Play();
           // mySoundSource.PlayOneShot(motorThrustSound);
        }
    }
    private void LeftTrhusting()
    {
        myRigidBody.AddRelativeForce(Vector3.left * leftThrust * Time.fixedDeltaTime);

        if (!mySoundSource.isPlaying)
        {
            mainEngineParticle.Play();
            mySoundSource.pitch = Random.Range(.8f, 1.2f);
            mySoundSource.Play();
            // mySoundSource.PlayOneShot(motorThrustSound);
        }
    }
    private void RightThrustin()
    {
        myRigidBody.AddRelativeForce(Vector3.right * rightThrust * Time.fixedDeltaTime);

        if (!mySoundSource.isPlaying)
        {
            mainEngineParticle.Play();
            mySoundSource.pitch = Random.Range(.8f, 1.2f);
            mySoundSource.Play();
            // mySoundSource.PlayOneShot(motorThrustSound);
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
