using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] monsterRefrence;

    private GameObject spawnedMonster;

    [SerializeField]
    private Transform leftPos,rightPos;

    private int randomIndex;
    private int randomSide;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnMonsters());
    }
    IEnumerator spawnMonsters() 
    {
          while(true)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));

                randomIndex = Random.Range(0, monsterRefrence.Length);
                randomSide = Random.Range(0, 2);
                spawnedMonster = Instantiate(monsterRefrence[randomIndex]);

                //left side spawn
                if (randomSide == 0)
                {
                    spawnedMonster.transform.position = leftPos.position;
                    spawnedMonster.GetComponent<Monsters>().monsterSpeed = Random.Range(4, 10);
                }
                //right side spawn
                else
                {
                    spawnedMonster.transform.position = rightPos.position;
                    spawnedMonster.GetComponent<Monsters>().monsterSpeed = -Random.Range(4, 10);
                    spawnedMonster.transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }
    }
}
