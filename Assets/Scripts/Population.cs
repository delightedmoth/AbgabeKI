using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class Population : MonoBehaviour
{
   public GameObject sprite;
   public GameObject startingPos;
   public int populationSize;
   public List<GameObject> population;
   public static float elapsed = 0f;
   public float trialTime = 10f;
   public float timeScale = 2f;
   private float startTime = 0f;
   private int generation = 1;

   public TMPro.TextMeshProUGUI textMesh;


   void Start()
   {
      for (int i = 0; i < populationSize; i++)
      {
         GameObject bot = Instantiate(sprite, startingPos.transform.position, this.transform.rotation);

         Player player = bot.GetComponent<Player>();
         player.gravity = Random.Range(1.0f, 10.0f);
         player.jumpForce = Random.Range(1.0f, 10.0f);
         player.jumpMultiplier = Random.Range(1.0f, 10.0f);
         player.jumpTime = Random.Range(0.05f, 0.4f);
         player.distanceforJump = Random.Range(0.5f, 10.5f);
         
         population.Add(bot);
         
         textMesh.text = "Gen: " + generation;
      }

      Time.timeScale = timeScale;
   }

   GameObject GeneSwap(Player parent1, Player parent2)
   {
      GameObject bot = Instantiate(sprite, startingPos.transform.position, this.transform.rotation);
      Player player = bot.GetComponent<Player>();
      
      player.gravity = parent1.gravity;
      player.jumpForce = parent2.jumpForce;
      player.jumpMultiplier = parent1.jumpMultiplier;
      player.jumpTime = parent2.jumpTime;
      player.distanceforJump = parent1.distanceforJump;

      return bot;
   }

   void BREED()
   {
      startTime = Time.realtimeSinceStartup;
      List<GameObject> sortedPopulation = population.OrderByDescending(o => o.GetComponent<Player>().fitness).ToList();

      int halfPopulation = (int)(sortedPopulation.Count / 2.0f);
      population.Clear();
      for (int i = 0; i < halfPopulation; i++)
      {
         population.Add(GeneSwap(sortedPopulation[i].GetComponent<Player>(),
            sortedPopulation[i + 1].GetComponent<Player>()));
         population.Add(GeneSwap(sortedPopulation[i + 1].GetComponent<Player>(),
            sortedPopulation[i].GetComponent<Player>()));
      }

      for (int i = 0; i < sortedPopulation.Count; i++)
      {
         Destroy(sortedPopulation[i]);
      }

      generation++;
      textMesh.text = "Gen: " + generation;
   }

   private void Update()
   {
      
      elapsed += Time.deltaTime;
      if (elapsed >= trialTime){
         BREED();
         //GameManager.Instance.gameSpeed = GameManager.Instance.initialGameSpeed;
         elapsed = 0f;
         GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
         foreach (GameObject obstacle in obstacles)
         {
            Destroy(obstacle);
         }
      }
      
      int j = 0;
      for (int i = 0; i < populationSize; i++)
      {
         
         if (population[i].GetComponent<Player>().dead == true)
         {
            j++;
         }

         if (j == populationSize)
         {
            BREED();
            elapsed = 0f;
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obstacle in obstacles)
            {
               Destroy(obstacle);
            }
         }
      }

      
   }
}
