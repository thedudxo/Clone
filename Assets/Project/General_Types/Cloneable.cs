using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Cloneable : MonoBehaviour {

    public bool isClone = false;
    public bool die = false;
    public int triggers;
    private GameObject button;
    private string dissolveAnim = "Vector1_FA6C32DC";
    private Color cloneColor = new Color(0, 0.8f, 1);
    private Color errorColor = new Color(1, 0, 0);
    public Material materialize;
    public Material dissolve;
    public GameObject player;
    public Transform reset;

    public void Start() {
        materialize.SetFloat(dissolveAnim, 0.5f);
        dissolve.SetFloat(dissolveAnim, -1.1f);
        if (isClone) {
            this.GetComponent<Renderer>().material = materialize;
            StartCoroutine(Materialize());
        }
    }

    public void Update() {
        if(player.GetComponent<Player_Clone>().canClone == false) {
            materialize.SetColor("Color_711056C5", errorColor);
        } else {
            materialize.SetColor("Color_711056C5", cloneColor);
        }
    }

    public void Die() {
        if (!die) {
            return;
        } else {
            this.GetComponent<Renderer>().material = dissolve;
            this.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
            StartCoroutine(Dissolve());
        }
    }

    public IEnumerator Materialize() {
        float lerp = 0.0f;
        while (lerp <= 1) {
            materialize.SetFloat(dissolveAnim, Mathf.Lerp(0.5f, -1.1f, lerp));
            lerp += Time.deltaTime;
        yield return lerp;
        }
    }

    public IEnumerator Dissolve() {
        float lerp = 0.0f;
        while (lerp <= 1) {
            dissolve.SetFloat(dissolveAnim, Mathf.Lerp(-1.1f, 0.5f, lerp));
            lerp += Time.deltaTime;
            yield return lerp;
        }
        transform.position = reset.position;
        yield return new WaitForEndOfFrame();
        Destroy(this.gameObject);
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
        if (gameObject.GetComponent<Weighted>().overButton) { return; }
        if (player.GetComponent<Player_Clone>().cloning) {
            triggers++;
            player.GetComponent<Player_Clone>().canClone = false;
        }//Debug.Log(triggers);
    }

    private void OnTriggerExit(Collider other) {
        if (gameObject.GetComponent<Weighted>().overButton) { return; }
        if (player.GetComponent<Player_Clone>().cloning) {
            triggers--;
            if (triggers == 0) {
                player.GetComponent<Player_Clone>().canClone = true;
            }Debug.Log(triggers);
        }
    }
}
