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
         
         population.Add(bot);
      }

      Time.timeScale = timeScale;
   }
}
