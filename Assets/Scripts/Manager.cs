using UnityEngine;

public enum Zone {None, Start, Safe, Hunting, Goal}
public enum HunterMode {Scout, Retreat, Rest, Respawn, Hunt, Gather, Leave}
public enum KillerMode {Patrol, Chase, Track}
public enum PreyMode {Flock, Escape, Dead}

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
