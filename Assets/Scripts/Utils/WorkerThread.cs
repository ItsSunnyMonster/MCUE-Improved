/*
 *   Copyright (c) 2022 ItsSunnyMonster

 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.

 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.

 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Threading;
using UnityEngine;

public class WorkerThread : MonoBehaviour
{
    public static WorkerThread Instance { get; private set; }

    private Queue<ThreadTask> _workerTasks = new Queue<ThreadTask>();
    private Queue<ThreadTask> _mainThreadTasks = new Queue<ThreadTask>();
    private Dictionary<int, ThreadTask> _finishedTasks = new Dictionary<int, ThreadTask>();

    [SerializeField, ReadOnly] private int _activeThreads = 0;

    public static int Execute(Func<dynamic> task)
    {
        ThreadTask threadTask = new ThreadTask(task);

        lock (Instance._workerTasks)
        {
            Instance._workerTasks.Enqueue(threadTask);
        }
        return threadTask.Id;
    }

    public static int ExecuteOnMainThread(Func<dynamic> task)
    {
        ThreadTask threadTask = new ThreadTask(task);
        lock (Instance._mainThreadTasks)
        {
            Instance._mainThreadTasks.Enqueue(threadTask);
        }
        return threadTask.Id;
    }

    public static dynamic GetResult(int ID)
    {
        while (true)
        {
            if (Instance._finishedTasks.ContainsKey(ID))
            {
                return Instance._finishedTasks[ID].Result;
            }
            Thread.Sleep(1);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("WorkerThread already exists! Deleting this instance.");
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < SystemInfo.processorCount; i++)
        {
            new Thread(ThreadFunc).Start();
            _activeThreads++;
        }
    }

    private void Update()
    {
        if (_mainThreadTasks.Count > 0)
        {
            ThreadTask threadTask;
            lock (_mainThreadTasks)
            {
                threadTask = _mainThreadTasks.Dequeue();
            }
            threadTask.Result = threadTask.Task();
            lock (_finishedTasks)
            {
                _finishedTasks.Add(threadTask.Id, threadTask);
            }
        }
    }

    private void ThreadFunc()
    {
        while (true)
        {
            if (_workerTasks.Count > 0)
            {
                ThreadTask threadTask;
                lock (_workerTasks)
                {
                    threadTask = _workerTasks.Dequeue();
                }
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                threadTask.Result = threadTask.Task();
                stopwatch.Stop();
                //Debug.Log("<color=green>Worker thread finished task</color> <color=red>" + threadTask.Id + "</color> in " + stopwatch.ElapsedMilliseconds + "ms");
                lock (_finishedTasks)
                {
                    _finishedTasks.Add(threadTask.Id, threadTask);
                }
            }
            else
            {
                Thread.Sleep(1);
            }
        }
    }
}

public struct ThreadTask
{
    public Func<dynamic> Task;
    public int Id;
    public dynamic Result;

    private static int _idCounter = 0;

    public ThreadTask(Func<dynamic> task)
    {
        Task = task;
        Id = _idCounter++;
        Result = null;
    }
}
