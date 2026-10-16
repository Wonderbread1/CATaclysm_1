
using UnityEngine;

public class TNTAnimationEvent : MonoBehaviour
{
    private TNTExplosion parentExplosion;

    private void Awake()
    {
        parentExplosion = GetComponentInParent<TNTExplosion>();
    }

    public void Explode()
    {
        if (parentExplosion != null)
        {
            parentExplosion.Explode();
        }
        else
        {
            Debug.LogWarning("TNT parent explosion script not found!");
        }
    }
}
