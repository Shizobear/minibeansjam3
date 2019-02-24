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



    // Use this for initialization
    void Start()
    {
        amount_of_tiles_x = (GameMetaData.ResolutionX / GameMetaData.PixelsPerUnit) / 2;
        amount_of_tiles_y = (GameMetaData.ResolutionY / GameMetaData.PixelsPerUnit) / 2;
        grid_layout = this.transform.GetComponentInParent<GridLayout>();
        m_transform = this.transform;
        tile_size_vector = new Vector2(1, 1);

        if (grid_layout == null)
            this.enabled = false;

        GenerateHookPool();

    }

    private void GenerateHookPool()
    {
        hook_pool = new List<Hook>();
        GameObject hook_object_cache;
        for (int i = -amount_of_tiles_x + 1; i < amount_of_tiles_x - 1; i++)
        {
            hook_object_cache = Instantiate(hook_gameObject, new Vector3(i, m_transform.position.y, 0), Quaternion.identity);
            hook_pool.Add(hook_object_cache.GetComponent<Hook>());
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
