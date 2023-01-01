using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class PokemonNPC : MonoBehaviour
{
    //[SerializeField] private Color defaultColor = Color.white;
    //[SerializeField] private Color charmanderColor = Color.red;
    //[SerializeField] private Color bulbasaurColor = Color.green;
    //[SerializeField] private Color squirtleColor = Color.blue;
    [SerializeField] private SkinnedMeshRenderer defaultColor;
    [SerializeField] private SkinnedMeshRenderer charmanderColor;
    [SerializeField] private SkinnedMeshRenderer bulbasaurColor;
    [SerializeField] private SkinnedMeshRenderer squirtleColor;

   // private SpriteRenderer spriteRenderer;
 
    private SkinnedMeshRenderer meshRender;
    

    private void Start()
    {

      //  spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        meshRender = GetComponentInChildren<SkinnedMeshRenderer>();
        
        //GetComponentInChildren<Renderer>().material.SetTexture("_BaseMap", texture);
    }

    private void Update()
    {
       
        string pokemonName = ((Ink.Runtime.StringValue)DialogueManager
            .GetInstance()
            .GetVariableState("pokemon_name")).value;

        switch (pokemonName)
        {
            case "":
                meshRender = defaultColor;
                meshRender.gameObject.SetActive(true);
                break;
            case "Charmander":
                meshRender = charmanderColor;
                meshRender.gameObject.SetActive(true);
                break;
            case "Bulbasaur":
                meshRender = bulbasaurColor;
                meshRender.gameObject.SetActive(true);
                break;
            case "Squirtle":
                meshRender = squirtleColor;
                meshRender.gameObject.SetActive(true);
                break;
            default:
                Debug.LogWarning("Pokemon name not handled by switch statement: " + pokemonName);
                break;
        }
    }
}
