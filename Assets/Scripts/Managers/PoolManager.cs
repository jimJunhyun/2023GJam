using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolData
{
    public PoolAble obj;
    public int num;
}


public abstract class PoolAble : MonoBehaviour
{
    public abstract void Reset();
    //public abstract void Disable();
}


public class PoolManager : Singleton<PoolManager>
{

    //[SerializeField] private List<PoolData> _data;

    private Dictionary<string, Pool<PoolAble>> _pools = new Dictionary<string, Pool<PoolAble>>();
    [SerializeField] PoolList list;

    private void Awake()
    {
        if(list == null)
            list = Resources.Load<PoolList>("PoolList");


        foreach (PoolData p in list.poolList)
        {
            CreatePool(p.obj);
        }
        

    }

    public void CreatePool(PoolAble prefab, int cnt = 5)
    {
        Pool<PoolAble> pool = new Pool<PoolAble>(prefab, transform, cnt);
        _pools.Add(prefab.gameObject.name, pool);
    }

    public PoolAble Pop(string prefabName)
    {
        if (_pools.ContainsKey(prefabName) == false)
        {
            Debug.LogError("프리펩없데");
            return null;
        }


        PoolAble item = _pools[prefabName].Pop();
        item.Reset();
        return item;
    }

    public void Push(PoolAble obj)
    {
        obj.transform.SetParent(this.transform);
        _pools[obj.name].Push(obj);
    }
    //public void RemovePool(string prefabName)
    //{
    //    if (_pools.ContainsKey(prefabName))
    //    {
    //        _pools.Remove(prefabName);
    //    }
    //    else
    //    {
    //        Debug.LogError("프리펩없데..");
    //    }
    //    
    //    for(int i =0; i < transform.childCount; i++)
    //    {
    //        if(transform.GetChild(i).gameObject.name == prefabName)
    //        {
    //            Destroy(transform.GetChild(i).gameObject);
    //        }
    //    }
    //}
}

public class Pool<T> where T : PoolAble
{
    private Stack<T> _pool = new Stack<T>();
    private T _prefab;
    private Transform _parent;

    public Pool(T prefab, Transform parent, int count = 5)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < count; i++) // �̸����� �̸� �����
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }

    public T Pop()
    {
        T obj = null;
        if (_pool.Count <= 0)
        {
            obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            obj = _pool.Pop();
            obj.gameObject.SetActive(true);
        }
        return obj;
    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }
}

