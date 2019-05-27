using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Clone : MonoBehaviour {

    private GameObject mainCamera;
    private GameObject lookingAt;
    private GameObject clipboard;
    private Renderer render;
    private Rigidbody cloneRb;
    public GameObject prevClone;
    public bool overButton = false;
    public bool canClone = true;
    private int cloneDist = 3;
    public GameObject clonedObject;
    public bool cloning = false;
    public bool hasCloned = false;
    public float smooth;

    int particleEmitAmmount = 150;
    [SerializeField] ParticleSystem copyParticles;
    [SerializeField] ParticleSystem failParticles;

    void Start() {
        mainCamera = GameObject.FindWithTag("MainCamera");
        PlayerManager.player_Clone = this;
        canClone = true;
    }

    void Update() {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        if (Input.GetMouseButtonDown(1)) {
            Scan(x, y);
            
        }
        if (Input.GetMouseButtonDown(0)) {
            if (!cloning) {
                Clone();
               
            } else if (cloning && canClone){
                Drop();
                copyParticles.Emit(particleEmitAmmount);
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (cloning && canClone) {
                Drop();
                copyParticles.Emit(particleEmitAmmount);
            }
        }
    }

    void FixedUpdate() {
        if (cloning) {
            CloneCarry(clonedObject);
        }
    }

    void CloneCarry(GameObject o) {
        //sends raycast out from the player to cloneDist
        Vector3 direction = (clonedObject.transform.position - mainCamera.transform.position).normalized;
        Ray cloneRay = new Ray(mainCamera.transform.position, direction * cloneDist);
        RaycastHit hit;
        Physics.Raycast(cloneRay, out hit);
        //if the raycast hits something other than the object being cloned then cloning is impossible.
        if (hit.collider.gameObject != clonedObject) {
            if (hit.collider.gameObject.tag == "IgnoreClone") {
                canClone = true;
                //if the objects tag is IgnoreClone then cloning is possible
            } else {
                canClone = false;
            }
        } else if (hit.collider.gameObject == clonedObject && clonedObject.GetComponent<Cloneable>().triggers == 0) {
            canClone = true;
            //as long as the cube isn't inside other colliders and Raycast is hitting the object being cloned, cloning is possible
        }
        o.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * cloneDist, Time.deltaTime * smooth));
        //Using the scollwheel on the mouse increases and decreases the distance
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && cloneDist != 14) {
            cloneDist++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && cloneDist != 2) {
            cloneDist--;
        }
    }

    void Drop() {
        //Cloned Rigidbody
        cloneRb.velocity = new Vector3(0, 0, 0);
        cloneRb.freezeRotation = false;
        //Clone Renderer
        render.shadowCastingMode = ShadowCastingMode.On;
        render.receiveShadows = true;
        clonedObject.GetComponent<BoxCollider>().isTrigger = false; //turns off the trigger, making collisions possible
        if (!clonedObject.GetComponent<Weighted>().overButton) {
            ButtonLevel.ButtonFall();
            //if cube is not over button
            cloneRb.useGravity = true;
            clonedObject.GetComponent<Weighted>().distance = 3;
        } else {
            //if cube is over button
            PuzzleManager.beamButton.AddCube(clonedObject, PuzzleManager.beamButton.CheckCubeHeight(clonedObject).level);   //Check cube height in BeamButton.cs
            ButtonLevel.DropLevelCubes(clonedObject);
            clonedObject.GetComponent<Weighted>().moveRot = true;
        }
        prevClone = clonedObject;
        clonedObject = null;
        cloning = false;
    }

    void Scan(int x, int y) {
        Ray cloneRay = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        bool didHit = false;
        if (Physics.Raycast(cloneRay, out hit)) {
            lookingAt = hit.transform.gameObject;
            if (lookingAt.GetComponent<Cloneable>() != null) {
                //if object has Cloneable component
                clipboard = lookingAt;
                didHit = true;
            }
        }
        //which particles to emit
        if (!didHit)
        {
            failParticles.Emit(particleEmitAmmount);
        }
        else
        {
            copyParticles.Emit(particleEmitAmmount);
        }
    }

    void Clone() {
        if (clipboard != null && this.GetComponent<Player_Pickup>().carrying == false) {
            if (hasCloned) {
                // happy path
                prevClone.GetComponent<Cloneable>().die = true;
                prevClone.GetComponent<Cloneable>().Die();
            }
            cloneDist = 3;
            cloning = true;
            clonedObject = Instantiate(clipboard, mainCamera.transform.position + mainCamera.transform.forward * cloneDist, Quaternion.identity);
            //Resets Weighted settings
            clonedObject.GetComponent<Weighted>().distance = 3;
            clonedObject.GetComponent<Weighted>().moveRot = false;
            clonedObject.GetComponent<Weighted>().overButton = false;
            //Cloned renderer
            render = clonedObject.GetComponent<Renderer>();
            render.shadowCastingMode = ShadowCastingMode.Off;
            //Cloned Rigidbody
            cloneRb = clonedObject.GetComponent<Rigidbody>();
            cloneRb.freezeRotation = true;
            cloneRb.useGravity = false;
            //Extra
            clonedObject.GetComponent<BoxCollider>().isTrigger = true;
            clonedObject.name = "Clone";
            clonedObject.GetComponent<Cloneable>().isClone = true;
            hasCloned = true;
            //Button Rise
            if (prevClone == null) {
                ButtonLevel.ButtonRise();
                return; //return to not run the next update because prevClone doesn't exist yet.
            }
            if (prevClone.gameObject.GetComponent<Weighted>().overButton) {
                ButtonLevel.RiseIndivCube(prevClone);
            } else {
                //if previous clone was not over button
                ButtonLevel.ButtonRise();
            }

            copyParticles.Emit(particleEmitAmmount);
        } else
        {
            failParticles.Emit(particleEmitAmmount);
        }
    }
}