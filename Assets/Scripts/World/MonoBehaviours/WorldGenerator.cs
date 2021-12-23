// 
// Copyright 2021 SunnyMonster
//

using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private void Start()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                new Chunk(x, y);
            }
        }
    }
}