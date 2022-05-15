using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioList;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        int index = Random.Range(0, audioList.Length);
        audioSource.clip = audioList[index];
        audioSource.loop = true;
        audioSource.Play();
    }
}
