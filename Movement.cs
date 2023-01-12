using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour, IDataPersistence
{
    [Header("Movement Params")]
    [SerializeField] float motorThrust = 1000f;
    [SerializeField] float rightThrust = 500f;
    [SerializeField] float leftThrust = 500f;
    [SerializeField] float forvardThrust = 500f;
    [SerializeField] float backThrust = 500f;
    [SerializeField] float rotationThrust = 1f;
     [SerializeField] AudioClip motorThrustSound;
    [SerializeField] AudioClip crashSound;
    //[SerializeField] AudioClip motorThrustSoundOff;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem gasRefStation;
    [SerializeField] Text UIGas;
    //Fuel-----------------------------------------------------
    [SerializeField] float Gas = 10f;
    [SerializeField] float MaxGas = 100.0f;
    [Range(-1, 1)]
    [SerializeField] float gasPlus = 0.010f;
    private const float GasDecreasePerFrame = 1.0f;
    //RespawnPoint---------------------------------------------
    [Header("Respawn Point")]
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Transform rotationPoint;

    private Rigidbody myRigidBody;
    private BoxCollider boxColl;
    private SphereCollider sphereColl;
    private Movement movement;
    public MeshRenderer meshRenderer;

    private AudioSource mySoundSource;
    //TODO optimalizovat zvuk. novou tridou. 

    private void Awake()
    {
        boxColl = GetComponent<BoxCollider>();
        sphereColl = GetComponent<SphereCollider>();
        movement = GetComponent<Movement>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
        void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        mySoundSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                Debug.Log("Finish");
                break;
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            case "Gold":
                Debug.Log("gold");
                break;
            case "NPC":
                Debug.Log("NPC");
                break;
            default:
                StartCoroutine(HandleDeath());
                Debug.Log("Sorry");
                break;
        }
    }
    //Particle Effect pro pristani k fuel station
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Fuel"))
        {
            gasRefStation.Play();
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Fuel"))
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
              
            }
        }
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
        this.Gas = data.saveGas;
    }
    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
        data.saveGas = this.Gas;
    }

    void FixedUpdate()
    {
      bool keyFlyingUp = Input.GetKey(KeyCode.Space);
      bool keyFlyingLeft = Input.GetKey(KeyCode.A);
      bool keyFlyingRight = Input.GetKey(KeyCode.D);
        bool keyFlyingFrwd = Input.GetKey(KeyCode.W);
        bool keyFlyingBack = Input.GetKey(KeyCode.S);

        // below code just used to test exiting the scene,
        // you probably wouldn't want to actually do this as part of your character controller script.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // save the game anytime before loading a new scene
            DataPersistenceManager.instance.SaveGame();
            // load the main menu scene
            SceneManager.LoadSceneAsync("Menu");
        }

        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;

        }
        
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

       // ProcessRotation();   

        if (keyFlyingUp || keyFlyingLeft || keyFlyingRight || keyFlyingFrwd || keyFlyingBack)
        {
            Gas = Mathf.Clamp(Gas - (GasDecreasePerFrame * Time.deltaTime), 0.0f, MaxGas);
        }

        UIGas.text = $"FUEL: {(int)Gas % 1000:0}";
    }

    private IEnumerator HandleDeath()
    {

        // freeze player movemet
        movement.enabled = false;
        myRigidBody.useGravity = false;
        myRigidBody.velocity = Vector3.zero;

        // prevent other collisions
        boxColl.enabled = false;
        sphereColl.enabled = false;
        mainEngineParticle.Stop();
        crashParticles.Play();
        mySoundSource.pitch = 1;
        mySoundSource.Stop();
        mySoundSource.PlayOneShot(crashSound);
        meshRenderer.enabled = false;
        this.transform.rotation = rotationPoint.rotation;
        this.transform.Rotate(0,0,0);
        // send off event that we died for other components in our system to pick up
        GameEventsManager.instance.PlayerDeath();

        yield return new WaitForSeconds(2.0f);
        Respawn();
    }
    private void Respawn()
    {
        myRigidBody.useGravity = true;
        boxColl.enabled = true;
        sphereColl.enabled = true;
        crashParticles.Stop();
        meshRenderer.enabled = true;
        // move the player to the respawn point
        movement.enabled = true;
        this.transform.position = respawnPoint.position;
        this.transform.rotation = rotationPoint.rotation;
        
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
        else if (Input.GetKey(KeyCode.W))
        {
            ForwardTrhusting();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            BackThrusting();
        }
        else
        {
            StopThrusting();
        }  
    }
    //Vypnuto
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
           // mySoundSource.Play();
            mySoundSource.PlayOneShot(motorThrustSound);
        }
    }
    private void ForwardTrhusting()
    {
        myRigidBody.AddRelativeForce(Vector3.right * forvardThrust * Time.fixedDeltaTime);

        if (!mySoundSource.isPlaying)
        {
            mainEngineParticle.Play();
            mySoundSource.pitch = Random.Range(.8f, 1.2f);
           // mySoundSource.Play();
            mySoundSource.PlayOneShot(motorThrustSound);
        }
    }
    private void BackThrusting()
    {
        myRigidBody.AddRelativeForce(Vector3.left * backThrust * Time.fixedDeltaTime);

        if (!mySoundSource.isPlaying)
        {
            mainEngineParticle.Play();
            mySoundSource.pitch = Random.Range(.8f, 1.2f);
            // mySoundSource.Play();
            mySoundSource.PlayOneShot(motorThrustSound);
        }
    }
    private void LeftTrhusting()
    {
        myRigidBody.AddRelativeForce(Vector3.forward * leftThrust * Time.fixedDeltaTime);

        if (!mySoundSource.isPlaying)
        {
            mainEngineParticle.Play();
            mySoundSource.pitch = Random.Range(.8f, 1.2f);
            // mySoundSource.Play();
            mySoundSource.PlayOneShot(motorThrustSound);
        }
    }
    private void RightThrustin()
    {
        myRigidBody.AddRelativeForce(Vector3.back * rightThrust * Time.fixedDeltaTime);

        if (!mySoundSource.isPlaying)
        {
            mainEngineParticle.Play();
            mySoundSource.pitch = Random.Range(.8f, 1.2f);
           // mySoundSource.Play();
            mySoundSource.PlayOneShot(motorThrustSound);
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
