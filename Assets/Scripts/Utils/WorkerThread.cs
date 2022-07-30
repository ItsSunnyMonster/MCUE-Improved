/*
 *   Copyright (c) 2022 ItsSunnyMonster
 *   All rights reserved.

 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at

 *   http://www.apache.org/licenses/LICENSE-2.0

 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
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

    public static int Execute(Func<dynamic> task)
    {
        ThreadTask threadTask = new ThreadTask(task);

        Instance._workerTasks.Enqueue(threadTask);
        return threadTask.Id;
    }

    public static int ExecuteOnMainThread(Func<dynamic> task)
    {
        ThreadTask threadTask = new ThreadTask(task);
        Instance._mainThreadTasks.Enqueue(threadTask);
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
            //Thread.Sleep(1);
        }
    }

    private void Start()
    {
        if (Instance != null)
        {
            Debug.LogWarning("WorkerThread already exists! Deleting this instance.");
            Destroy(this);
            return;
        }
        Instance = this;

        new Thread(() =>
        {
            while (true)
            {
                if (_workerTasks.Count > 0)
                {
                    ThreadTask threadTask = _workerTasks.Dequeue();
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    threadTask.Result = threadTask.Task();
                    stopwatch.Stop();
                    //Debug.Log("Worker thread finished task " + threadTask.Id + " in " + stopwatch.ElapsedMilliseconds + "ms");
                    _finishedTasks.Add(threadTask.Id, threadTask);
                }
                // else
                // {
                //     Thread.Sleep(1);
                // }
            }
        }).Start();
    }

    private void Update()
    {
        while (_mainThreadTasks.Count > 0)
        {
            ThreadTask threadTask = _mainThreadTasks.Dequeue();
            threadTask.Result = threadTask.Task();
            _finishedTasks.Add(threadTask.Id, threadTask);
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
