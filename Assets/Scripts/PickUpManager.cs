using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public static List<GameObject> pickUps = new List<GameObject>();
    // Start is called before the first frame update
    private void OnEnable()
    {
        pickUps.Add(gameObject);
    }

    public static void DeactivatePickUp(GameObject otherGameObject)
    {
        if (pickUps.Contains(otherGameObject))
        {
            pickUps.Remove(otherGameObject);
            otherGameObject.SetActive(false);
        }
    }
}
