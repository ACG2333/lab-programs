using UnityEngine;

public class PointToLight : MonoBehaviour {

    public GameObject PointLight;

    private int layerIndex;
    private bool isPointing = false;

	void Start ()
    {
        layerIndex = LayerMask.GetMask("Pointable");

        /*打开界面时直接开灯*/
        isPointing = true;
        PointLight.SetActive(true);
    }

    void Update () {
        
        /*开关灯光*/
        /*点击鼠标右键开灯
        if(Input.GetMouseButtonDown(1))
        {
            if (isPointing)
            {
                isPointing = false;
                PointLight.SetActive(false);
            }
            else
            {
                isPointing = true;
                PointLight.SetActive(true);
            }
        }*/

        /*灯光跟随鼠标*/
        if (isPointing)
        {
            /*射线检测*/
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100, layerIndex))
            {
                transform.position = Vector3.Lerp(transform.position, hitInfo.point, 18 * Time.deltaTime);
            }
        }
	}
}