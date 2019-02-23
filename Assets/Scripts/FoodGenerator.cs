using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class FoodGenerator : GameBehaviour
{
    private Tilemap tilemap;
    private int amount_of_tiles_x;
    private int amount_of_tiles_y;
    [SerializeField]
    private float spawn_interval = 3f;
    private float time_last_spawned = 0;

    [SerializeField]
    private GameObject meatFood;
    [SerializeField]
    private GameObject plantFood;
    private GridLayout grid_layout;

    private List<GameObject> spawned_food;
    [SerializeField]
    private const int max_amount_of_food = 8;



    // Use this for initialization
    private void Start()
    {
        tilemap = this.gameObject.GetComponent<Tilemap>();
        amount_of_tiles_x = (GameMetaData.ResolutionX / GameMetaData.PixelsPerUnit) / 2;
        amount_of_tiles_y = (GameMetaData.ResolutionY / GameMetaData.PixelsPerUnit) / 2;
        grid_layout = this.transform.GetComponentInParent<GridLayout>();
        spawned_food = new List<GameObject>();
        tile_size_vector = new Vector2(1, 1);

        if (grid_layout == null)
            this.enabled = false;
    }

    public override void UpdateBehaviour()
    {
        if (Time.time > time_last_spawned + spawn_interval)
        {
            SpawnFood();
            time_last_spawned = Time.time;
        }

        Check_if_food_was_eaten();

    }


    private Vector3Int random_tile_coordinates;
    private Vector3 random_tile_Worldcoordinates;
    [SerializeField]
    private int spawn_offset = 1;
    private Vector2 tile_size_vector;
    private Vector3Int last_spawned_coordinate;
    private float minimum_distance_between_food = 2;
    private void SpawnFood()
    {
        if (max_amount_of_food <= spawned_food.Count)
            return;

        do
        {
            random_tile_coordinates.x = Random.Range(-amount_of_tiles_x + spawn_offset, amount_of_tiles_x - spawn_offset);
            random_tile_coordinates.y = Random.Range(-amount_of_tiles_y + spawn_offset, amount_of_tiles_y - spawn_offset);
            random_tile_coordinates.z = 0;
        }
        while (Too_close_to_other_food(random_tile_coordinates) || Vector3Int.Distance(random_tile_coordinates, last_spawned_coordinate) < minimum_distance_between_food * 1.5f);






        random_tile_Worldcoordinates = grid_layout.CellToWorld(random_tile_coordinates);

        if (Physics2D.OverlapBox(random_tile_Worldcoordinates, tile_size_vector, 0))
        {
            Debug.Log("Something is in the way");
            return;
        }



        if (Random.Range(0, 100) >= 50)
            spawned_food.Add(Instantiate(meatFood, random_tile_Worldcoordinates, Quaternion.identity));
        else
            spawned_food.Add(Instantiate(plantFood, random_tile_Worldcoordinates, Quaternion.identity));

        last_spawned_coordinate = random_tile_coordinates;
    }

    private void Check_if_food_was_eaten()
    {
        foreach (var item in spawned_food.ToArray())
        {
            if (item == null || !item.gameObject.activeInHierarchy)
                spawned_food.Remove(item);
        }
    }

    private bool Too_close_to_other_food(Vector3Int _position)
    {


        foreach (var item in spawned_food)
        {
            if (Vector3.Distance(_position, item.transform.position) < minimum_distance_between_food)
                return true;
        }

        return false;


    }



}
