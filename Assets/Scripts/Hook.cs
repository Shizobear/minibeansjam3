using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Hook : GameBehaviour
{

    private Transform m_transform;
    private Transform rope_transform;
    [SerializeField]
    private GameObject tile_placeholder;
    private bool caught_something = false;
    private Vector3 start_position;
    private bool is_in_use = false;
    private float speed = 6f;    // Use this for initialization
    void Start()
    {
        m_transform = this.transform;
        start_position = new Vector3(m_transform.position.x, m_transform.position.y, m_transform.position.z);
        endPosition = new Vector3(m_transform.position.x, -2, 0);
        tile_placeholder.SetActive(false);
        tile_placeholder.transform.SetParent(m_transform.parent);
        try
        {
            rope_transform = m_transform.GetChild(0).transform;
        }
        catch (System.Exception)
        {

            Debug.LogWarning("kein rope");
        }

        this.GetComponent<BoxCollider2D>().isTrigger = true;

    }

    public override void UpdateBehaviour()
    {
        if (caught_something == true)
            PullInCatch();

        if (is_being_thrown == true)
        {
            if (EndpositionReached() == false)
                Throw_Hook();
        }


    }

    private void PullInCatch()
    {
        m_transform.position = Vector3.MoveTowards(m_transform.position, start_position, Time.deltaTime * speed);
        if (Vector3.Distance(m_transform.position, start_position) < endPosition_reached_threshold)
        {
            is_in_use = false;
            tile_placeholder.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (caught_something == true)
            return;

        iCatchable caught = other.GetComponent<iCatchable>();

        if (caught != null)
        {
            caught.OnCatch(m_transform);
            caught_something = true;
        }

    }
    private Vector3 endPosition;
    private bool is_being_thrown = false;
    private float endPosition_reached_threshold = 0.001f;

    private bool EndpositionReached()
    {
        if (Vector3.Distance(m_transform.position, endPosition) < endPosition_reached_threshold)
        {
            is_being_thrown = false;
            return true;
        }

        else
            return false;
    }

    private void Throw_Hook()
    {
        m_transform.position = Vector3.MoveTowards(m_transform.position, endPosition, Time.deltaTime * speed);
    }

    public void Throw_Hook_To_Depth(float _depth)
    {
        endPosition = new Vector3(m_transform.position.x, _depth, 0);
        tile_placeholder.transform.position = endPosition;
        tile_placeholder.SetActive(true);
        is_being_thrown = true;
        is_in_use = true;
    }

    public bool Is_In_Use()
    {
        return is_in_use;
    }


}

