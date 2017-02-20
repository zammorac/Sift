using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioSource m_ExploreTrack;
    public AudioSource m_ChaseTrack;

    // Use this for initialization
    void Start () {
		
	}

    public void PlayExplore()
    {
        if (!m_ExploreTrack.isPlaying)
            m_ExploreTrack.Play();
    }

    public void StopExplore()
    {
        if (m_ExploreTrack.isPlaying)
            m_ExploreTrack.Stop();
    }

    public void PlayChase()
    {
        if (!m_ChaseTrack.isPlaying)
            m_ChaseTrack.Play();
    }

    public void StopChase()
    {
        if (m_ChaseTrack.isPlaying)
            m_ChaseTrack.Stop();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
