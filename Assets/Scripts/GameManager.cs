using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject m_pipe;
    public float m_horizSpacing = 6.0f;
    public float m_vertSpacing = 4.0f;
    public float m_maxY = 4.0f;
    public GameObject m_pauseMenu;

    Vector3 m_lastPos;
    Vector3 m_lastCamPos;
    PlayerBird m_player;
    bool m_isPaused = false;

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

        // grab the player for future reference
        m_player = FindObjectOfType<PlayerBird>();

        SetPause(false);
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

        if (null == m_player)
        {   // Player Died
            StartCoroutine(GameOver());
        }

        // keys
        if (Input.GetKeyDown(KeyCode.Escape))
        {   // this doubles as the option key in the android navigation bar
            SetPause(!m_isPaused);
        }
    }

    IEnumerator GameOver()
    {
        // wait 3 seconds
        yield return new WaitForSecondsRealtime(3.0f);
        // and reload the scene
        SceneManager.LoadScene(0);
    }

    public void SetPause(bool setPause)
    {
        if (setPause)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        m_pauseMenu.SetActive(setPause);
        m_isPaused = setPause;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
