//
// Copyright (c) SunnyMonster
//

using UnityEngine;

public class Block
{
    public Vector3 ChunkPosition { get; private set; }

    public Block(Vector3 position)
    {
        ChunkPosition = position;
    }

    public Block(float x, float y, float z)
    {
        ChunkPosition = new Vector3(x, y, z);
    }
}