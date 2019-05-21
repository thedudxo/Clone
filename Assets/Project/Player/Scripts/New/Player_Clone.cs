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
                //copyParticles.Emit(particleEmitAmmount);
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (cloning && canClone) {
                Drop();
                //copyParticles.Emit(particleEmitAmmount);
            }
        }
    }

    void FixedUpdate() {
        if (cloning) {
            CloneCarry(clonedObject);
        }
    }

    void CloneCarry(GameObject o) {
        Vector3 direction = (clonedObject.transform.position - mainCamera.transform.position).normalized;
        Ray cloneRay = new Ray(mainCamera.transform.position, direction * cloneDist);
        RaycastHit hit;
        Physics.Raycast(cloneRay, out hit);
        if (hit.collider.gameObject != clonedObject) {
            if (hit.collider.gameObject.tag == "IgnoreClone") {
                canClone = true;
            } else {
                canClone = false;
            }
        } else if (hit.collider.gameObject == clonedObject && clonedObject.GetComponent<Cloneable>().triggers == 0) {
            canClone = true;
        }
        o.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * cloneDist, Time.deltaTime * smooth));
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && cloneDist != 10) {
            cloneDist++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && cloneDist != 2) {
            cloneDist--;
        }
        Debug.DrawRay(mainCamera.transform.position, direction * cloneDist);
    }

    void Drop() {
        cloneRb.velocity = new Vector3(0, 0, 0);
        cloneRb.freezeRotation = false;
        render.shadowCastingMode = ShadowCastingMode.On;
        render.receiveShadows = true;
        clonedObject.GetComponent<BoxCollider>().isTrigger = false;
        if (!clonedObject.GetComponent<Weighted>().overButton) {
            cloneRb.useGravity = true;
            ButtonLevel.ButtonFall();
        } else {
            ButtonLevel.cubesOverButton.Add(clonedObject);
            clonedObject.GetComponent<Weighted>().ChangeIndex();
            clonedObject.GetComponent<Weighted>().movePos = true;
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
                clipboard = lookingAt;
                Debug.Log("Can clone");
                didHit = true;
            } else {
                Debug.Log("Cant clone " + hit.transform.gameObject);
            }
        }

        //which particles to emit
        if (!didHit)
        {
            //failParticles.Emit(particleEmitAmmount);
        }
        else
        {
            //copyParticles.Emit(particleEmitAmmount);
        }
    }

    void Clone() {
        if (clipboard != null && this.GetComponent<Player_Pickup>().carrying == false) {
            if (hasCloned) {
                prevClone.GetComponent<Cloneable>().die = true;
                prevClone.GetComponent<Cloneable>().Die();
            }
            cloneDist = 3;
            cloning = true;
            clonedObject = Instantiate(clipboard, mainCamera.transform.position + mainCamera.transform.forward * cloneDist, Quaternion.identity);
            clonedObject.GetComponent<Weighted>().distance = 3;
            clonedObject.GetComponent<Weighted>().movePos = false;
            clonedObject.GetComponent<Weighted>().overButton = false;
            render = clonedObject.GetComponent<Renderer>();
            cloneRb = clonedObject.GetComponent<Rigidbody>();
            render.shadowCastingMode = ShadowCastingMode.Off;
            cloneRb.freezeRotation = true;
            cloneRb.useGravity = false;
            clonedObject.GetComponent<BoxCollider>().isTrigger = true;
            clonedObject.name = "Clone";
            clonedObject.GetComponent<Cloneable>().isClone = true;
            hasCloned = true;
            if (prevClone == null) {
                ButtonLevel.ButtonRise();
                return;
            }
            if (!prevClone.gameObject.GetComponent<Weighted>().overButton) {
                ButtonLevel.ButtonRise();
            }

            //copyParticles.Emit(particleEmitAmmount);
        } else
        {
            //failParticles.Emit(particleEmitAmmount);
        }
    }
}