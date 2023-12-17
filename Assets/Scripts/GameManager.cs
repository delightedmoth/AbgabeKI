using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   [FormerlySerializedAs("initalGameSpeed")] public float initialGameSpeed = 5f;
   public float gameSpeedIncrease = 0.1f;
   public float gameSpeed { get;  set; }

   private Player player;
   private Spawner spawner;
   
   
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         DestroyImmediate(gameObject);
      }
   }

   private void OnDestroy()
   {
      if (Instance == this)
      {
         Instance = null;
      }
   }

   private void Start()
   {
      //player = FindObjectOfType<Player>();
      spawner = FindObjectOfType<Spawner>();
      
      NewGame();
   }

   private void NewGame()
   {
      Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

      foreach (var obstacle in obstacles)
      {
       Destroy(obstacle.gameObject);  
      }
      
      gameSpeed = initialGameSpeed;
      enabled = true;
      
      //player.gameObject.SetActive(true);
      spawner.gameObject.SetActive(true);
   }

   private void Update()
   {
      //gameSpeed += gameSpeedIncrease * Time.deltaTime;
   }

   public void gameOver()
   {
      
      
      //player.gameObject.SetActive(false);
      //spawner.gameObject.SetActive(false);
   }
}
