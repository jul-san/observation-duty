using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour {
    public AudioSource scream;
    public GameObject thePlayer;
    public GameObject JumpCam;
    public GameObject Flash;

    void Start() {
        StartCoroutine(EndJump());
    }

    IEnumerator EndJump() {
        yield return new WaitForSeconds(5);
        Scream.Play();
        JumpCam.SetActive(true);
        thePlayer.SetActive(false);
        Flash.SetActive(true);
        yield return new WaitForSeconds(2.03f);
        JumpCam.SetActive(false);
        thePlayer.SetActive(true);
        Flash.SetActive(false);
    }




}