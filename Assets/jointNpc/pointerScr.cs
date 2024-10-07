using UnityEngine;

public class pointerScr : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    void Start() {
        Debug.Log(1);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    Debug.Log(2);
    }
    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
       
        Vector3 rotation = mousePos - transform.position;

        float rotZ =  Mathf.Atan2(rotation.y,rotation.x) *Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }
}
