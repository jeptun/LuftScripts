using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    [SerializeField] GameObject scoreText;
    public static int score;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.GetComponent<Text>().text = "SCORE" + score;
    }
}
