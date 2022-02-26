using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject m_pipe;
    public float m_horizSpacing = 6.0f;
    public float m_vertSpacing = 4.0f;
    public float m_maxY = 4.0f;

    Vector3 m_lastPos;
    Vector3 m_lastCamPos;

    // Start is called before the first frame update
    void Start()
    {
        // Create the first pipe
        GameObject obj = Instantiate(m_pipe);
        Vector3 pos = obj.transform.position;
        // get the left-boundary of this object
        Bounds bound = new Bounds(pos, Vector3.zero);
        Renderer[] renders = obj.transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer render in renders)
        {
            bound.Encapsulate(render.bounds);
        }
        Vector3 leftEdge = bound.min - transform.position;
        // place this object just off the right edge of the screen
        Vector3 right = new Vector3(1.0f, 0.5f, 0.0f);
        right = Camera.main.ViewportToWorldPoint(right);
        pos.x = right.x - leftEdge.x;
        obj.transform.position = pos;
        m_lastPos = pos;
        m_lastCamPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = Camera.main.transform.position;
        while (camPos.x - m_lastCamPos.x >= m_horizSpacing)
        {
            GameObject obj = Instantiate(m_pipe);
            Vector3 pos = obj.transform.position;
            m_lastPos.x += m_horizSpacing;
            m_lastPos.y += Random.Range(-m_vertSpacing, m_vertSpacing);
            m_lastPos.y = Mathf.Clamp(m_lastPos.y, -m_maxY, m_maxY);
            obj.transform.position = m_lastPos;
            m_lastCamPos.x += m_horizSpacing;
        }
    }
}
