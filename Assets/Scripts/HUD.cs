using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {
    private Image img;

	// Use this for initialization
	void Start () {
        img = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void drawHandItem(GameObject item)
    {
        img.enabled = true;
        img.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

    public void removeHandItem()
    {
        img.enabled = false;
    }
}
