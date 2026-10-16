
using UnityEngine;

public class GolemManager : MonoBehaviour
{
    public static GolemManager Instance { get; private set; }

    [SerializeField] private int maxActiveGolems = 2;

    private int activeGolems = 0;

    private void Awake()
    {
        Instance = this;
    }

    public bool TryActivateGolem()
    {
        if (activeGolems >= maxActiveGolems)
        {
            return false;
        }

        activeGolems++;
        Debug.Log("Golem awakened! Active: " + activeGolems);
        return true;
    }

    public void GolemDied()
    {
        activeGolems = Mathf.Max(0, activeGolems - 1);
        Debug.Log("Golem defeated! Active: " + activeGolems);
    }
}
