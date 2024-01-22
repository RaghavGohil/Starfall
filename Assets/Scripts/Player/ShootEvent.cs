using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

internal sealed class ShootEvent : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

    public Shoot[] shootInstances;

    public void SetShootInstances(Shoot[] instances) 
    {
        shootInstances = instances;
    }

    public void OnPointerDown(PointerEventData data) 
    {
        foreach(Shoot instance in shootInstances) 
        {
            if (instance != null)
                instance.FireDown();
        }
    }
    public void OnPointerUp(PointerEventData data)
    {
        foreach (Shoot instance in shootInstances)
        {
            if (instance != null)
                instance.FireUp();
        }
    }
}
