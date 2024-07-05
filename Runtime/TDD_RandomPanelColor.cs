using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TDD_RandomPanelColor : MonoBehaviour
{

    public UnityEvent<Color> ColorChangedEvent;
    public Image[] m_panels;
    public float m_alpha=0.1f;
    
    public void ChangeForRandomColor()
    {
        Color randomColor = Random.ColorHSV();
        randomColor.a = m_alpha;
        foreach (var panel in m_panels)
        {
            if(panel!=null)
            panel.color = randomColor;
        }
        ColorChangedEvent.Invoke(randomColor);
    }
}
