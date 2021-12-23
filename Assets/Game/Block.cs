//
// Copyright (c) SunnyMonster
//

using UnityEngine;

public abstract class Block
{
    public Vector3 ChunkPosition { get; private set; }

    public Block(Vector3 position)
    {
        ChunkPosition = position;
    }

    public Block(float x, float y, float z) : this(new Vector3(x, y, z)) {}
}