using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    Animator m_anim;
    protected bool m_flap = false;
    protected bool m_glide = false;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    protected virtual void Update()
    {
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
