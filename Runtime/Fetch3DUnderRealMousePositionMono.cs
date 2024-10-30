using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Fetch3DUnderRealMousePositionMono : MonoBehaviour
{
    public InputActionReference m_mouseInputPosition;
    public List<MonoBehaviour> m_curent = new List<MonoBehaviour>();
    public List<MonoBehaviour> m_previous = new List<MonoBehaviour>();
    public UnityEvent<MonoBehaviour> m_onImageEnter;
    public UnityEvent<MonoBehaviour> m_onImageExit;
    public MonoBehaviour m_lastEnter;
    public MonoBehaviour m_lastExit;


    public bool m_lastHasInterface;


    public List<MonoBehaviour> m_enter = new List<MonoBehaviour>();
    public List<MonoBehaviour> m_exit = new List<MonoBehaviour>();

    private void Awake()
    {
        m_mouseInputPosition.action.Enable();
    }
    void Update()
    {
        List<GameObject> results = RaycastUI();
        m_previous = m_curent.ToList();
        m_curent.Clear();

        foreach (GameObject result in results)
        {
            if (result.gameObject.GetComponents<MonoBehaviour>() != null)
            {

                m_curent.AddRange(result.gameObject.GetComponents<MonoBehaviour>());
            }
        }
        List<MonoBehaviour> m_enter = m_curent.Except(m_previous).ToList();
        List<MonoBehaviour> m_exit = m_previous.Except(m_curent).ToList();
        foreach (MonoBehaviour targetFound in m_enter)
        {
            if (HasIPointerEnterHandler(targetFound.gameObject, out var list))
            {
                foreach (IPointerEnterHandler handler in list)
                    handler.OnPointerEnter(null);
                m_lastHasInterface = true;
            }
            m_onImageEnter.Invoke(targetFound);
            if (targetFound != null)
                m_lastEnter = (targetFound);
        }
        foreach (MonoBehaviour targetFound in m_exit)
        {
            if (targetFound != null) { 
                if (HasIPointerExitHandler(targetFound.gameObject, out var list))
                {
                    foreach (IPointerExitHandler handler in list)
                        handler.OnPointerExit(null);

                    m_lastHasInterface = true;
                }
            }

            m_onImageExit.Invoke(targetFound);

            if (targetFound != null)
                m_lastExit = targetFound;
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
        return list.Count > 0;
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

            if (component != null && component is IPointerEnterHandler)
            {
                list.Add((IPointerEnterHandler)component);
            }
        }
        return list.Count > 0;
    }

    public List<GameObject> m_hits = new List<GameObject>();
    List<GameObject> RaycastUI()
    {

            if(m_mouseInputPosition == null)
            {
                return new List<GameObject>();
            }
            List< GameObject> results = new List<GameObject>();
        
            Vector2 mousePosition = m_mouseInputPosition.action.ReadValue<Vector2>();

            // Convert the mouse position to a ray
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            // Perform the raycast
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray);

            m_hits = hits.Select(x => x.collider.gameObject).ToList();
        results= m_hits;
        return results;
    }
}
