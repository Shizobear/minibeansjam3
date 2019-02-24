using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaSpawner : GameBehaviour
{
    private GameObject piranha_pool_gameobject;
    private List<Piranha> spawned_piranhas;

    [SerializeField]
    private GameObject piranha_prefab;

    private float time_last_piranha_spawned = 0;

    private Vector3[] spawning_positions;

    // Use this for initialization
    void Start()
    {
        piranha_pool_gameobject = new GameObject("Piranha Pool");
        spawned_piranhas = new List<Piranha>();
        Generate_spawning_positions();
    }

    private void Generate_spawning_positions()
    {
        int spawn_offset_in_tiles = 20;
        spawning_positions = new Vector3[4];
        Vector3 camera_mid_point = Camera.main.transform.position;

        spawning_positions[0] = new Vector3(camera_mid_point.x + spawn_offset_in_tiles, 0, 0);
        spawning_positions[1] = new Vector3(camera_mid_point.x, camera_mid_point.y + spawn_offset_in_tiles, 0);
        spawning_positions[2] = new Vector3(camera_mid_point.x, camera_mid_point.y - spawn_offset_in_tiles, 0);
        spawning_positions[3] = new Vector3(camera_mid_point.x - spawn_offset_in_tiles, 0, 0);

    }

    public override void UpdateBehaviour()
    {
        if (time_last_piranha_spawned + GameMetaData.Piranha_cooldown < Time.time)
            Spawn_Piranha();

    }

    private int random_spawn_index;
    private void Spawn_Piranha()
    {
        if (spawned_piranhas.Count >= GameMetaData.Max_amount_of_piranhas)
            return;

        random_spawn_index = Random.Range(0, spawning_positions.Length);
        GameObject pa_ob = Instantiate(piranha_prefab, spawning_positions[random_spawn_index], Quaternion.identity);
        spawned_piranhas.Add(pa_ob.GetComponent<Piranha>());

        time_last_piranha_spawned = Time.time;

    }
}
