using UnityEngine;

public class Dude_gun_handler : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos; 
    public GameObject pointer;
    public GameObject physicalContent;
    public GameObject gun;
    public float yOffSet;
    void Start() {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void Update()
    {
        UpdateLocation_This();
        AlingPointer();
        AlingPhysicalContent();
        AlingGun();
    }
    void UpdateLocation_This()
{
    Vector3 newPosition = physicalContent.transform.position;
    newPosition.y += yOffSet;
    transform.position = newPosition;
}
    void AlingPointer(){
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);    
        Vector3 rotation = mousePos - transform.position;
        float rotZ =  Mathf.Atan2(rotation.y,rotation.x) *Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotZ);        
    }
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
    public void PickUpGun(GameObject pickedGun){
        if((pickedGun == null) || (pointer == null)){
            Debug.LogError("null parameter on PickUpGun | pickedGun: "+(pickedGun !=null)+" pointer: "+(pointer!=null) );
            return;
        }        
        gun = pickedGun;
    }

}
