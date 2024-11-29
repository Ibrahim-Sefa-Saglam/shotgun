using UnityEngine;

public class Dude_gun_handler : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos; 
    public GameObject pointer;
    public GameObject physicalContent;
    public GameObject gun;
    public GameObject rightHand;
    public float yOffSet;
    void Start() {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void Update()
    {
        UpdateLocationThis();
        AlingPointer();
        AlingPhysicalContent();
        if(gun!=null){
        AlingGun();
        Holding();
        }
    }
    // update the locaiton of the PointerAxis(objcet this script assinged to) to stay in desired location
    void UpdateLocationThis()
{
    Vector3 newPosition = physicalContent.transform.position;
    newPosition.y += yOffSet;
    transform.position = newPosition;
}
    // aling the pointer with cursor by rotating the pointerAxis(objcet this script assinged to)
    void AlingPointer(){
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);    
        Vector3 rotation = mousePos - transform.position;
        float rotZ =  Mathf.Atan2(rotation.y,rotation.x) *Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotZ);        
    }
    // aling the direction of the players/characters body
    void AlingPhysicalContent(){
        if(physicalContent == null) return;
        Vector3 scale = physicalContent.transform.localScale;        
        if (mousePos.x < transform.position.x)        {
            scale.x = -Mathf.Abs(scale.x); // Ensure facing left
            physicalContent.transform.localScale = scale;                         
        }
        else
        {
            scale.x = Mathf.Abs(scale.x); // Ensure facing right
            physicalContent.transform.localScale = scale;
        }
    }
    // aling the guns direction with pointer/cursor
    void AlingGun(){
        if(gun==null) return;        
        gun.transform.position = pointer.transform.position;// carry the gun to pointer    
        gun.layer = LayerMask.NameToLayer("eq_gun");// change the layer to make it not collide with other stuff
        // set sprite order so it can be seen
        SpriteRenderer gunSpriteRenderer = gun.GetComponent<SpriteRenderer>();
        gunSpriteRenderer.sortingOrder = 10;
        // rotate the gun same as pointerAxis so direction is correct
        gun.transform.rotation = transform.rotation;
        if (gun.transform.eulerAngles.z > 90 && gun.transform.eulerAngles.z < 270){
            Vector3 scale = gun.transform.localScale;
            scale.y = -1;
            gun.transform.localScale = scale;
        }
        else{
            Vector3 scale = gun.transform.localScale;
            scale.y = 1;
            gun.transform.localScale = scale;
        }
    }
    // pick up the gun and assing it to "gun", called by another script and is here for easy data access
    public void PickUpGun(GameObject pickedGun){
        if((pickedGun == null) || (pointer == null)){
            Debug.LogError("null parameter on PickUpGun | pickedGun: "+(pickedGun !=null)+" pointer: "+(pointer!=null) );
            return;
        }        
        gun = pickedGun;
    }
    // put the players/characters hands(rightHand/leftHand) on the guns holdPoints
    void Holding()
    {
        if (gun == null || rightHand == null){ 
            Debug.Log(1+"   gun: "+(gun == null)+"  rightHand: "+(rightHand == null));
            return;
        }

        Gun_Info gunInfo = gun.GetComponent<Gun_Info>();// Ensure the gun has the holdPoints attribute
        if (gunInfo == null || gunInfo.holdPoints == null || gunInfo.holdPoints.Length == 0)
        {
            Debug.Log(
            2+"gunInfo: "+(gunInfo == null)+
            "\nholdPoints: "+(gunInfo.holdPoints == null)+
            "\nLength: "+(gunInfo.holdPoints.Length == 0));
            return;
        }

        // Set the rightHand position to the first HoldingPoint
        rightHand.transform.position = gunInfo.holdPoints[0].transform.position;
    }    
}