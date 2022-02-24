using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBird : Bird
{
    // Update is called once per frame
    protected override void Update()
    {
        m_flap = Input.GetMouseButtonDown(0);
        m_glide = m_flap || Input.GetMouseButton(0);
        base.Update();
    }
}
