using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningHandler : MonoBehaviour
{
    public bool DoOpening = true;
    public AudioClip openingSound;

    public void Start() {
        if (!DoOpening)
            Destroy(this.gameObject);
        else StartCoroutine(Timer());
    }

    IEnumerator Timer() {
        Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(openingSound);
        yield return new WaitForSeconds(3.5f);
        Destroy(this.gameObject);

    }
}
