using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FetchUI2DUnderRealMousePositionMono : MonoBehaviour
{

    public List<RaycastResult> results = new List<RaycastResult>();
    public List<Image> m_curentImage = new List<Image>();
    public List<Image> m_previous = new List<Image>();
    public UnityEvent<Image> m_onImageEnter;
    public UnityEvent<Image> m_onImageExit;
    public Image m_lastEnter;
    public Image m_lastExit;

    public Canvas canvas; // Assign the Canvas containing your UI elements

    public bool m_lastHasInterface;


    public List<Image> m_enter = new List<Image>();
    public List<Image> m_exit = new List<Image>();

    void Update()
    {
            List<RaycastResult> results = RaycastUI();
            m_previous = m_curentImage.ToList();
            m_curentImage.Clear();
            
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<Image>() != null) {

                    m_curentImage.Add(result.gameObject.GetComponent<Image>());
                }  
            }
            List<Image> m_enter = m_curentImage.Except(m_previous).ToList();
            List<Image> m_exit = m_previous.Except(m_curentImage).ToList();
            foreach (Image image in m_enter)
            {
                if (HasIPointerEnterHandler(image.gameObject, out var list)) { 
                foreach (IPointerEnterHandler handler in list)
                        handler.OnPointerEnter(null);
                    m_lastHasInterface = true;
                }
                m_onImageEnter.Invoke(image);
                if(image!=null)
                  m_lastEnter=(image);
            }
            foreach (Image image in m_exit)
            {
                if (HasIPointerExitHandler(image.gameObject, out var list)) { 
                    foreach (IPointerExitHandler handler in list)
                            handler.OnPointerExit(null);

                    m_lastHasInterface = true;
                }

                    m_onImageExit.Invoke(image);

                    if (image != null)
                        m_lastExit = image;
            }
    }

    private bool HasIPointerExitHandler(GameObject gamo, out List<IPointerExitHandler> list)
    {
        list = new List<IPointerExitHandler>();
        if (gamo == null)
        {
            return false;
        }
        
        MonoBehaviour[] components = gamo.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour component in components)
        {
            if (component != null && component is IPointerExitHandler)
            {
                list.Add((IPointerExitHandler)component);
            }
        }
        return list.Count>0;
    }

    private bool HasIPointerEnterHandler(GameObject gamo, out List<IPointerEnterHandler> list)
    {
        list = new List<IPointerEnterHandler>();
        
        if (gamo == null)
        {
            return false;
        }
        
        MonoBehaviour[] components = gamo.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour component in components)
        {
            
            if (component!=null && component is IPointerEnterHandler)
            {
                list.Add((IPointerEnterHandler)component);
            }
        }
        return list.Count>0;
    }

    List<RaycastResult> RaycastUI()
    {
        // Create a pointer event data object
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        // Create a list to store the raycast results
        List<RaycastResult> results = new List<RaycastResult>();

        // Perform the raycast using the GraphicRaycaster
        GraphicRaycaster graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        graphicRaycaster.Raycast(pointerEventData, results);

        return results;
    }
}




