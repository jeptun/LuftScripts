using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] float motorThrust = 500f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip mainEngineOff;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem gasRefStation;
    [SerializeField] Text UIGas;

    //Fuel-----------------------------------------------------
    //
    public float Gas = 100.0f;
    public float MaxGas = 100.0f;
    public int playerScore = 0;
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

        if (isFlying)
        {
            Gas = Mathf.Clamp(Gas - (GasDecreasePerFrame * Time.deltaTime), 0.0f, MaxGas);
        }

        // pokud se vozidlo dotýká palivové stanice a má ménì paliva než je plná hodnota
        //if (IsTouchingFuelStation() && Gas < MaxGas)
        //{
        //    // pokud ano, zvýší se stav paliva pozvolna
        //    Gas = Mathf.Min(Gas + Time.deltaTime, 100f);
        //}

        //UIGas.text ={(int)Gas.ToString()};
        UIGas.text = $"FUEL: {(int)Gas % 1000:0}";
    }
    void ProcessThrust()
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
    private void StartThrusting()
    {
        myRigidBody.AddRelativeForce(Vector3.up * motorThrust * Time.fixedDeltaTime);

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
    //bool IsTouchingFuelStation()
    //{
    //    // získání Collider komponenty vozidla
    //    Collider vehicleCollider = GetComponent<Collider>();
    //    // získání Collider komponenty palivové stanice
    //    Collider fuelStationCollider = fuelStation.GetComponent<Collider>();
    //    // zjištìní, zda vozidlo a palivová stanice se dotýkají
    //    return vehicleCollider.bounds.Intersects(fuelStationCollider.bounds);
    //}
}
