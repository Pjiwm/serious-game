using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonClickSound : MonoBehaviour,  IPointerEnterHandler
{
    [SerializeField] private AudioSource audioSource;
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.Play();
    }
}
