using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
public class ArenaScaleController : MonoBehaviour
{
    ARSessionOrigin _arSessionOrigin;
    public Slider scaleSlider, rotateSlider;

    private void Awake()
    {
        _arSessionOrigin = GetComponent<ARSessionOrigin>();
    }

    public void Start()
    {
        scaleSlider.onValueChanged.AddListener(OnSliderScale);
        rotateSlider.onValueChanged.AddListener(OnSliderRotate);
    }

    public void OnSliderScale(float sliderValue)
    {
        if(scaleSlider != null)
        {
            // Limits resize if values is less than 1
            if(sliderValue < 1f)
                sliderValue = 1f;

            _arSessionOrigin.transform.localScale = Vector3.one * sliderValue;
        }
    }

    public void OnSliderRotate(float sliderValue)
    {
        if (scaleSlider != null)
        {
            _arSessionOrigin.transform.localEulerAngles = new Vector3(0.0f, sliderValue, 0.0f);
        }
    }


}
