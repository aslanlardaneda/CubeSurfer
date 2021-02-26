using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [HideInInspector]
    public static LevelManager instance;

    public GameObject playerPrefab;

    public GameObject groundPrefab;
    public GameObject finalGroundPrefab;

    public List<GameObject> foodPrefab = new List<GameObject>();
    public List<GameObject> groundList = new List<GameObject>();
    public List<GameObject> foodList = new List<GameObject>();

    public List<GameObject> obstaclePrefabList;
    public List<GameObject> obstacleList_1 = new List<GameObject>();
    public List<GameObject> obstacleList_2 = new List<GameObject>();

    public int groundSize;
    public int foodSize;
    public int obstacleSize_1;
    public int obstacleSize_2;

    public float zPos;
    public float foodZPos = 20;
    public float obstacleZPos = 70;

    private void Awake()
    {
        instance = this;
    }

    public void SetLevel()
    {
        groundList.ForEach(x => Destroy(x.gameObject));
        groundList.Clear();
        foodList.ForEach(x => Destroy(x.gameObject));
        foodList.Clear();
        obstacleList_1.ForEach(x => Destroy(x.gameObject));
        obstacleList_1.Clear();
        obstacleList_2.ForEach(x => Destroy(x.gameObject));
        obstacleList_2.Clear();
        zPos = 0;
        foodZPos = 20;
        obstacleZPos = 70;

        SetGround();
        SetObstacles();
        SetFoods();

        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CameraFollow.instance.player = player;
    }

    public void SetGround()
    {
        for (int i = 0; i < groundSize; i++)
        {
            GameObject settedGround = Instantiate(groundPrefab);
            groundList.Add(settedGround);
            settedGround.transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
            zPos += 100;

        }
        GameObject finalGround = Instantiate(finalGroundPrefab);
        finalGround.transform.position = new Vector3(transform.position.x, transform.position.y, groundList.Where(x => x.activeSelf).Last().transform.position.z + 100);
    }

    public void SetObstacles()
    {
        for (int i = 0; i < obstacleSize_1; i++)
        {
            int randomObstacle = Random.Range(0, 5);
            GameObject settedObstacle = Instantiate(obstaclePrefabList[randomObstacle]);
            obstacleList_1.Add(settedObstacle);

            obstacleZPos += Random.Range(30, 70);
            float obstacleBoundaryZPos = Mathf.Clamp(obstacleZPos, 50, 270);
            Vector3 obstaclePos = new Vector3(obstaclePrefabList[randomObstacle].transform.position.x, obstaclePrefabList[randomObstacle].transform.position.y, obstacleBoundaryZPos);
            settedObstacle.transform.position = obstaclePos;
        }
        for (int i = 0; i < obstacleSize_2; i++)
        {
            int randomObstacle = Random.Range(0, obstaclePrefabList.Count);
            GameObject settedObstacle_2 = Instantiate(obstaclePrefabList[randomObstacle]);
            obstacleList_2.Add(settedObstacle_2);

            obstacleZPos += Random.Range(30, 70);
            float obstacleBoundaryZPos = Mathf.Clamp(obstacleZPos, 290, 685);
            Vector3 obstaclePos = new Vector3(obstaclePrefabList[randomObstacle].transform.position.x, obstaclePrefabList[randomObstacle].transform.position.y, obstacleBoundaryZPos);
            settedObstacle_2.transform.position = obstaclePos;
        }
    }

    public void SetFoods()
    {
        for (int i = 0; i < foodSize; i++)
        {
            int randomFood = Random.Range(0, foodPrefab.Count);
            GameObject settedFood = Instantiate(foodPrefab[randomFood]);
            foodList.Add(settedFood);

            float foodXPos = Random.Range(-2, 2);
            foodZPos += Random.Range(15, 30);
            float foodBoundaryZPos = Mathf.Clamp(foodZPos, 15, 685);
            settedFood.transform.position = new Vector3(foodXPos, foodPrefab[randomFood].transform.position.y, foodBoundaryZPos);

        }
    }


}
