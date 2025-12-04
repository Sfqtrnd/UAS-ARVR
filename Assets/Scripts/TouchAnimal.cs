using UnityEngine;
using UnityEngine.InputSystem; // new Input System

public class TouchAnimal : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isSoundEnabled = true; // Toggle state for sound

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        // Ensure audio source is not playing initially
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void Update()
    {
        if (Touchscreen.current == null) return;

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(touchPos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                {
                    // Toggle sound on/off with each touch
                    isSoundEnabled = !isSoundEnabled; // Toggle the state
                    
                    if (isSoundEnabled)
                    {
                        // Play the sound if enabled
                        if (audioSource != null)
                        {
                            if (audioSource.isPlaying)
                            {
                                audioSource.Stop(); // Stop any currently playing sound
                            }
                            audioSource.Play();
                        }
                    }
                    else
                    {
                        // Stop the sound if disabled
                        if (audioSource != null && audioSource.isPlaying)
                        {
                            audioSource.Stop();
                        }
                    }
                    
                    // Optionally add visual feedback to show toggle state
                    ToggleVisualFeedback();
                }
            }
        }
    }

    // Method to toggle the sound state
    public void ToggleSound()
    {
        isSoundEnabled = !isSoundEnabled;
    }

    // Method to get the current sound state
    public bool IsSoundEnabled()
    {
        return isSoundEnabled;
    }

    // Optional: Visual feedback method that can be implemented by child classes or extended
    private void ToggleVisualFeedback()
    {
        // You can add visual feedback here, such as:
        // - Changing material color
        // - Activating a particle effect
        // - Scaling the object slightly
        
        // Example: Make the object slightly larger when sound is enabled
        float scaleMultiplier = isSoundEnabled ? 1.1f : 1.0f;
        transform.localScale = Vector3.one * scaleMultiplier;
        
        // Reset scale back after a short time
        Invoke("ResetScale", 0.1f);
    }

    private void ResetScale()
    {
        transform.localScale = Vector3.one;
    }
}