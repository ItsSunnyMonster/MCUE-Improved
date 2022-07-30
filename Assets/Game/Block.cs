//
// Copyright 2021 SunnyMonster
//

using UnityEngine;

public abstract class Block
{
    public Vector3UInt ChunkPosition { get; private set; }

    public Block(Vector3UInt position)
    {
        ChunkPosition = position;
    }

    public Block(uint x, uint y, uint z) : this(new Vector3UInt(x, y, z)) { }

    public static Block Create(BlockInShader block)
    {
        switch (block.BlockType)
        {
            case BlockType.BEDROCK:
                return new b_Bedrock(block.Position);
            case BlockType.WIREFRAME:
                return new b_WireframeBlock(block.Position);
            default:
                return new SolidBlock(block.Position);
        }
    }
}

public struct BlockInShader
{
    public Vector3UInt Position;
    public BlockType BlockType;

    public static BlockInShader zero = new BlockInShader(new Vector3UInt(0, 0, 0), 0);

    public BlockInShader(Vector3UInt position, BlockType blockType)
    {
        Position = position;
        BlockType = blockType;
    }

    // Equals operator
    public static bool operator ==(BlockInShader a, BlockInShader b)
    {
        return a.Position == b.Position && a.BlockType == b.BlockType;
    }

    // Not equals operator
    public static bool operator !=(BlockInShader a, BlockInShader b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

public enum BlockType : uint
{
    CPU = 0,
    BEDROCK = 1,
    WIREFRAME = 2,
}