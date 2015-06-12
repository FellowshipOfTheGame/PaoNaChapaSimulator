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

    public void removeEnemyWithOrder()
    {
        if (enemies.Count > 0)
        {
                GameObject remove = (GameObject)enemies[0];
                NPC thing;

                enemies.RemoveAt(0);

                updateLine();

                thing = remove.GetComponent<NPC>();
                thing.setTarget(GameObject.Find("GameManager"));
                thing.setHappiness(true);
                thing.setIsLeaving(true);
        }
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
            thing.setHappiness(false);
        }
    }

    public void updateLine()
    {
        int index = 0;

        if (enemies.Count > 0)
        {
            ((GameObject)enemies[0]).GetComponent<NPC>().setIsFirst(true);
            ((GameObject)enemies[0]).GetComponent<NPC>().setCollision(false);
        }

        foreach (GameObject thing in enemies)
        {
            thing.GetComponent<NPC>().setDist((index * dist) + cte, index++);
        }
    }

    public bool checkOrder(GameObject item)
    {
        if (enemies.Count > 0)
        {
            return ((GameObject)enemies[0]).GetComponent<NPC>().checkPedido(item);
        }

        return false;
    }
}
