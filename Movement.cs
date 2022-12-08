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

    [SerializeField] float motorThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip mainEngineOff;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] Text UIGas;
    //---------------------------------------------------------
    Rigidbody myRigidBody;
    AudioSource mySoundSource;
    //Fuel-----------------------------------------------------
    //
    public float Gas = 100.0f;
    public float MaxGas = 100.0f;
    //---------------------------------------------------------
    private float GasRegenTimer = 0.0f;
    //---------------------------------------------------------

    //TODO->Predelat public na private const
    private const float GasDecreasePerFrame = 1.0f;
    private const float GasIncreasePerFrame = 1.0f;
    private const float GasTimeToRegen = 3.0f;

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
                GetGas();
                Debug.Log(Gas);
                break;
            case "Gold":
                Debug.Log("gold");
                break;
        }
    }
    public void GetGas()
    {
        //float gasIncrement = Gas + 1;
        //Gas = gasIncrement;
        //return;
        if (Gas < MaxGas)
        {

            //Gas += GasIncreasePerFrame * Time.time;
            if (GasRegenTimer >= GasTimeToRegen)
            {
                Gas = Mathf.Clamp(Gas + (GasIncreasePerFrame * Time.time), 0.0f, MaxGas);
                Debug.Log((GasIncreasePerFrame * Time.time) + "Druhy deb");
            }
            else
                GasRegenTimer += Time.time;
        }
       UIGas.text = ((float)Gas).ToString();

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

    //TODO dodelat zvuk

    //public void GassOffSound()
    //{
    //    if (Gas != 0)
    //    {
    //        mySoundSource.PlayOneShot(crash);
    //        return;
    //    }
    //    else
    //    {
    //        mySoundSource.Pause();
    //    }
    
    //}

}
