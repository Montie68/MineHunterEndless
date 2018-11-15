using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour {
    [HideInInspector]
    public float m_Time;
    [HideInInspector]
    public bool m_IsPlaying;

    public List<TMP_Text> m_TimerText;
    
    private float m_minutes;
    private float m_seconds;

	// Use this for initialization
	void Awake () {
        m_Time = 0;
        m_IsPlaying = false;
	}

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate () {

            if (!m_IsPlaying) return;
            m_Time += Time.fixedDeltaTime;

            m_minutes = Mathf.Floor(m_Time / 60);
            m_seconds = m_Time % 60;
            if (m_seconds > 59) m_seconds = 0;

    }


        public void startTimer()
        {
            m_IsPlaying = true;
            StartCoroutine(updateCoroutine());
        }


        private IEnumerator updateCoroutine()
        {
            while (m_IsPlaying)
            {
            foreach (TMP_Text txt in m_TimerText)
                { txt.text = string.Format("{0:00}:{1:00}", m_minutes, m_seconds); }

                yield return new WaitForSeconds(0.2f);
            }
        }


    public void setIsPlaying(bool isPlaying = true)
    {
        m_IsPlaying = isPlaying;
        if (!isPlaying) StopCoroutine(updateCoroutine());
    }


    public bool getIsPlaying()
    {
        return m_IsPlaying;
    }

}
