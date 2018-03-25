using UnityEngine;

public class AssistLine : MonoBehaviour {

    private LineRenderer line;
    public GameObject point;

	void Start () {
        line = GetComponent<LineRenderer>();
	}
	
	void Update () {
        
        /*设置射线起点*/
        line.SetPosition(0, transform.position);

        /*发射射线*/
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, 100))
        {
            line.SetPosition(1, hitInfo.point);
            point.transform.position = hitInfo.point;
        }
        else
        {
            line.SetPosition(1, transform.position + transform.forward * 100);
        }
	}
}
