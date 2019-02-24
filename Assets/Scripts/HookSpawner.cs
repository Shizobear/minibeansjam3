using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSpawner : GameBehaviour
{
    private Transform m_transform;

    [SerializeField]
    private GameObject hook_gameObject;
    private int amount_of_tiles_x;
    private int amount_of_tiles_y;
    private GridLayout grid_layout;
    private Vector2 tile_size_vector;

    private List<Hook> hook_pool;
    private List<Hook> thrown_hooks;
    private float time_last_hook_spawned = 0;
    private Vector3Int random_tile_coordinates;
    private int spawn_offset = 1;
    private Vector3 random_tile_Worldcoordinates;
    private Transform hook_pool_parent;





    // Use this for initialization
    void Start()
    {
        amount_of_tiles_x = (GameMetaData.ResolutionX / GameMetaData.PixelsPerUnit) / 2;
        amount_of_tiles_y = (GameMetaData.ResolutionY / GameMetaData.PixelsPerUnit) / 2;
        grid_layout = this.transform.GetComponentInParent<GridLayout>();
        m_transform = this.transform;
        tile_size_vector = new Vector2(1, 1);
        time_last_hook_spawned = Time.time + GameMetaData.Hook_cooldown;
        hook_pool_parent = new GameObject("Hook Pool").transform;

        if (grid_layout == null)
            this.enabled = false;

        GenerateHookPool();
        thrown_hooks = new List<Hook>();

    }

    private void GenerateHookPool()
    {
        hook_pool = new List<Hook>();
        GameObject hook_object_cache;
        for (int i = -amount_of_tiles_x + 1; i < amount_of_tiles_x - 1; i++)
        {
            hook_object_cache = Instantiate(hook_gameObject, new Vector3(i, m_transform.position.y, 0), Quaternion.identity);
            hook_pool.Add(hook_object_cache.GetComponent<Hook>());
            hook_object_cache.transform.SetParent(hook_pool_parent);
        }

    }

    public override void UpdateBehaviour()
    {
        if (time_last_hook_spawned + GameMetaData.Hook_cooldown < Time.time)
            Spawn_Hook_At_Random_Position();


        foreach (var item in thrown_hooks.ToArray())
        {
            if (item.Is_In_Use() == false)
                thrown_hooks.Remove(item);
        }
    }


    private List<Hook> hooks_not_in_use;
    private void Spawn_Hook_At_Random_Position()
    {
        if (GameMetaData.Max_amount_of_hooks <= thrown_hooks.Count)
            return;

        do
        {
            random_tile_coordinates.x = Random.Range(-amount_of_tiles_x + spawn_offset, amount_of_tiles_x - spawn_offset);
            random_tile_coordinates.y = Random.Range(-amount_of_tiles_y + spawn_offset, amount_of_tiles_y - spawn_offset);
            random_tile_coordinates.z = 0;
            random_tile_Worldcoordinates = grid_layout.CellToWorld(random_tile_coordinates);
        }
        while (Physics2D.OverlapBox(random_tile_Worldcoordinates, tile_size_vector, 0));

        hooks_not_in_use = new List<Hook>();

        foreach (var item in hook_pool)
        {
            if (item.Is_In_Use())
                continue;
            else
                hooks_not_in_use.Add(item);
        }

        if (hooks_not_in_use.Count == 0)
            return;

        int random_hook_index = Random.Range(0, hooks_not_in_use.Count);
        hooks_not_in_use[random_hook_index].Throw_Hook_To_Depth(random_tile_coordinates.y);
        thrown_hooks.Add(hooks_not_in_use[random_hook_index]);


        time_last_hook_spawned = Time.time;
    }
}
