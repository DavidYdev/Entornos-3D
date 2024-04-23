using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTPSController : MonoBehaviour
{
    public Camera cam;
    private InputData input;
    private CharacterAnimBasedMovement characterMovement;

    public bool blockInput {get; set;}

    public bool onInteractionZone{get;set; }

    public static event Action OnInteractionInput;
    // Start is called before the first frame update
    void Start()
    {
        characterMovement=GetComponent<CharacterAnimBasedMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if(blockInput){
            input.resetInput();
        }else{
            input.getInput();
        }

        if(onInteractionZone && input.jump){

                OnInteractionInput?.Invoke();
        }else{

            characterMovement.moveCharacter(input.hMovement, input.vMovement, cam, input.jump, input.dash);
        }
    }
}