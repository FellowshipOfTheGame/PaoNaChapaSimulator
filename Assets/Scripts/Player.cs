using UnityEngine;

public class Player : MonoBehaviour {
    public float time = 0.01666666f;
    public float speed = 2.0f;
    public Vector3 dialogOffset;
    public Vector3 moneyOffset;
    public GameObject[] falas;
    public GameObject money;
    public int cooldown = 0;
    public int cdAction = 20;

    private Rigidbody2D rb2D;
    private Vector2 mov;
    private GameObject hand = null;
    private HUD hud;
    private float score = 0f;
    private float value = 0f;
    private Animator anim;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        hud = GameObject.Find("Canvas").GetComponent<HUD>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (cooldown > 0)
        {
            cooldown--;
        }
	}

    void FixedUpdate(){
        Move();
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

                    if (hand != null)
                    {
                        hud.drawHandItem(hand);
                        value = hit.getValue();
                    }
                }
            }
            else if (obj.tag == "Lixo" && Input.GetKeyDown(KeyCode.Z) && hand != null)
            {
                Destroy(hand);
                hud.removeHandItem();
                setValue(0);
            }
            else if (obj.tag == "Target" && Input.GetKeyDown(KeyCode.Z) && hand != null)
            {
                fila = obj.GetComponent<Fila>();

                if (fila.checkOrder(hand))
                {
                    Score();
                    Speak(money);
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
        Vector3 old = rb2D.position;

        mov.Set(horMov, verMov);

        rb2D.velocity = mov * speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        Vector3 Lhamaspeed = new Vector3(0.0f, 0.0f, mov.x);

        if (Lhamaspeed.magnitude > 0.01)
        {
            transform.rotation = Quaternion.LookRotation(Lhamaspeed.normalized, Vector3.up);
        }

        if(horMov == 0 && verMov == 0){
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
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
            int rand = Random.Range(0, falas.Length);

            message = (GameObject)Instantiate(falas[rand], transform.position + dialogOffset, Quaternion.identity);

            message.transform.SetParent(transform);

            cooldown = 300;
        }
    }

    public void Speak(GameObject img)
    {
        GameObject message;
        
        message = (GameObject)Instantiate(img, transform.position + moneyOffset, Quaternion.identity);
    }

    public void Score()
    {
        this.score += this.value;
        this.value = 0f;
        hud.updateScore(this.score);
    }

    public void Score(float value)
    {
        this.score += value;
    }

    public void setValue(float value)
    {
        this.value = value;
    }
}
