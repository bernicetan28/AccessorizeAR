using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Mediapipe.Unity.CoordinateSystem;
using Mediapipe.Unity;
using mptcc = Mediapipe.Tasks.Components.Containers;
using System;

public class Test : MonoBehaviour
{
    [SerializeField] private RawImage _screen;
    private int _width;
    private int _height;

    private Texture2D _inputTexture;
    private Color32[] _pixelData;
    public MultiHandLandmarkListAnnotation multiHandLandmark;



    // private void HandleEggPositionChanged(int newEggPosition)
    // {
    //     // Debug.Log($"Received updated eggPosition: {newEggPosition}");
    //     // Use the updated eggPosition value here
    // }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => _screen != null);
        // multiHandLandmark.OnEggPositionChanged += HandleEggPositionChanged;

// Vector3 watchPosition = new Vector3(multiHandLandmark.eggPosition, 0, 0);






        // Debug.Log("Initial Watch Position: " + watchPosition);

        // Get the RectTransform component of the RawImage
        RectTransform rectTransform = _screen.GetComponent<RectTransform>();

        // Use the rect width and height from the RectTransform component
        _width = (int)rectTransform.rect.width;
        _height = (int)rectTransform.rect.height;

        // Check if width or height is 0
        if (_width <= 0 || _height <= 0)
        {
            Debug.LogError("Invalid width or height");
            yield break; // Exit the coroutine if dimensions are invalid
        }

        // Set the size of the RawImage to match the screen dimensions
        _screen.rectTransform.sizeDelta = new Vector2(_width, _height);
        _screen.texture = _screen.mainTexture;

        // Create a new Texture2D with the screen dimensions
        _inputTexture = new Texture2D(_width, _height, TextureFormat.RGBA32, false);
        _pixelData = _inputTexture.GetPixels32();

        var screenRect = _screen.GetComponent<RectTransform>().rect;

        // Continuously update the Texture2D
        while (true)
        {
            _inputTexture.SetPixels32(_pixelData);
            _inputTexture.Apply();
            Debug.Log("ScreenRect: " + screenRect);

            yield return null;
        }
    }

    // private void HandleHandDataUpdated(NormalizedLandmarkList hand)
    // {
    //     // Perform your hand calculations here
    //     var midPoint = hand.Landmark[10];
    //     var wristPoint = hand.Landmark[0];

    //     // Use the updated midPoint and wristPoint values
    //     Debug.Log($"Received updated MidPoint: {midPoint}");
    //     Debug.Log($"Received updated WristPoint: {wristPoint}");

    //     // You can also perform additional calculations or actions based on the hand data
    // }

    private void OnDestroy()
    {
                // multiHandLandmark.OnEggPositionChanged -= HandleEggPositionChanged;
        // multiHandLandmark.OnHandDataUpdated -= HandleHandDataUpdated;

        if (_inputTexture != null)
        {
            Destroy(_inputTexture);
        }
    }
}
