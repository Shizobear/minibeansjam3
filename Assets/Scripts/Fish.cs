using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Fish : GameBehaviour, iCollectorFish, iCatchable, iPiranhaPrey
{
    private static Fish player;
    private Transform m_transform;
    private Rigidbody2D m_rigidbody;
    private FishMovement movement;
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float weight = 0.5f;

    private bool caught = false;

    private Vector3 current_direction;
    private SoundManager sound_manager;

    public override void Awake()
    {
        base.Awake();
        player = this;
    }

    // Use this for initialization
    void Start()
    {
        m_transform = this.gameObject.transform;
        movement = new FishMovement(this);
        m_rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        sound_manager = SoundManager.GetInstance();

    }

    public float GetSpeed()
    {
        return speed;
    }

    public override void UpdateBehaviour()
    {
        movement.UpdateInput();
        UpdateWeightPenalty();
    }


    private float weightPenalty = 50f;
    public void Move(Vector3 _direction)
    {
        if (_direction != Vector3.zero)
            current_direction = _direction;

        _direction = _direction.normalized;
        _direction = _direction * (speed * Time.deltaTime);
        m_transform.Translate(_direction - (_direction * (weight / weightPenalty)));
    }

    public void IncreaseWeight(float _amount)
    {
        Debug.Log(_amount);
        weight += _amount;
    }

    public float GetWeight()
    {
        return weight;
    }

    private void UpdateWeightPenalty()
    {
        //TODO
    }

    public void OnCatch(Transform _hook)
    {
        caught = true;
        BoxCollider2D[] _hitboxes = this.GetComponents<BoxCollider2D>();
        for (int i = 0; i < _hitboxes.Length; i++)
            _hitboxes[i].enabled = false;
        m_transform.SetParent(_hook);

    }

    public bool IsCaught()
    {
        return caught;
    }

    public static Fish GetReference()
    {
        return player;
    }

    public void On_eaten_by_piranha(Piranha _piranha)
    {
        Die();
    }

    private void Die()
    {

    }

    private void OnBecameInvisible()
    {
        if (IsCaught() == true)
        {
            Debug.Log("rip in peace");
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }

    }

    public Vector3 GetCurrentDirection()
    {
        return current_direction;
    }
}
