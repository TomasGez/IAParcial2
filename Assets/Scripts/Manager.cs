using System.Collections.Generic;
using UnityEngine;

public enum Zone {None, Start, Safe, Hunting, Goal}

public class Manager : MonoBehaviour
{
    public static Manager Instance {get; private set;}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
