using System.Collections;
using UnityEngine;

public enum Zone {None, Start, Safe, Hunting, Goal}
public enum HunterMode {Scout, Retreat, Rest, Respawn, Hunt, Gather, Leave}
public enum KillerMode {Patrol, Chase, Track}
public enum PreyMode {Flock, Escape, Dead}

public class Manager : MonoBehaviour
{
    public static Manager Instance {get; private set;}

    [SerializeField] private Transform _spawner;
    [SerializeField] private GameObject _sheepPrefab;
    [SerializeField] private float _spawnTime;


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

    public void RemoveSheep()
    {
        StartCoroutine(SpawnNewSheep());
    }

    private IEnumerator SpawnNewSheep()
    {
        yield return new WaitForSeconds(_spawnTime);
        Instantiate(_sheepPrefab, _spawner.position, Quaternion.identity);
    }
}
