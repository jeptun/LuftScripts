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
    [SerializeField] ParticleSystem mainEngineParticle;
    

    Rigidbody myRigidBody;
    AudioSource mySoundSource;

    //Fuel

    public float Gas = 100.0f;
    public float MaxGas = 100.0f;
    [SerializeField] Text UIGas;

    private const float GasDecreasePerFrame = 1.0f;

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
    private void GetGas()
    {
        float gasIncrement = Gas + 1;
        Gas = gasIncrement;
        return;
    }

    void FixedUpdate()
    {
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
        //mySoundSource.Pause();
        //mainEngineParticle.Stop();
        //  mySoundSource.PlayOneShot(mainEngineOff);
        bool isFlying = Input.GetKey(KeyCode.Space);
        if (isFlying)
        {
            Gas = Mathf.Clamp(Gas - (GasDecreasePerFrame * Time.deltaTime), 0.0f, MaxGas);
        }

        UIGas.text = ((float)Gas).ToString();
        //else if (Gas < MaxGas)
        //{
        //    if (GasRegenTimer >= GasTimeToRegen)
        //        Gas = Mathf.Clamp(Gas + (GasIncreasePerFrame * Time.deltaTime), 0.0f, MaxGas);
        //    else
        //        GasRegenTimer += Time.deltaTime;
        //}
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
         //mySoundSource.Stop();
         //mainEngineParticle.Stop();
         //mySoundSource.PlayOneShot(mainEngineOff);
     
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

}
