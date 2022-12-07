using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    /* PARAMETERS - for tuning, typicaliy set in the editor
     *  CAHCE - e.g referencis for readibility  or speed 
     *  State -  private instance (member) variables
     */

    [SerializeField] float motorThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticle;


    Rigidbody myRigidBody;
    AudioSource mySoundSource;

    //-------------------------------------------------------

    [SerializeField] float LevelLoadDelay = 1f;
    [SerializeField] float delayLevelFinishTime = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    //Movement movement;
    //[SerializeField] GameObject Movement;

    AudioSource audioSource;
    bool isTransitioning = false;

    public float Gas = 100.0f;
    public float MaxGas = 100.0f;
    [SerializeField] Text UIGas;

    private const float GasDecreasePerFrame = 1.0f;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        mySoundSource = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

    }


    void FixedUpdate()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                FinishSequence();
                Debug.Log("Finish");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                GetGas();
                Debug.Log(Gas);
                break;
            case "Gold":
                Debug.Log("gold");
                break;
            default:
                StartCrashSequence();
                Debug.Log("Sorry");
                break;
        }
    }


    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
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
          //  Debug.Log("Hodnota Gasu" + Gas);

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

    private void GetGas()
    {
        float gasIncrement = Gas + 1;
        Gas = gasIncrement;
        return;
    }

    //Todo Sound Particle
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        crashParticles.Play();
        audioSource.PlayOneShot(crash);
        GetComponent<GameManager>().enabled = false;
        Invoke(nameof(ReloadLevel), LevelLoadDelay);
    }
    void FinishSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<GameManager>().enabled = false;
        Invoke(nameof(NextLevel), delayLevelFinishTime);
    }
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
