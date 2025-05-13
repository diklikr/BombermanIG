using UnityEngine;
using System;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System.Net;

public class PlayerBombManager : MonoBehaviour
{
    inputmanager InputManager;
    public GameObject Bomb;

    [SerializeField] Transform bombPoolParent;
    [Header("Bomb Stats")]
    [SerializeField] int maxBombs;
   [SerializeField] int bombRange;
   
    List<GameObject> bombsPool = new List<GameObject>();
    
    private void Awake()
    {
        InputManager = GetComponent<inputmanager>();
    }
    private void Start()
    {
        for (int i = 0; i < maxBombs; i++)
        {
            GameObject bomb = Instantiate(Bomb, bombPoolParent);
            bomb.SetActive(false);
            bombsPool.Add(bomb);
        }
    }
    private void OnEnable()
    {
        InputManager.OnBombPressed.AddListener(DeployBomb);
    }
    private void OnDisable()
    {
        maxBombs = 1;
        bombRange = 1;
        InputManager.OnBombPressed.RemoveListener(DeployBomb);
    }
    private void DeployBomb()
    {
        foreach (GameObject bomb in bombsPool)
        {
            if (bomb.activeSelf) continue;
            {
                bomb.transform.position = transform.position;
                bomb.GetComponent<Bombscript>().SetBombRange(bombRange);
                bomb.SetActive(true);
                return;
            }
        }
        Instantiate(Bomb, transform.position, Quaternion.identity);
    }
    public void AddExtraBomb()
    {
        maxBombs++;
    GameObject bomb = Instantiate(Bomb, bombPoolParent);
        bomb.SetActive(false);
        bombsPool.Add(bomb) ;
    }

    public void AddExtraRange()
    {
        bombRange++;
    }
}
