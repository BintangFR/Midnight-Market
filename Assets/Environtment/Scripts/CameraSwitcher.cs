using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Import the UI namespace.

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Array for cameras
    public TextMeshProUGUI cameraLabel; // Reference to the UI Text element.
    private int currentCameraIndex = 0;

    private void Start()
    {
        // Disable all cameras except camera 01 
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        cameras[0].gameObject.SetActive(true);

        // Set the initial camera label text
        SetCameraLabel(0);
    }

    private void Update()
    {
        // Switch between cameras
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchToNextCamera();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchToPreviousCamera();
        }
        // Back to main scene (Exit CCTV)
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ExitCamera();
        }
    }

    private void SetCameraLabel(int index)
    {
        if (cameraLabel != null)
        {
            cameraLabel.text = "Camera " + (index + 1).ToString("D2");
        }
    }

    private void SwitchToNextCamera()
    {
        // Disable the current camera
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // Move to the next camera 
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Enable the next camera
        cameras[currentCameraIndex].gameObject.SetActive(true);

        // Update the camera label
        SetCameraLabel(currentCameraIndex);
    }

    private void SwitchToPreviousCamera()
    {
        // Disable the current camera
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // Move to the previous camera
        currentCameraIndex = (currentCameraIndex - 1 + cameras.Length) % cameras.Length;

        // Enable the previous camera
        cameras[currentCameraIndex].gameObject.SetActive(true);

        // Update the camera label
        SetCameraLabel(currentCameraIndex);
    }

    private void ExitCamera()
    {
        // Unload Prototype-CCTV System scene
        // SceneManager.UnloadSceneAsync("Prototype-CCTV System");

        SceneManager.LoadScene("Prototype-CCTV");
    }
}
