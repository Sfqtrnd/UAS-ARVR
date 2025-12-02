using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTargetHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToSpawn; // Assign your 3D model prefab in the Inspector

    private ARTrackedImageManager _arTrackedImageManager;

    void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // Instantiate your 3D content as a child of the tracked image
            GameObject spawnedObject = Instantiate(prefabToSpawn, trackedImage.transform);
            // You can further customize spawnedObject based on trackedImage.referenceImage.name
        }

        // Handle updated and removed images as needed
    }
}