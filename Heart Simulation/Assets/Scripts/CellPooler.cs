using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public int size;
        public GameObject prefab;
    }

    #region Singleton
    public static CellPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in Pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objPool.Enqueue(obj);
                obj.transform.SetParent(gameObject.transform);
                obj.AddComponent<Animation>();
            }
            poolDictionary.Add(pool.tag, objPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Cell model with tag " + tag + " doesn't exist");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        IPooledCell pooledCell = objToSpawn.GetComponent<IPooledCell>();

        if (pooledCell != null)
        {
            pooledCell.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }

}
