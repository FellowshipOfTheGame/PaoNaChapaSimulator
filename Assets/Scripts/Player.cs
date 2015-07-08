using UnityEngine;

public class Player : MonoBehaviour {
    public float time = 0.01666666f;
    public float vel = 2.0f;
    public Vector3 dialogOffset;
    public GameObject[] falas;
    public int cooldown = 0;

    private Rigidbody2D rb2D;
    private Vector2 mov;
    private GameObject hand = null;
    private HUD hud;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        hud = GameObject.Find("ItemHUD").GetComponent<HUD>();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        if (cooldown > 0)
        {
            cooldown--;
        }
	}

    private void OnTriggerStay2D (Collider2D obj)
    {
        Loja hit;
        Fila fila;
        
        if (obj.tag == "Loja" && Input.GetKeyDown(KeyCode.Z) && hand == null)
        {
            hit = obj.gameObject.GetComponent<Loja>();

            if (hit != null)
            {
                hand = hit.InstantiateItem();
                hand.transform.position = new Vector3(100, 100, 100);
                //hand.transform.position = this.transform.position;
                //hand.transform.SetParent(this.transform);
                if(hand != null)
                    hud.drawHandItem(hand);
            }
        }
        else if (obj.tag == "Lixo" && Input.GetKeyDown(KeyCode.Z) && hand != null)
        {           
            Destroy(hand);
            hud.removeHandItem();
        }
        else if (obj.tag == "Target" && Input.GetKeyDown(KeyCode.Z) && hand != null)
        {
            fila = obj.GetComponent<Fila>();

            if (fila.checkOrder(hand))
            {
                fila.removeEnemyWithOrder();
                Destroy(hand);
                hud.removeHandItem();
            }
            
        }
    }

    private void Move()
    {
        float horMov = Input.GetAxis("Horizontal");
        float verMov = Input.GetAxis("Vertical");
        mov.Set(horMov, verMov);

        rb2D.position += mov * time * vel;
    }

    public GameObject getHand()
    {
        return this.hand;
    }

    public void Speak()
    {
        if (cooldown == 0 && Random.Range(0, 100) > 40)
        {
            GameObject message;
            int rand = Random.Range(0, falas.Length - 1);
            Debug.Log(rand);

            message = (GameObject)Instantiate(falas[rand], transform.position + dialogOffset, Quaternion.identity);

            message.transform.SetParent(transform);

            cooldown = 300;
        }
    }
}
