// 
// Copyright 2021 SunnyMonster
//

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public static WorldGenerator Instance { get; private set; }

    [Header("Threading Options")]
    [SerializeField] private int _maxThreads;

    private Queue<Action> _chunkGenFuncs = new Queue<Action>();
    private Queue<Action> _mainThreadFuncs = new Queue<Action>();

    private int _activeThreads = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There are more than one instances of WorldGenerator found in the scene! This instance will be destroyed.");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                new Chunk(x, y);
            }
        }
    }

    private void Update()
    {
        // Manage threading functions
        if ((_chunkGenFuncs.Count > 0 && _activeThreads < _maxThreads) || _maxThreads == 0)
        {
            new Thread(new ThreadStart(ChunkGenThread)).Start();
        }

        // Manage main thread functions (including updating mesh and other Unity API calls)
        Action func = null;
        lock (_mainThreadFuncs)
        {
            if (_mainThreadFuncs.Count > 0)
            {
                func = _mainThreadFuncs.Dequeue();
            }
        }
        func?.Invoke();
    }

    public void AddFunctionToThread(Action func)
    {
        lock (_chunkGenFuncs)
        {
            _chunkGenFuncs.Enqueue(func);
        }
    }

    public void ExecuteOnMainThread(Action func)
    {
        lock (_mainThreadFuncs)
        {
            _mainThreadFuncs.Enqueue(func);
        }
    }

    public void ChunkGenThread()
    {
        _activeThreads++;
        Action function;

        while (true)
        {
            lock (_chunkGenFuncs)
            {
                if (_chunkGenFuncs.Count > 0)
                {
                    function = _chunkGenFuncs.Dequeue();
                }
                else break;
            }
            function();
        }
        _activeThreads--;
    }
}