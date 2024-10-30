using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CheckIfMouseOverMono : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
        public bool m_isMouseOver = false;
        public UnityEvent m_onMouseEnter;
        public UnityEvent m_onMouseExit;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            m_isMouseOver = true; 
            m_onMouseEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_isMouseOver = false;
            m_onMouseExit.Invoke();
        }

        public bool IsMouseOver()
        {
            return m_isMouseOver;
        }
    }