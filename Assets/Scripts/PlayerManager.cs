using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public static PlayerManager instance;

    public float forwardSpeed;
    public int stack_length;
    public float upperFoodCount = 1f;
    public GameObject[] go_stack;
    public GameObject upperPlayer;

    public bool isFinished;

    #region Fake Foods & Toss Back
    [Header("Fake Foods & Toss Back")]
    public List<GameObject> fakeFoodList_1 = new List<GameObject>();
    public List<GameObject> fakeFoodList_2 = new List<GameObject>();
    public List<GameObject> fakeFoodList_3 = new List<GameObject>();
    public List<GameObject> fakeFoodList_4 = new List<GameObject>();
    public List<GameObject> fakeFoodList_5 = new List<GameObject>();
    public GameObject fakeFoodPrefab;
    public int fakeFoodCount;
    public int collidedObsCount_1 = 0;
    public int collidedObsCount_2 = 0;
    public int collidedObsCount_3 = 0;
    public int collidedObsCount_4 = 0;
    public int collidedObsCount_5 = 0;
    public GameObject cube_1;
    public GameObject cube_2;
    public GameObject cube_3;
    public GameObject cube_4;
    public GameObject cube_5;
    public float tossBackCount;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        stack_length = transform.childCount;

        SpawnFakeFood_1();
        SpawnFakeFood_2();
        SpawnFakeFood_3();
        SpawnFakeFood_4();
        SpawnFakeFood_5();
    }

    void Update()
    {

        if (!GameManager.instance.isGameRunning)
            return;

        if (!GameSceneManager.instance.tutorialImage.activeInHierarchy)
            transform.Translate(Vector3.forward * forwardSpeed);

        float xBoundaryPos = Mathf.Clamp(transform.position.x, -1.99f, 1.99f);
        transform.position = new Vector3(xBoundaryPos, transform.position.y, transform.position.z);

        go_stack = new GameObject[stack_length];
        for (int i = 0; i < stack_length; i++)
        {
            go_stack[i] = transform.GetChild(i).gameObject;
        }
    }

    public void SetDefault()
    {
        isFinished = false;
        Destroy(GameObject.FindGameObjectWithTag("NotFood"));

        fakeFoodList_1.ForEach(x => Destroy(x.gameObject));
        fakeFoodList_1.Clear();
        fakeFoodList_2.ForEach(x => Destroy(x.gameObject));
        fakeFoodList_2.Clear();
        fakeFoodList_3.ForEach(x => Destroy(x.gameObject));
        fakeFoodList_3.Clear();
        fakeFoodList_4.ForEach(x => Destroy(x.gameObject));
        fakeFoodList_4.Clear();
        fakeFoodList_5.ForEach(x => Destroy(x.gameObject));
        fakeFoodList_5.Clear();
    }

    public void StackFood()
    {
        for (int i = 0; i < stack_length - 1; i++)
        {
            go_stack[stack_length - 1].SetActive(true);
            go_stack[i].transform.localPosition += new Vector3(0, transform.localPosition.y + upperFoodCount, 0);
            go_stack[i].transform.SetAsLastSibling();
            go_stack[stack_length - 1].transform.localPosition = new Vector3(0, transform.localPosition.y + 0.5f, 0);
        }
    }

    public void Unstack()
    {
        if (upperPlayer.transform.localPosition.y <= 1.5)
            return;

        for (int i = 0; i < stack_length - 1; i++)
        {
            go_stack[0].SetActive(false);
            go_stack[i].transform.localPosition -= new Vector3(0, transform.localPosition.y + upperFoodCount, 0);
            if (go_stack[i].transform.position.y < 0)
            {
                go_stack[i].transform.SetAsLastSibling();
            }
        }

        upperPlayer.GetComponent<Collider>().enabled = false;
        StartCoroutine(UpperPlayerColliderEnable());
    }

    // For prevent to interaction between upperplayertrigger and upperPlayer which is fall down after notFood and obstacle collision
    public IEnumerator UpperPlayerColliderEnable()
    {
        yield return new WaitForSeconds(0.3f);
        upperPlayer.GetComponent<Collider>().enabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isFinished)
            return;

        if (other.CompareTag("ObsTrigger1"))
        {
            collidedObsCount_1++;
            cube_1.transform.position = other.gameObject.transform.position;
            other.gameObject.SetActive(false);
            TossBackFakeFood_1();
            Unstack();
        }
        if (other.CompareTag("ObsTrigger2"))
        {
            collidedObsCount_2++;
            cube_2.transform.position = other.gameObject.transform.position;
            other.gameObject.SetActive(false);
            TossBackFakeFood_2();
            Unstack();
        }
        if (other.CompareTag("ObsTrigger3"))
        {
            collidedObsCount_3++;
            cube_3.transform.position = other.gameObject.transform.position;
            other.gameObject.SetActive(false);
            TossBackFakeFood_3();
            Unstack();
        }
        if (other.CompareTag("ObsTrigger4"))
        {
            collidedObsCount_4++;
            cube_4.transform.position = other.gameObject.transform.position;
            other.gameObject.SetActive(false);
            TossBackFakeFood_4();
            Unstack();
        }
        if (other.CompareTag("ObsTrigger5"))
        {
            collidedObsCount_5++;
            cube_5.transform.position = other.gameObject.transform.position;
            other.gameObject.SetActive(false);
            TossBackFakeFood_5();
            Unstack();
        }

        if (other.CompareTag("EndTrigger"))
        {
            GameManager.instance.isSucces = true;
        }
        if (other.CompareTag("FinalObstacle_10X"))
        {
            isFinished = true;
        }
    }

    #region Fake Food Spawners
    public void SpawnFakeFood_1()
    {
        for (int i = 0; i < fakeFoodCount; i++)
        {
            GameObject go = Instantiate(fakeFoodPrefab);
            fakeFoodList_1.Add(go);
        }
    }

    public void SpawnFakeFood_2()
    {
        for (int i = 0; i < fakeFoodCount; i++)
        {
            GameObject go = Instantiate(fakeFoodPrefab);
            fakeFoodList_2.Add(go);
        }
    }
    public void SpawnFakeFood_3()
    {
        for (int i = 0; i < fakeFoodCount; i++)
        {
            GameObject go = Instantiate(fakeFoodPrefab);
            fakeFoodList_3.Add(go);
        }
    }
    public void SpawnFakeFood_4()
    {
        for (int i = 0; i < fakeFoodCount; i++)
        {
            GameObject go = Instantiate(fakeFoodPrefab);
            fakeFoodList_4.Add(go);
        }
    }
    public void SpawnFakeFood_5()
    {
        for (int i = 0; i < fakeFoodCount; i++)
        {
            GameObject go = Instantiate(fakeFoodPrefab);
            fakeFoodList_5.Add(go);
        }
    }

    #endregion

    #region Fake Food Toss Backers
    public void TossBackFakeFood_1()
    {
        for (int i = 0; i < collidedObsCount_1; i++)
        {
            fakeFoodList_1[i].SetActive(true);
            fakeFoodList_1[i].transform.position = new Vector3(transform.position.x, cube_1.transform.position.y, transform.position.z - tossBackCount);
        }
        collidedObsCount_1 = 0;
    }

    public void TossBackFakeFood_2()
    {
        for (int i = 0; i < collidedObsCount_2; i++)
        {
            fakeFoodList_2[i].SetActive(true);
            fakeFoodList_2[i].transform.position = new Vector3(transform.position.x, cube_2.transform.position.y, transform.position.z - tossBackCount);
        }
        collidedObsCount_2 = 0;
    }

    public void TossBackFakeFood_3()
    {
        for (int i = 0; i < collidedObsCount_3; i++)
        {
            fakeFoodList_3[i].SetActive(true);
            fakeFoodList_3[i].transform.position = new Vector3(transform.position.x, cube_3.transform.position.y, transform.position.z - tossBackCount);
        }
        collidedObsCount_3 = 0;
    }
    public void TossBackFakeFood_4()
    {
        for (int i = 0; i < collidedObsCount_4; i++)
        {
            fakeFoodList_4[i].SetActive(true);
            fakeFoodList_4[i].transform.position = new Vector3(transform.position.x, cube_4.transform.position.y, transform.position.z - tossBackCount);
        }
        collidedObsCount_4 = 0;
    }
    public void TossBackFakeFood_5()
    {
        for (int i = 0; i < collidedObsCount_5; i++)
        {
            fakeFoodList_5[i].SetActive(true);
            fakeFoodList_5[i].transform.position = new Vector3(transform.position.x, cube_5.transform.position.y, transform.position.z - tossBackCount);
        }
        collidedObsCount_5 = 0;
    }
    #endregion

}
