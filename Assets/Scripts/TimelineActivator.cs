using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class TimelineActivator : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public string playerTAG;
    public Transform interactionLocation;
    public bool autoActivate = false;

    [Header("Activation Zone Event")]
    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerExit;

    [Header("Timeline Events")]
    public UnityEvent OnTimelineStart;
    public UnityEvent OnTimelineEnd;

    private bool isPlaying;
    private bool playerInside;
    private Transform playerTransform;


    // Update is called once per frame
    void Update()
    {
        if(playerInside && !isPlaying && autoActivate){

            PlayTimeline();
        }
    }

    private void PlayTimeline(){

        if(playerTransform && interactionLocation){

            playerTransform.SetPositionAndRotation(interactionLocation.position, interactionLocation.rotation); 
        }

        if(autoActivate) playerInside = false;

        if(playableDirector){
            playableDirector.Play();

            OnTimelineStart.Invoke();
        }

        isPlaying = true;
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag.Equals(playerTAG)){
            playerInside = true;
            playerTransform = other.transform;
            OnPlayerEnter.Invoke();
            PlayerTPSController.OnInteractionInput += PlayTimeline;
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag.Equals(playerTAG)){
            playerInside = false;
            playerTransform = null;
            OnPlayerExit.Invoke();
            PlayerTPSController.OnInteractionInput -= PlayTimeline;
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector playable){
        OnTimelineEnd.Invoke();
        isPlaying = false;
    }

    void OnEnable(){
        playableDirector.stopped += OnPlayableDirectorStopped;
    }

    void Disable(){
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }
}
