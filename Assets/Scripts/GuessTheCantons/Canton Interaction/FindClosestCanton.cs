using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestCanton : MonoBehaviour
{
    public static GameObject cantonObjects;

    public static string closestCanton = "";
    void Start()
    {
        cantonObjects = GameObject.Find("Canton Interactables");
    }

    public static string findClosestCanton(Vector3 playerPosition){
        float closestDistance = float.MaxValue;
        foreach(Transform canton in cantonObjects.transform){
            float distance = Vector3.Distance(canton.position, playerPosition);
            if(distance < closestDistance){
                closestDistance = distance;
                closestCanton = canton.name;
            }
        }
        return closestCanton;
    }

}
