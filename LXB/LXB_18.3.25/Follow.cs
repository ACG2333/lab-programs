using UnityEngine;

public class Follow : MonoBehaviour
{

    public GameObject item;

    void Update()
    {
        transform.position = item.transform.position;
    }
}
