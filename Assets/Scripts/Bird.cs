using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float m_gravity = 1.0f;
    public float m_flapBoost = 3.0f;
    public float m_glideSpeed = 2.0f;
    public float m_maxFallSpeed = 10.0f;
    public float m_maxRiseSpeed = 6.0f;
    public float m_yMax = 5.0f;

    Animator m_anim;
    protected bool m_flap = false;
    protected bool m_glide = false;

    protected float m_ySpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Vector3 pos = transform.position;
        m_ySpeed -= m_gravity * Time.deltaTime;
        if (m_flap)
        {
            m_ySpeed += m_flapBoost;
        }
        if (m_glide)
        {
            m_ySpeed = Mathf.Max(m_ySpeed, -m_glideSpeed);
        }
        else
        {
            m_ySpeed = Mathf.Max(m_ySpeed, -m_maxFallSpeed);
        }
        m_ySpeed = Mathf.Min(m_ySpeed, m_maxRiseSpeed);
        pos.y += m_ySpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, -m_yMax, m_yMax);
        transform.position = pos;

        if (null != m_anim)
        {
            m_anim.SetBool("FlapDown", m_glide);
            if (m_flap)
            {
                m_anim.SetTrigger("FlapHit");
                m_flap = false;
            }
        }
    }
}
