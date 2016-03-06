using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuPrincipal : MonoBehaviour {
    private GameObject button;
    private GameObject insertCoin;
    private bool isShowingText = true;

	// Use this for initialization
	void Start () {
        button = GameObject.FindGameObjectWithTag("Buttons");
        insertCoin = GameObject.Find("Press Start");
        
        button.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape)){
            if (isShowingText)
            {
                Application.Quit();
            }

            destroiBotoes();
            insertCoin.GetComponent<Text>().enabled = true;
            isShowingText = true;
        }
        else if (Input.anyKeyDown && isShowingText)
        {
            criaBotoes();
            insertCoin.GetComponent<Text>().enabled = false;
            isShowingText = false;
        }
	}

    public void criaBotoes()
    {
        button.SetActive(true);
        button.GetComponentInChildren<Button>().Select();
    }

    public void destroiBotoes()
    {
        button.SetActive(false);
    }

    public void onNewGame()
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

    public void onContinue()
    {

    }

    public void onLeaderboard()
    {

    }

    public void onOptions()
    {

    }

    public void onExit()
    {
        Application.Quit();
    }
}
