
using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    public float floatHeight = 0.12f;
    public float floatSpeed = 2f;

    public Transform shadow;
    public float shadowScaleChange = 0.15f;

    private Vector3 startPosition;
    private Vector3 shadowStartScale;

    void Start()
    {
        startPosition = transform.localPosition;

        if (shadow != null)
            shadowStartScale = shadow.localScale;
    }

    void Update()
    {
        float wave = Mathf.Sin(Time.time * floatSpeed);

        transform.localPosition =
            startPosition + Vector3.up * (wave * floatHeight);

        if (shadow != null)
        {
            float scaleMultiplier = 1f - wave * shadowScaleChange;
            shadow.localScale = shadowStartScale * scaleMultiplier;
        }
    }
}
