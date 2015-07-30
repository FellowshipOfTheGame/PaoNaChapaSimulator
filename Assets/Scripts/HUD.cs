using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {
    private Image img;
    private GameObject go;
    private Text txt;

	// Use this for initialization
    void Start() {
        go = GameObject.Find("ItemHUD");
        img = go.GetComponentInChildren<Image>();
        txt = gameObject.GetComponentInChildren<Text>();
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

    public void updateScore(float score)
    {
        txt.text = "Score: " + score;
    }
}
