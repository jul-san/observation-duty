using UnityEngine;

public class PropBehaviour : MonoBehaviour
{
    public float minDelay = 1f;
    public float maxDelay = 150f;
    public float force = 10f;

    private Rigidbody rb;
    private bool hasRolled = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Invoke("RollOffShelf", Random.Range(minDelay, maxDelay));
        }
    }

    private void RollOffShelf()
    {
        if (!hasRolled)
        {
            rb.AddForce(transform.forward * force, ForceMode.Impulse);
            hasRolled = true;
        }
    }
}
