using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Piranha : GameBehaviour, iCollectorFish, iCatchable
{

    private Transform m_transform;
    private Transform attack_target;
    private Vector3 initial_position_in_level;
    private int amount_of_tiles_x;
    private int amount_of_tiles_y;
    private GridLayout grid_layout;
    private Vector2 tile_size_vector;
    [SerializeField]
    private float speed = 0.001f;
    [SerializeField]
    private float attack_speed = 6f;
    private bool caught = false;
    [SerializeField]
    private float weight = 1;

    private PiranhaStates current_state;
    // Use this for initialization
    void Start()
    {
        m_transform = this.transform;
        initial_position_in_level = new Vector3(2, -2, 0);

        amount_of_tiles_x = (GameMetaData.ResolutionX / GameMetaData.PixelsPerUnit) / 2;
        amount_of_tiles_y = (GameMetaData.ResolutionY / GameMetaData.PixelsPerUnit) / 2;
        grid_layout = this.transform.GetComponentInParent<GridLayout>();
        tile_size_vector = new Vector2(1, 1);
        GenerateInitialPosition();
        current_state = PiranhaStates.entering;
    }

    public override void UpdateBehaviour()
    {
        switch (current_state)
        {
            case PiranhaStates.idling:
                IdleBehaviour();
                break;
            case PiranhaStates.chasing:
                ChasingBehaviour();
                break;
            case PiranhaStates.entering:
                EnteringBehaviour();
                break;
            case PiranhaStates.caught:
                CaughtBehaviour();
                break;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void OnCatch(Transform _hook)
    {
        caught = true;
        BoxCollider2D[] _hitboxes = this.GetComponents<BoxCollider2D>();
        for (int i = 0; i < _hitboxes.Length; i++)
            _hitboxes[i].enabled = false;
        m_transform.SetParent(_hook);
        current_state = PiranhaStates.caught;
    }

    public void IncreaseWeight(float _amount)
    {
        weight += _amount;
    }

    private Vector3 idle_waypoint;
    private int idle_move_distance = 1;
    private float idle_move_distance_threshold = 0.001f;
    private void IdleBehaviour()
    {
        if (idle_waypoint == null)
            idle_waypoint = new Vector3(initial_position_in_level.x - idle_move_distance, initial_position_in_level.y, initial_position_in_level.z);

        m_transform.position = Vector3.MoveTowards(m_transform.position, idle_waypoint, (speed / 2) * Time.deltaTime);

        if (Vector3.Distance(m_transform.position, idle_waypoint) < idle_move_distance_threshold)
        {
            int x = Random.Range(-3, 3);
            int y = Random.Range(-3, 3);
            idle_waypoint = new Vector3(initial_position_in_level.x + x, initial_position_in_level.y + y, initial_position_in_level.z);
        }

        Scan_for_prey();
    }

    private void ChasingBehaviour()
    {
        if (attack_target == null || attack_target.gameObject.activeInHierarchy == false)
            current_state = PiranhaStates.idling;

        m_transform.position = Vector3.MoveTowards(m_transform.position, attack_target.position, Time.deltaTime * speed);
    }

    private void EnteringBehaviour()
    {
        m_transform.position = Vector3.MoveTowards(m_transform.position, initial_position_in_level, (speed / 2) * Time.deltaTime);
        Scan_for_prey();
    }

    private void CaughtBehaviour()
    {

    }

    private void OnBecameInvisible()
    {
        if (current_state == PiranhaStates.caught)
            Destroy(this.gameObject);

    }

    [SerializeField]
    private float aggro_radius = 5f;
    private Collider2D[] prey_array_cache;


    private void Scan_for_prey()
    {
        prey_array_cache = Physics2D.OverlapCircleAll(m_transform.position, aggro_radius);
        for (int i = 0; i < prey_array_cache.Length; i++)
        {
            if (prey_array_cache[i].gameObject.GetComponent<iPiranhaPrey>() != null)
            {
                Set_Target(prey_array_cache[i].gameObject.transform);
            }
        }
    }

    private void Set_Target(Transform _target)
    {
        attack_target = _target;
        current_state = PiranhaStates.chasing;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        iPiranhaPrey prey = other.GetComponent<iPiranhaPrey>();

        if (prey != null && current_state == PiranhaStates.chasing)
            prey.On_eaten_by_piranha(this);
    }

    private Vector3Int random_tile_coordinates;
    private int spawn_offset = 1;
    private Vector3 random_tile_Worldcoordinates;

    private void GenerateInitialPosition()
    {

        do
        {
            random_tile_coordinates.x = Random.Range(-amount_of_tiles_x + spawn_offset, amount_of_tiles_x - spawn_offset);
            random_tile_coordinates.y = Random.Range(-amount_of_tiles_y + spawn_offset, amount_of_tiles_y - spawn_offset);
            random_tile_coordinates.z = 0;
            random_tile_Worldcoordinates = grid_layout.CellToWorld(random_tile_coordinates);
        }
        while (Physics2D.OverlapBox(random_tile_Worldcoordinates, tile_size_vector, 0));

        initial_position_in_level = random_tile_Worldcoordinates;
    }


}
