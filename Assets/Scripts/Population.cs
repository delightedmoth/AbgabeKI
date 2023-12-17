using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
   public GameObject sprite;
   public GameObject startingPos;
   public int populationSize;
   private List<GameObject> population = new List<GameObject>();
   public static float elapsed = 0f;
   public float trialTime = 10f;
   public float timeScale = 2f;
   private int generation = 1;
   
   


   void Start()
   {
      for (int i = 0; i < populationSize; i++)
      {
         GameObject bot = Instantiate(sprite, startingPos.transform.position, this.transform.rotation);

         Player player = bot.GetComponent<Player>();
         player.gravity = Random.Range(1.0f, 10.0f);
         player.jumpForce = Random.Range(5.0f, 15.0f);
         player.jumpMultiplier = Random.Range(5.0f, 15.0f);
         player.jumpTime = Random.Range(0.05f, 0.4f);
         player.jump = Random.Range(0,2) < 1;
         
         population.Add(bot);
      }

      Time.timeScale = timeScale;
   }
}
