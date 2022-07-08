using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    Bounds m_bounds;

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (null == renderer)
        {
            Debug.LogError("RepeatBackground with no renderer");
            Destroy(this);
        }
        m_bounds = renderer.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        {   // TODO add new tiles to fill to the right
#if true
            Transform lastChild = transform.GetChild(transform.childCount - 1);
            Renderer render = lastChild.GetComponent<Renderer>();
            Vector3 corner = Camera.main.WorldToViewportPoint(render.bounds.max);
            float rightEdge = corner.x;
            while (rightEdge < 1.0f)
            {
                GameObject newObj = Instantiate(lastChild.gameObject);
                newObj.transform.parent = transform;
                Vector3 pos = lastChild.position;
                pos.x += m_bounds.size.x;
                newObj.transform.position = pos;
                render = newObj.GetComponent<Renderer>();
                corner = Camera.main.WorldToViewportPoint(render.bounds.max);
                rightEdge = corner.x;
            }
#endif
        }

        {   // TODO remove tiles on the left
#if true
            int index = 0;
            Transform firstChild = transform.GetChild(0);
            Renderer render = firstChild.GetComponent<Renderer>();
            Vector3 corner = Camera.main.WorldToViewportPoint(render.bounds.max);
            float rightEdge = corner.x;
            while (rightEdge < 0.0f)
            {
                Destroy(firstChild.gameObject);
                ++index;
                firstChild = transform.GetChild(index);
                render = firstChild.GetComponent<Renderer>();
                corner = Camera.main.WorldToViewportPoint(render.bounds.max);
                rightEdge = corner.x;
            }
#endif
        }
    }
}
