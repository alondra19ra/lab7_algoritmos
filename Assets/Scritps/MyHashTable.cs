using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHashTable<TKey, TValue>
{
    private readonly int _capacity;

    private readonly LinkedList<KeyValuePair<TKey, TValue>>[] _buckets;

    public MyHashTable(int capacity)
    {
        _capacity = Mathf.Max(1, capacity);
        _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[_capacity];
    }

    private int GetBucketIndex(TKey key)
    {
        int hash = key.GetHashCode() & 0x7FFFFFFF;
        return hash % _capacity;
    }

    public void Add(TKey key, TValue value)
    {
        int index = GetBucketIndex(key);
        if (_buckets[index] == null)
            _buckets[index] = new LinkedList<KeyValuePair<TKey, TValue>>();

        foreach(var kv in _buckets[index])
        {
            if(EqualityComparer<TKey>.Default.Equals(kv.Key, key))
            {
                Debug.LogWarning($"[MyHashTable] La clave ya existe: {key}");
                return;
            }
        }

        _buckets[index].AddLast(new KeyValuePair<TKey, TValue>(key, value));
    }

    public TValue Get(TKey key)
    {
        int index = GetBucketIndex(key);
        var bucket = _buckets[index];
        if (bucket != null)
        {
            foreach(var kv in bucket)
            {
                if (EqualityComparer<TKey>.Default.Equals(kv.Key, key))
                    return kv.Value;
            }
        }

        Debug.LogWarning($"[MyHashTable] Clave no encontrada: {key}");
        return default;
    }

    public bool Remove(TKey key)
    {
        int index = GetBucketIndex(key);
        var bucket = _buckets[index];
        if(bucket != null)
        {
            var node = bucket.First;
            while(node != null)
            {
                if(EqualityComparer<TKey>.Default.Equals(node.Value.Key, key))
                {
                    bucket.Remove(node);
                    return true;
                }
                node = node.Next;
            }
        }

        Debug.LogWarning($"[MyHashTable] No se encontro la clave para eliminar: {key}");
        return false;
    }

    public bool ConstainsKey(TKey key)
    {
        int index = GetBucketIndex(key);
        var bucket = _buckets[index];
        if(bucket != null)
        {
            foreach(var kv in bucket)
            {
                if (EqualityComparer<TKey>.Default.Equals(kv.Key, key))
                    return true;
            }
        }
        return false;
    }
}