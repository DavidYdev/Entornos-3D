using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTPSController : MonoBehaviour
{
    public Camera cam;
    private InputData input;
    private CharacterAnimBasedMovement characterMovement;

    public bool onInteractionZone{get;set; }
    // Start is called before the first frame update
    void Start()
    {
        input = new InputData(); // Inicializar la variable input
        characterMovement = GetComponent<CharacterAnimBasedMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        input.getInput();
        characterMovement.moveCharacter(input.hMovement, input.vMovement, cam, input.jump, input.dash);
    }
}