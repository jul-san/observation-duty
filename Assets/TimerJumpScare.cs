using UnityEngine;

public class TimerJumpScare : MonoBehaviour
{
    public Timer timer;
    public JumpScare jumpScare;

    private void Start()
    {
        timer.onTimerEnd.AddListener(StartJumpScare);
    }

    private void StartJumpScare()
    {
        jumpScare.StartJumpScare();
    }
}