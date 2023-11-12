using System.Threading;
//using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    Vector3 position = new Vector3(4.0f,0,0);

    public float spawnRate = 0.5f;
    public float minHeight = -2f;
    public float maxHeight = 3f;
    Timer spawntimer;
    bool init = false;

    private void Awake()
    {
        EventManager.AddListener(EventName.GameStartedEvent, HandleGameStartEvent);
        spawntimer = gameObject.AddComponent<Timer>();
        spawntimer.Duration = spawnRate;
        spawntimer.AddTimerFinishedEventListener(HandleCooldownTimerFinishedEvent);
    }
    private void HandleGameStartEvent(int notUsed)
    {
        if (!init)
        {
            spawntimer.Run();
            init = true;
        }
    }
    private void HandleCooldownTimerFinishedEvent()
    {
        Spawn();
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(prefab, position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
        spawntimer.Run();
    }

}
