using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Hook : GameBehaviour
{

    private Transform m_transform;
    private Transform rope_transform;
    private bool caught_something = false;
    private float speed = 6f;    // Use this for initialization
    void Start()
    {
        m_transform = this.transform;
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
    }

    private void PullInCatch()
    {
        m_transform.Translate(Vector3.up * (Time.deltaTime * speed));
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


}

