using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 0f;
    public float minHeight = -1f;
    public float maxHeight = 2f;
    private float lastSpawn = 0f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    public void SetSpawnRate(float old_speed, float new_speed)
    {
        var oldSpawnRate = spawnRate;
        if (old_speed >= 0)
        {
            spawnRate = spawnRate * (old_speed / new_speed);
        }
        else {
            spawnRate = new_speed;
        }

        if (old_speed != new_speed)
        {
            CancelInvoke(nameof(Spawn));
            var diff = Time.time - lastSpawn;
            float rate;
            if (new_speed > old_speed)
            {
                rate = (spawnRate + oldSpawnRate)/2 - diff;
            }
            else {
                rate = (spawnRate + oldSpawnRate)/2 - diff;
            }
            Invoke(nameof(Spawn), rate);
        }
        
    }

    private void Spawn()
    {
        _Spawn();
        Invoke(nameof(Spawn), spawnRate + Time.deltaTime);
    }

    private void _Spawn()
    {
        lastSpawn = Time.time;
        GameObject trunks = Instantiate(prefab, transform.position, Quaternion.identity);
        trunks.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}