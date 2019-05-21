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
    private Color buttonColor = new Color(0, 1, 0);
    public Material materialize;
    public Material dissolve;

    public void Start() {
        materialize.SetFloat(dissolveAnim, 0.5f);
        dissolve.SetFloat(dissolveAnim, -1.1f);
        if (isClone) {
            this.GetComponent<Renderer>().material = materialize;
            StartCoroutine(Materialize());
        }
    }

    public void Update() {
        if(PlayerManager.player_Clone.canClone == false) {
            materialize.SetColor("Color_ED5ABF3C", errorColor);
            materialize.SetFloat("Vector1_1A249BD6", 0.4f);
        } else {
            materialize.SetColor("Color_ED5ABF3C", cloneColor);
            materialize.SetFloat("Vector1_1A249BD6", 0.4f);
        }
        if (gameObject.GetComponent<Weighted>().overButton) {
            materialize.SetFloat("Vector1_1A249BD6", -1);
            materialize.SetColor("Color_ED5ABF3C", buttonColor);
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
        if (!gameObject.GetComponent<Weighted>().overButton) {
            //ButtonLevel.ButtonRise();
        }
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
        transform.position = new Vector3(0, -100, 0);
        yield return new WaitForEndOfFrame();
        gameObject.GetComponent<Weighted>().destroyed = true;
        yield return new WaitForEndOfFrame();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "IgnoreClone") {
            Debug.Log("Change Color");
            return;
        }
        if (gameObject == PlayerManager.player_Clone.clonedObject) {
            triggers++;
            PlayerManager.player_Clone.canClone = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "IgnoreClone") { return; }
        if (gameObject == PlayerManager.player_Clone.clonedObject) {
            triggers--;
            if (triggers == 0) {
                PlayerManager.player_Clone.canClone = true;
            }
        }
    }
}
