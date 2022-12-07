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


    [SerializeField] float LevelLoadDelay = 1f;
    [SerializeField] float delayLevelFinishTime = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    [SerializeField] Text UIGas;

    Rigidbody myRigidBody;
    AudioSource mySoundSource;
    AudioSource audioSource;

    bool isTransitioning = false;
    public float Gas = 100.0f;
    public float MaxGas = 100.0f;

    private const float GasDecreasePerFrame = 1.0f;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        mySoundSource = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
    }

    private void GetGas()
    {
        float gasIncrement = Gas + 1;
        Gas = gasIncrement;
        return;
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
        if (Gas != 0)
        {
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
            Debug.Log("Hodnota Gasu" + Gas);
        }

    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
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
     void StartThrusting()
    {
        myRigidBody.AddRelativeForce(Vector3.up * motorThrust * Time.fixedDeltaTime);

        if (!mySoundSource.isPlaying)
        {
            mainEngineParticle.Play();
            mySoundSource.PlayOneShot(mainEngine);
        }
    }
     void StopThrusting()
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

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        crashParticles.Play();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), LevelLoadDelay);
    }
    void FinishSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
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
