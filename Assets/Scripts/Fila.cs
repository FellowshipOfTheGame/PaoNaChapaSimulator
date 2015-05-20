using UnityEngine;
using System.Collections;

public class Fila : MonoBehaviour {
    private ArrayList enemies;
    
    public float dist = 1f;
    public float cte = 0.1f;


	// Use this for initialization
	void Start () {
        enemies = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    public void addEnemy(GameObject thing)
    {
        int index = enemies.Add(thing);
        NPC enemy = thing.GetComponent<NPC>();
        
        enemy.setLinePosition(index);
        
        if (index == 0)
        {
            enemy.setIsFirst(true);
        }
        
        enemy.setDist((index * dist) + cte);
    }

    public bool removeEnemyWithOrder(GameObject item)
    {
        if (enemies.Count > 0)
        {
            if (checkOrder(item))
            {
                GameObject remove = (GameObject)enemies[0];

                enemies.RemoveAt(0);
                Destroy(remove);

                updateLine();

                return true;
            }
        }

        return false;
    }

    public void removeEnemyWithTimer(int index)
    {
        if (enemies.Count > 0)
        {
            GameObject remove = (GameObject)enemies[index];
            NPC thing;

            enemies.RemoveAt(index);

            updateLine();

            thing = remove.GetComponent<NPC>();
            thing.setTarget(GameObject.Find("GameManager"));
            thing.setAngriness(true);
        }
    }

    public void updateLine()
    {
        int index = 0;

        if (enemies.Count > 0)
        {
            ((GameObject)enemies[0]).GetComponent<NPC>().setIsFirst(true);
        }

        foreach (GameObject thing in enemies)
        {
            thing.GetComponent<NPC>().setDist((index * dist) + cte, index++);
        }
    }

    public bool checkOrder(GameObject item)
    {
        if (((GameObject)enemies[0]).GetComponent<NPC>().checkPedido(item))
        {
            return true;
        }

        return false;
    }
}
