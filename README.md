Creating image targets for Augmented Reality (AR) in Unity without Vuforia typically involves using Unity's built-in AR Foundation and its AR Reference Image Library. This approach allows you to define images that your AR application will recognize and track, triggering specific content or interactions when detected.
Here's a general outline of the process:
Set up AR Foundation:
Open your Unity project.
Go to Window > Package Manager.
Search for and install the "AR Foundation" package.
Also, install the platform-specific AR package for your target device (e.g., "ARCore XR Plugin" for Android, "ARKit XR Plugin" for iOS).
Create an AR Session and XR Origin:
In your scene, right-click in the Hierarchy.
Go to XR > AR Session and XR > XR Origin. These are essential components for AR functionality.
Create an AR Reference Image Library:
In your Project window, right-click and go to Create > XR > AR Reference Image Library.
Give it a descriptive name (e.g., "MyImageLibrary").
Add Images to the Library:
Select your newly created AR Reference Image Library asset.
In the Inspector, click the "+" button under "Reference Images" to add new images.
Drag and drop your desired image files (e.g., PNG, JPG) into the "Texture" field for each entry.
Crucially, set the "Physical Size": of each image in meters. This tells AR Foundation the real-world dimensions of your target image, which is vital for accurate tracking and scaling of virtual content.
Optionally, you can give each image a "Name" for easier identification in your scripts.
Configure the AR Tracked Image Manager:
Select the XR Origin GameObject in your scene.
Add an "AR Tracked Image Manager" component to it.
Drag and drop your created "MyImageLibrary" into the "Reference Library" field of the AR Tracked Image Manager.
You can also specify a "Max Number of Moving Images" to track simultaneously.
Handle Image Detection with a Script:
Create a C# script (e.g., "ImageTargetHandler.cs") and attach it to a GameObject in your scene (e.g., the XR Origin).
In this script, you will subscribe to the trackedImagesChanged event of the ARTrackedImageManager.
When an image is detected, added, updated, or removed, this event will fire, providing you with ARTrackedImage objects that contain information about the detected image (its name, position, rotation, etc.).
You can then instantiate or manipulate 3D GameObjects as children of the ARTrackedImage to make them appear on top of the detected image.
Example Script Snippet (conceptual):
Kode

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
By following these steps, you can create and manage image targets for your AR applications in Unity using AR Foundation, without relying on third-party solutions like Vuforia.