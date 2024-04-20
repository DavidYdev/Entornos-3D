using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class CharacterAnimBasedMovement : MonoBehaviour
{
    public float rotationSpeed = 4f;

    public float rotationThreshold = 0.3f;

    public int degreesToTurn = 160;

    [Header("Animator Parameters")]

    public string motionParam = "motion";
    public string mirrorIdleParam ="mirrorIdle";

    public string turn180Param = "turn180";

    [Header("Animation Smoothing")]
    [Range(0, 1f)]

    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;
    private float Speed;

    private Vector3 desiredMoveDirection;
    private CharacterController charactercontroller;
    private Animator animator;

    private bool mirrorIdle;

    private bool turn180;


    // Start is called before the first frame update
    void Start()
    {
        charactercontroller=GetComponent<CharacterController>();
        animator=GetComponent<Animator>();
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;
        
    }

    public void moveCharacter(float hInput,float vInput,Camera cam,bool jump,bool dash){

        //Calculate Input Magnitude
        Speed = new Vector2(hInput, vInput).normalized.sqrMagnitude;

        if(Speed >= Speed-rotationThreshold && dash){
            Speed = 1.5f;
             }

        
        if(Speed > rotationThreshold){

            animator.SetFloat(motionParam, Speed, StartAnimTime, Time.deltaTime);
            Vector3 forward = cam.transform.forward;
            Vector3 right = cam.transform.right;

            forward.y= 0f;
            right.y =0f;

            forward.Normalize();
            right.Normalize();

            desiredMoveDirection = forward * vInput + right * hInput;

            if(Vector3.Angle(transform.forward, desiredMoveDirection) >= degreesToTurn){

                turn180 = true;
                 }else{

                    turn180 = false;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), rotationSpeed * Time.deltaTime);

                    }
            
            animator.SetBool(turn180Param, turn180);
            }

        else if(Speed < rotationThreshold){

            animator.SetBool(mirrorIdleParam, mirrorIdle);
            //Stop the character
            animator.SetFloat(motionParam, Speed, StopAnimTime, Time.deltaTime);
             }
    }

    private void OnAnimatorIK(int layerIndex) {

        if(Speed < rotationThreshold) return;

        float distanceToLeftFoot = Vector3.Distance(transform.position,animator.GetIKPosition(AvatarIKGoal.LeftFoot));
        float distanceToRightFoot = Vector3.Distance(transform.position,animator.GetIKPosition(AvatarIKGoal.RightFoot));

        if(distanceToRightFoot > distanceToLeftFoot){

            mirrorIdle=true;

            }else{

                mirrorIdle=false;
                 }

        }
    
    }