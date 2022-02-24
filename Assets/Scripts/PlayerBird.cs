using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBird : Bird
{
    public float m_forwardSpeed = 1.0f;

    // Update is called once per frame
    protected override void Update()
    {
        m_flap = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);
        m_glide = m_flap || Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);
        Vector3 pos = transform.position;
        pos.x += m_forwardSpeed * Time.deltaTime;
        transform.position = pos;
        base.Update();
    }
}
