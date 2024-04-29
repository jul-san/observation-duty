using UnityEngine;

public class ClipboardInteract : MonoBehaviour
{
    public float interactionDistance = 3f;
    public Transform player;

    private bool showInteractionMessage = false;
    private bool showMessage = false;
    private string message = "Interactable clipboard message";
    private float messageTimer = 0f;
    private const float MessageDuration = 3f;
    private bool timerRunning = false;
    private float timerDuration = 10f;  // Duration of the timer

    private void Update()
    {
        showInteractionMessage = false;
        if (Vector3.Distance(player.position, transform.position) < interactionDistance && IsPlayerLookingAtClipboard())
        {
            showInteractionMessage = true;
        }

        if (showInteractionMessage && Input.GetKeyDown(KeyCode.E))
        {
            showMessage = true;
            messageTimer = 0f;
            StartTimer();
        }

        if (timerRunning)
        {
            UpdateTimer();
        }

        if (showMessage)
        {
            messageTimer += Time.deltaTime;
            if (messageTimer >= MessageDuration)
            {
                showMessage = false;
            }
        }
    }

    private void StartTimer()
    {
        timerRunning = true;
        messageTimer = 0f;  // Resetting the timer when starting
    }

    private void UpdateTimer()
    {
        messageTimer += Time.deltaTime;
        if (messageTimer >= timerDuration)
        {
            timerRunning = false;
            Debug.Log("Timer Finished!");
        }
    }

    private bool IsPlayerLookingAtClipboard()
    {
        RaycastHit hit;
        Vector3 directionToClipboard = (transform.position - player.position).normalized;

        if (Physics.Raycast(player.position, player.forward, out hit, interactionDistance))
        {
            if (hit.transform == transform)
            {
                return true;
            }
        }

        return false;
    }

    private void OnGUI()
    {
        if (showInteractionMessage)
        {
            Rect labelRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50);
            string interactMessage = "Press (E) to read the clipboard";
            GUI.Label(labelRect, interactMessage);
        }

        if (showMessage)
        {
            Rect messageRect = new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 50);
            GUI.Label(messageRect, message);
        }
    }
}
