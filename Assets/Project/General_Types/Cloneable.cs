using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloneable : MonoBehaviour {

    public bool isClone = false;
    private GameObject button;
    private string dissolveAnim = "Vector1_FA6C32DC";
    public Material dissolve;

    public void Start() {
        dissolve.SetFloat(dissolveAnim, 0.5f);
        if (isClone) {
            this.GetComponent<Renderer>().material = dissolve;
            StartCoroutine(AnimateDissolve());
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

    public void destroyClone()
    {
        Debug.Log("KILKLIGN THE CLONE");
        if (Player_Pickup_Old.Instance.carriedObject == this.gameObject)
        {
            Player_Pickup_Old.Instance.dropObject();
            Debug.Log("dropped");
        }

        if(button != null)
        {
            button.GetComponent<Button_Behaviour>().weights--;
            Debug.Log("yuep");
        }

        Destroy(gameObject);
    }

    public void setButton(GameObject button)
    {
        this.button = button;
        Debug.Log(button);
    }
}
