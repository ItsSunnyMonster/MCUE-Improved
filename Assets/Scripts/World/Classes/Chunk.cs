// 
// Copyright 2021 SunnyMonster
//

using UnityEngine;
using System.Collections.Generic;

public class Chunk
{
    private GameObject _chunkObject;
    private GameObject _chunkParent;

    private const int CHUNK_SIZE = 16;

    // The Vector3 in this dictionary is not the world position of the block
    // It is the position of the block in the chunk
    // so e.g. block 9, 64, 9 in chunk 1, 0 is at 25, 64, 9 in world position
    private Dictionary<Vector3, Block> _blocks = new Dictionary<Vector3, Block>();

    private List<Vector3> _vertices = new List<Vector3>();
    private List<int> _triangles = new List<int>();
    private List<Vector2> _uvs = new List<Vector2>();

    public Vector2Int ChunkPosition { get; private set; }

    // The coordinates parameter is not the world position of the chunk
    // Instead it is the coordinates of the chunk
    // so e.g. chunk 1, 0 is at 16, 0, 0 in world position
    public Chunk(Vector2Int coordinates)
    {
        // Try find a GameObject with the name "Chunks"
        // If it doesn't exist, create it
        // Set _chunkParent to that GameObject
        _chunkParent = GameObject.Find("Chunks");
        if (_chunkParent == null)
        {
            _chunkParent = new GameObject("Chunks");
        }

        _chunkObject = Object.Instantiate(
            Resources.Load<GameObject>("Prefabs/Chunk"), 
//                                                                .z
            new Vector3(coordinates.x * CHUNK_SIZE, 0, coordinates.y * CHUNK_SIZE), 
            Quaternion.identity, 
            _chunkParent.transform);
        _chunkObject.name = coordinates.ToString();
        ChunkPosition = coordinates;

        _chunkObject.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = TextureStitcher.Instance.BlockTexAtlas;

        WorldGenerator.Instance.AddFunctionToThread(ChunkGenThread);
    }

    public Chunk(int x, int z)
        : this(new Vector2Int(x, z)) {}

    private void ChunkGenThread()
    {
        GenerateChunk();
        GenerateMesh();
        WorldGenerator.Instance.ExecuteOnMainThread(UpdateMesh);
    }

    private void GenerateChunk()
    {
        var random = new System.Random();

        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                for (int i = 0; i < 64; i++)
                {
                    if (random.Next(0, 2) < 1)
                    {
                        _blocks.Add(new Vector3(x, i, y), new b_Bedrock(x, i, y));
                    }
                    else
                    {
                        _blocks.Add(new Vector3(x, i, y), new b_WireframeBlock(x, i, y));
                    }
                }
            }
        }
    }

    private void GenerateMesh()
    {
        foreach (var block in _blocks)
        {
            // If there is a block next to the current block, 
            // don't add a face
            // Otherwise add a face
            if (block.Value is SolidBlock)
            {
                if (!_blocks.ContainsKey(block.Key + new Vector3(1, 0, 0)) || !(_blocks[block.Key + new Vector3(1, 0, 0)] is SolidBlock))
                {
                    AddFace(
                        block.Key + new Vector3(0.5f,  0.5f, -0.5f),
                        block.Key + new Vector3(0.5f,  0.5f,  0.5f),
                        block.Key + new Vector3(0.5f, -0.5f, -0.5f),
                        block.Key + new Vector3(0.5f, -0.5f,  0.5f),
                        (SolidBlock)block.Value);
                }
                if (!_blocks.ContainsKey(block.Key + new Vector3(-1, 0, 0)) || !(_blocks[block.Key + new Vector3(-1, 0, 0)] is SolidBlock))
                {
                    AddFace(
                        block.Key + new Vector3(-0.5f,  0.5f,  0.5f),
                        block.Key + new Vector3(-0.5f,  0.5f, -0.5f),
                        block.Key + new Vector3(-0.5f, -0.5f,  0.5f),
                        block.Key + new Vector3(-0.5f, -0.5f, -0.5f),
                        (SolidBlock)block.Value);
                }
                if (!_blocks.ContainsKey(block.Key + new Vector3(0, 0, 1)) || !(_blocks[block.Key + new Vector3(0, 0, 1)] is SolidBlock))
                {
                    AddFace(
                        block.Key + new Vector3( 0.5f,  0.5f,  0.5f),
                        block.Key + new Vector3(-0.5f,  0.5f,  0.5f),
                        block.Key + new Vector3( 0.5f, -0.5f,  0.5f),
                        block.Key + new Vector3(-0.5f, -0.5f,  0.5f),
                        (SolidBlock)block.Value);
                }
                if (!_blocks.ContainsKey(block.Key + new Vector3(0, 0, -1)) || !(_blocks[block.Key + new Vector3(0, 0, -1)] is SolidBlock))
                {
                    AddFace(
                        block.Key + new Vector3(-0.5f,  0.5f, -0.5f),
                        block.Key + new Vector3( 0.5f,  0.5f, -0.5f),
                        block.Key + new Vector3(-0.5f, -0.5f, -0.5f),
                        block.Key + new Vector3( 0.5f, -0.5f, -0.5f),
                        (SolidBlock)block.Value);
                }
                if (!_blocks.ContainsKey(block.Key + new Vector3(0, 1, 0)) || !(_blocks[block.Key + new Vector3(0, 1, 0)] is SolidBlock))
                {
                    AddFace(
                        block.Key + new Vector3( 0.5f,  0.5f, -0.5f),
                        block.Key + new Vector3(-0.5f,  0.5f, -0.5f),
                        block.Key + new Vector3( 0.5f,  0.5f,  0.5f),
                        block.Key + new Vector3(-0.5f,  0.5f,  0.5f),
                        (SolidBlock)block.Value);
                }
                if (!_blocks.ContainsKey(block.Key + new Vector3(0, -1, 0)) || !(_blocks[block.Key + new Vector3(0, -1, 0)] is SolidBlock))
                {
                    AddFace(
                        block.Key + new Vector3( 0.5f, -0.5f,  0.5f),
                        block.Key + new Vector3(-0.5f, -0.5f,  0.5f),
                        block.Key + new Vector3( 0.5f, -0.5f, -0.5f),
                        block.Key + new Vector3(-0.5f, -0.5f, -0.5f),
                        (SolidBlock)block.Value);
                }
            }
        }
    }

    private void AddFace(Vector3 topLeft, Vector3 topRight, Vector3 bottomLeft, Vector3 bottomRight, SolidBlock block)
    {
        _vertices.Add(topLeft);
        _vertices.Add(topRight);
        _vertices.Add(bottomLeft);
        _vertices.Add(bottomRight);

        var verticesLength = _vertices.Count - 1;

        _triangles.Add(verticesLength - 1);
        _triangles.Add(verticesLength - 3);
        _triangles.Add(verticesLength - 2);
        _triangles.Add(verticesLength - 1);
        _triangles.Add(verticesLength - 2);
        _triangles.Add(verticesLength);

        var blockUV = block.Uv;

        _uvs.Add(blockUV.topLeft);
        _uvs.Add(blockUV.topRight);
        _uvs.Add(blockUV.bottomLeft);
        _uvs.Add(blockUV.bottomRight);
    }

    private void UpdateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = _vertices.ToArray();
        mesh.triangles = _triangles.ToArray();
        mesh.uv = _uvs.ToArray();
        mesh.RecalculateNormals();
        _chunkObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        _chunkObject.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
