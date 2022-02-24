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
        Transform lastChild = transform.GetChild(transform.childCount - 1);
        List<Transform> offScreen = new List<Transform>();
        // go through deleting any tiles that have scrolled off the left
        float xMax = float.MinValue;
        for (int childIndex = 0; childIndex < transform.childCount; ++childIndex)
        {
            Transform child = transform.GetChild(childIndex);
            Renderer render = child.GetComponent<Renderer>();
            if (null != render)
            {
                Vector3 botRight = Camera.main.WorldToViewportPoint(render.bounds.max);
                if (botRight.x < 0.0f)
                {
                    if (child != lastChild)
                    {
                        Vector3 pos = lastChild.position;
                        pos.x += m_bounds.size.x;
                        child.position = pos;
                        botRight = Camera.main.WorldToViewportPoint(render.bounds.max);
                        lastChild = child;
                        offScreen.Add(child);
                    }
                }
                if (botRight.x > xMax)
                {
                    xMax = botRight.x;
                }
            }
        }

        foreach (Transform child in offScreen)
        {
            child.SetAsLastSibling();
        }

        // add new tiles to fill to the right
        while (xMax < 1.0f)
        {
            GameObject newObj = Instantiate(transform.GetChild(0).gameObject);
            newObj.transform.parent = transform;
            Vector3 pos = transform.GetChild(0).position;
            pos.x = lastChild.position.x + m_bounds.size.x;
            newObj.transform.position = pos;
            pos.x += m_bounds.size.x;
            xMax = Camera.main.WorldToViewportPoint(pos).x;
            lastChild = newObj.transform;
        }
    }
}
