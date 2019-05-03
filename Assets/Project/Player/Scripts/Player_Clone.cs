using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Clone : MonoBehaviour {

    private GameObject targetObject;        // The real object the player wants to clone
    private GameObject clonedObject;        // The current cloned object visible in the world
    public GameObject targetPosition;       // Position to clone new objects at (in front of player)
    GameObject mainCamera;
    public bool cloned = false;
    public ParticleSystem GunshotParticles;
    public ParticleSystem PasteshotParticles;

    void Start ()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }
	

    public void reset()
    {
        targetObject = null;
        if (Player_Pickup.Instance.carriedObject != null && Player_Pickup.Instance.carriedObject.GetComponent<Cloneable>().isClone)
        {
            Player_Pickup.Instance.dropObject();
        }

        Debug.Log("yehp");
        if(clonedObject != null) { clonedObject.GetComponent<Cloneable>().destroyClone(); }
        cloned = false;
    }

	void Update () {

        int x = Screen.width / 2;
        int y = Screen.height / 2;

        if (Input.GetMouseButtonDown(1))    //right click
        {
            Ray cloneRay = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;

            if(Physics.Raycast(cloneRay, out hit))
            {
                GameObject lookingAt = hit.transform.gameObject;
                PasteshotParticles.transform.position = hit.point;
                PasteshotParticles.Emit(30);

                if (lookingAt.GetComponent<Cloneable>() != null)
                {
                    targetObject = lookingAt;
                    FindObjectOfType<AudioManager>().Play("Beeps");
                } else if (lookingAt.GetComponent<Cloneable>() == null)
                {
                    FindObjectOfType<AudioManager>().Play("Error_Clone");
                }
            }
        }

        if (Input.GetMouseButtonDown(0)) {

            Ray cloneRay = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;

            if (Physics.Raycast(cloneRay, out hit))
            {
                GameObject lookingAt = hit.transform.gameObject;
                GunshotParticles.transform.position = hit.point;
                GunshotParticles.Emit(30);
            }


            if(Player_Pickup.Instance.carriedObject != null && Player_Pickup.Instance.drop)
            {
                Player_Pickup.Instance.dropObject();
            }

            else if (targetObject != null) {

                if (cloned == false)
                {
                    //Initial Clone
                    Player_Pickup.Instance.cloning = true;
                    clonedObject = Instantiate(targetObject, mainCamera.transform.position + mainCamera.transform.forward * Player_Pickup.Instance.distance, Quaternion.identity);
                    clonedObject.name = "Clone";
                    clonedObject.GetComponent<Cloneable>().isClone = true;
                    Player_Pickup.Instance.carrying = true;
                    clonedObject.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    Player_Pickup.Instance.carriedObject = clonedObject;
                    Player_Pickup.Instance.carriedObject.GetComponent<BoxCollider>().isTrigger = true;
                    cloned = true;
                    clonedObject.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;

                    clonedObject.GetComponent<MeshRenderer>().material = Player_Pickup.Instance.hologram;
                    Player_Pickup.Instance.holoAudio.Play();
                }

                else
                {
                    GunshotParticles.transform.position = clonedObject.transform.position;
                    GunshotParticles.Emit(30);
                    clonedObject.gameObject.transform.position = mainCamera.transform.position + mainCamera.transform.forward * Player_Pickup.Instance.distance;
                    if (targetObject.name == "Clone")
                    {
                        targetObject = null;
                        Debug.Log("No clone");
                    }
                    Player_Pickup.Instance.cloning = true;
                    clonedObject.GetComponent<BoxCollider>().isTrigger = true;
                    clonedObject.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
                    clonedObject.GetComponent<MeshRenderer>().material = Player_Pickup.Instance.hologram;
                    clonedObject.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    Player_Pickup.Instance.carriedObject = clonedObject;
                    Player_Pickup.Instance.carrying = true;
                    Player_Pickup.Instance.holoAudio.Play();
                }
            }
        }

    }
}
