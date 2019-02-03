using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AndroidPermissionsUsageExample : MonoBehaviour
{
    public Text updateText;
    private const string MIC_PERMISSION = "android.permission.RECORD_AUDIO";

    private void Start()
    {
        OnMicRequestPermission();
    }

    private void Update()
    {
    }

    // Function to be called first (by UI button)
    // For example, click on Avatar to change it from the device gallery
    public void OnMicRequestPermission()
    {
        if (!CheckPermissions())
        {
            updateText.text = "Missing permission to use the mic, please grant the permission first";

            // Your code to show in-game pop-up with the explanation why you need this permission (required for Google Featuring program)
            // This pop-up should include a button "Grant Access" linked to the function "OnGrantButtonPress" below

            OnGrantButtonPress();

            return;
        }
        updateText.text = "Permission Granted!";
    }

    private bool CheckPermissions()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            return true;
        }

        return AndroidPermissionsManager.IsPermissionGranted(MIC_PERMISSION);
    }

    public void OnGrantButtonPress()
    {
        AndroidPermissionsManager.RequestPermission(new []{ MIC_PERMISSION }, new AndroidPermissionCallback(
            grantedPermission =>
            {
                // The permission was successfully granted, restart the change avatar routine
                OnMicRequestPermission();
            },
            deniedPermission =>
            {
                updateText.text = "Permission Denied";
                // The permission was denied
            },
            deniedPermissionAndDontAskAgain =>
            {
                updateText.text = "Permission Denied, dont ask again!";
                // The permission was denied, and the user has selected "Don't ask again"
                // Show in-game pop-up message stating that the user can change permissions in Android Application Settings
                // if he changes his mind (also required by Google Featuring program)
            }));
    }
}
