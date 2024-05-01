using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public AudioSource scream;
    public GameObject thePlayer;
    public GameObject JumpCam;
    public GameObject Flash;

    public void StartJumpScare()
    {
        StartCoroutine(EndJump());
    }

    IEnumerator EndJump()
    {
        scream.Play();
        JumpCam.SetActive(true);
        thePlayer.SetActive(false);
        Flash.SetActive(true);

        yield return new WaitForSeconds(2.03f);

        JumpCam.SetActive(false);
        thePlayer.SetActive(true);
        Flash.SetActive(false);
    }
}