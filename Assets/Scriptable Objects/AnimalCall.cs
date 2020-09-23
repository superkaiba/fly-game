using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AnimalCall", order = 1)]
public class AnimalCall : ScriptableObject
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] float volume;

    public AudioClip AudioClip
    {
        get
        {
            return audioClip;
        }
    }

    public float Volume
    {
        get
        {
            return volume;
        }
    }
}
