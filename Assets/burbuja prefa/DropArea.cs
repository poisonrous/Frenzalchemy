using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrop : MonoBehaviour, IDropArea
{
    [SerializeField] private GameObject objectToSpawn;

    public void OnDrop(Draggable sample)
    {
        Destroy(sample.gameObject);
        Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}
