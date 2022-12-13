using GasStation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    /* PARAMETERS - for tuning, typicaliy set in the editor
     *  CAHCE - e.g referencis for readibility  or speed 
     *  State -  private instance (member) variables
     */

    [SerializeField] float motorThrust = 500f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip mainEngineOff;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] Text UIGas;

    //Fuel-----------------------------------------------------
    //
    public float Gas = 100.0f;
    public float MaxGas = 100.0f;
    public int playerScore = 0;

    public GameObject fuelStation;
    //---------------------------------------------------------
   
    private const float GasDecreasePerFrame = 1.0f;
    
    //---------------------------------------------------------
    Rigidbody myRigidBody;
    AudioSource mySoundSource;

    public Rigidbody rb;
    public float forceX;
    public float forceY;
    public float forceZ;
 



    //public GameObject[] objs;

    //private int myWindZone;

    //TODO Dod�lat Array cyklus pro fuelStation
    //[System.Serializable]
    //public class Data
    //{
    //    public GameObject obj;
    //    public bool objectBool;
    //}
    //public Data[] fuelStation;

    //private void Awake()
    //{
    //    myWindZone = LayerMask.NameToLayer("MyWindZone");
    //}

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        mySoundSource = GetComponent<AudioSource>();
      

        // TODO Dod�lat Array cyklus pro fuelStation
        // GameObject[] newObjs = new GameObject[objs.Length];
        // var objs = GameObject.FindGameObjectsWithTag("Fuel");
        // create new Data array with the same number of elements as we have in the objs array.
        // fuelStation = new Data[objs.Length];
        // for (int i = 0; i < objs.Length; i++)
        //     {
        //     create new Data object
        //     var tmp = new Data();
        //     set the values you want.
        //     tmp.obj = objs[i];
        //     tmp.objectBool = false;
        //     store the Data object in our dataArray
        //     fuelStation[i] = tmp;
        //     }

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
   
        switch (collider.gameObject.layer)
        {
            case 10:
                Debug.Log("Wind");
                AddForce();
                //minusMotorThrust();
                break;
            case 6:
                Debug.Log("Default");
                //plusMotorThrust();
                break;

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

        // pokud se vozidlo dot�k� palivov� stanice a m� m�n� paliva ne� je pln� hodnota
        if (IsTouchingFuelStation() && Gas < MaxGas)
        {
            // pokud ano, zv��� se stav paliva pozvolna
            Gas = Mathf.Min(Gas + Time.deltaTime, 100f);
        }

        UIGas.text = Gas.ToString();

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
    bool IsTouchingFuelStation()
    {
        // z�sk�n� Collider komponenty vozidla
        Collider vehicleCollider = GetComponent<Collider>();

        // z�sk�n� Collider komponenty palivov� stanice
        Collider fuelStationCollider = fuelStation.GetComponent<Collider>();

        // zji�t�n�, zda vozidlo a palivov� stanice se dot�kaj�
        return vehicleCollider.bounds.Intersects(fuelStationCollider.bounds);
    }

    void AddForce()
    {
        Debug.Log("AddForce");
        rb.AddForce(forceX, forceY, forceZ);
    }

    //void minusMotorThrust()
    //{
    //    Debug.Log("minusMotorThrust");
    //    motorThrust -= 500;
    //}

    //void plusMotorThrust()
    //{
    //    Debug.Log("plusMotorThrust");
    //    motorThrust += 1000;
    //}

}
