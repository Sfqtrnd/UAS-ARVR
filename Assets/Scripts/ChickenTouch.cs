using UnityEngine;

public class ChickenTouch : MonoBehaviour
{
    public float knockForce = 1.5f;
    public float upForce = 0.5f;
    public float runSpeed = 0.5f;
    public float runDuration = 1.5f;

    private AudioSource audioSource;
    private Rigidbody rb;
    private bool isRunning = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(t.position);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        OnTouched();
                    }
                }
            }
        }
    }

    private void OnTouched()
    {
        if (isRunning) return;

        if (audioSource != null)
            audioSource.Play();

        StartCoroutine(KnockAndRun());
    }

    private System.Collections.IEnumerator KnockAndRun()
    {
        isRunning = true;

        Vector3 sideDir = -transform.right;
        Vector3 impulse = sideDir * knockForce + Vector3.up * upForce;
        rb.AddForce(impulse, ForceMode.Impulse);

        yield return new WaitForSeconds(0.15f);

        float t = 0f;
        while (t < runDuration)
        {
            Vector3 move = transform.forward * runSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + move);

            t += Time.deltaTime;
            yield return null;
        }

        isRunning = false;
    }
}
