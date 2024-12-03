using UnityEngine;

public class Dude_gun_handler : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos; 
    public GameObject pointer;
    public GameObject physicalContent;
    public GameObject gun;
    public GameObject rightHand;
    public GameObject rightShoulder;
    public GameObject rightArm;
    public GameObject elbow; // ********* TEMP: REMOVE AFTER TESTING
    public GameObject shoulderPivot; // ********* TEMP: REMOVE AFTER TESTING
    public HingeJoint2D rigthHandJoint;
    public HingeJoint2D leftHandJoint;
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
    void Holding(){
        if (gun == null ){ 
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
        rightArm.transform.position = gunInfo.holdPoints[0].transform.position;
                
        if(rightShoulder != null){
        }
        if(rightArm != null){ 
        }
    }    
    
    
    /*
    // get where elbow should be 
    public Vector3 GetElbowPoint(GameObject shoulder, GameObject arm, GameObject shoulderPivot, GameObject gun)
    {
        Gun_Info gunInfo = gun.GetComponent<Gun_Info>();
        if (gunInfo == null || gunInfo.holdPoints == null)
        {
            Debug.LogError("Gun_Info component or holdPoint is missing on the gun.");
            return shoulderPivot.transform.position;
        }

        Vector3 shoulderPos = shoulderPivot.transform.position;
        Vector3 holdPointPos = gunInfo.holdPoints[0].transform.position;

        // Calculate distance between shoulder and holdPoint
        float d = Vector3.Distance(shoulderPos, holdPointPos);

        // Get the length of the shoulder (distance to forearm or default length)
        SpriteRenderer shoulderRenderer = shoulder.GetComponent<SpriteRenderer>();
        float shoulderLength = shoulderRenderer.sprite.bounds.size.y;
        SpriteRenderer armRenderer = arm.GetComponent<SpriteRenderer>();
        float armLength = armRenderer.sprite.bounds.size.y;

        // a = [(shoulderLength)^2 - (armRenderer)^2 + d^2] /2d
        float A = ((shoulderLength * shoulderLength) - (armLength * armLength) + (d * d)) / (2 * d);
        // Calculate the radius r
        float rSquared = (shoulderLength * shoulderLength) - (A* A);
        if (rSquared < 0) rSquared = -rSquared;
        float r = Mathf.Sqrt(rSquared) + 5f;

    

        // Calculate the midpoint (direction of d/2)
        Vector3 direction = (holdPointPos - shoulderPos).normalized;
        Vector3 midPoint = shoulderPos + direction * A;

        // Calculate the perpendicular direction
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized * r;

        // Final elbow position (M = midPoint + perpendicular)
        Vector3 elbowPos = midPoint + perpendicular;           
        Debug.Log(shoulderLength);
        return elbowPos; 

    }
    public void AlingArms(GameObject shoulderPivot, GameObject shoulder, GameObject elbow, GameObject arm, GameObject gun)
{
    // Get the elbow position based on the calculated point
    elbow.transform.position = GetElbowPoint(shoulder,arm, shoulderPivot, gun);

    // Ensure the gun has the Gun_Info component and retrieve holdPoint
    Gun_Info gunInfo = gun.GetComponent<Gun_Info>();
    if (gunInfo == null || gunInfo.holdPoints == null || gunInfo.holdPoints.Length == 0)
    {
        Debug.LogError("Gun_Info or holdPoints missing on the gun.");
        return;
    }

    Vector3 holdPointPos = gunInfo.holdPoints[0].transform.position;

    // Calculate shoulderPlace: midpoint between shoulderPivot and elbow
    Vector3 shoulderPlace = (shoulderPivot.transform.position + elbow.transform.position) / 2;

    // Calculate armPlace: midpoint between holdPointPos and elbow
    Vector3 armPlace = (holdPointPos + elbow.transform.position) / 2;

    // Place the shoulder at shoulderPlace
    shoulder.transform.position = shoulderPlace;

    // Place the arm at armPlace
    arm.transform.position = armPlace;

    
    // Align the shoulder to point from shoulderPivot to elbow along the Z-axis
    Vector3 shoulderDirection = elbow.transform.position - shoulderPivot.transform.position;
    float shoulderAngle = Mathf.Atan2(shoulderDirection.y, shoulderDirection.x) * Mathf.Rad2Deg;
    shoulder.transform.rotation = Quaternion.Euler(0, 0, shoulderAngle+90);

    // Align the arm to point from elbow to holdPointPos along the Z-axis
    Vector3 armDirection = holdPointPos - elbow.transform.position;
    float armAngle = Mathf.Atan2(armDirection.y, armDirection.x) * Mathf.Rad2Deg;
    arm.transform.rotation = Quaternion.Euler(0, 0, armAngle+90);
    
    Debug.Log("Arms aligned successfully with Z-axis rotations.");
    
}
    */
}