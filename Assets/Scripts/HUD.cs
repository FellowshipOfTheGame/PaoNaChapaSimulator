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
        go = GameObject.Find("ScoreIcon");
        txt = go.GetComponentInChildren<Text>();
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
        txt.text = score.ToString();
    }

    public void onPlayAgain()
    {
        float fadeTime = GameObject.Find("Canvas").GetComponent<Fading>().BeginFade(1);
        Debug.Log(fadeTime);
        StartCoroutine(changeLevelFade(fadeTime));
    }

    private IEnumerator changeLevelFade(float fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel("Game");
    }

    public void onExitGame()
    {
        float fadeTime = GameObject.Find("Canvas").GetComponent<Fading>().BeginFade(1);
        Debug.Log(fadeTime);
        StartCoroutine(endGameFade(fadeTime));
    }

    private IEnumerator endGameFade(float fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);
        Application.Quit();
    }
}
