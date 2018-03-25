using UnityEngine;

public class FollowSlowly : MonoBehaviour {

    public Transform obj;
    public float t;

	void Update () {
        transform.position = Vector3.Lerp(transform.position, obj.position, t * Time.deltaTime);
	}
}
