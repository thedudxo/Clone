using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloneable : MonoBehaviour {

    public bool isClone = false;
    public int triggers;
    private GameObject button;
    private string dissolveAnim = "Vector1_FA6C32DC";
    private Color cloneColor = new Color(0, 0.8f, 1);
    private Color errorColor = new Color(1, 0, 0);
    public Material dissolve;
    public GameObject player;

    public void Start() {
        dissolve.SetFloat(dissolveAnim, 0.5f);
        if (isClone) {
            this.GetComponent<Renderer>().material = dissolve;
            StartCoroutine(AnimateDissolve());
        }
    }

    public void Update() {
        if(player.GetComponent<Player_Clone>().canClone == false) {
            dissolve.SetColor("Color_711056C5", errorColor);
        } else {
            dissolve.SetColor("Color_711056C5", cloneColor);
        }
    }

    public IEnumerator AnimateDissolve() {
        float lerp = 0.0f;
        while (lerp <= 1) {
            dissolve.SetFloat(dissolveAnim, Mathf.Lerp(0.5f, -1.1f, lerp));
            lerp += Time.deltaTime;
        yield return lerp;
        }
    }

    public void destroyClone() {
        Debug.Log("KILKLIGN THE CLONE");
        if (Player_Pickup_Old.Instance.carriedObject == this.gameObject) {
            Player_Pickup_Old.Instance.dropObject();
            Debug.Log("dropped");
        }

        if(button != null) {
            button.GetComponent<Button_Behaviour>().weights--;
            Debug.Log("yuep");
        }
        Destroy(gameObject);
    }

    public void setButton(GameObject button) {
        this.button = button;
        Debug.Log(button);
    }

    private void OnTriggerEnter(Collider other) {
        
        if (player.GetComponent<Player_Clone>().cloning) {
            triggers++;
            player.GetComponent<Player_Clone>().canClone = false;
        }Debug.Log(triggers);
    }

    private void OnTriggerExit(Collider other) {
        
        if (player.GetComponent<Player_Clone>().cloning) {
            triggers--;
            if (triggers == 0) {
                player.GetComponent<Player_Clone>().canClone = true;
            }Debug.Log(triggers);
        }
    }
}
