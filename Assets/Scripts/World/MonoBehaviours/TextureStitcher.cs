// 
// Copyright (c) SunnyMonster
//

using System.Collections.Generic;
using UnityEngine;

public class TextureStitcher : MonoBehaviour
{
    public static TextureStitcher Instance { get; private set; }

    [SerializeField] private Texture2D _blockTexAtlas;
    public Texture2D BlockTexAtlas { get { return _blockTexAtlas; } private set { _blockTexAtlas = value; } }

    private Dictionary<string, TextureUV> _blockTextures = new Dictionary<string, TextureUV>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.LogWarning("Multiple instances of TextureStitcher found!");
            return;
        }
        else
        {
            Instance = this;
        }

        var allTextures = Resources.LoadAll<Texture2D>("Textures/Blocks");
        var colourArray2D = new Color[allTextures.Length * 16, 16];
        var colourArray = new Color[allTextures.Length * 16 * 16];
        BlockTexAtlas = new Texture2D(allTextures.Length * 16, 16);
        BlockTexAtlas.filterMode = FilterMode.Point;

        for (var i = 0; i < allTextures.Length; i++)
        {
            var texture = allTextures[i];

            Debug.Log($"Stitching texture: {texture.name}");

            if (_blockTextures.ContainsKey(texture.name))
            {
                Debug.LogWarning($"Texture {texture.name} already exists and therefore will not be added to the atlas! \n"
                    + $"Consider naming your texture following the naming convention: mod_id'block_name and don't start your texture name with a underscore (_) as they are reserved for internal use.");
                continue;
            }

            if (texture.width != texture.height)
            {
                Debug.LogWarning($"Texture {texture.name} is not square and therefore is not added!");
                continue;
            }
            if (texture.width != 16 || texture.height != 16)
            {
                Debug.LogWarning($"Texture {texture.name} is not 16x16! and therefore is not added!");
                continue;
            }

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    colourArray2D[x + 16 * i, y] = texture.GetPixel(x, y);
                }
            }

            var uvUnit = 1f / allTextures.GetLength(0);
            var topLeftUV = new Vector2(i * uvUnit, 1);
            _blockTextures.Add(texture.name, new TextureUV(topLeftUV, new Vector2(topLeftUV.x + uvUnit, 1), new Vector2(topLeftUV.x, 0), new Vector2(topLeftUV.x + uvUnit, 0)));
        } 

        var index = 0;
        for (var y = 0; y < colourArray2D.GetLength(1); y++)
        {
            for (var x = 0; x < colourArray2D.GetLength(0); x++)
            {
                colourArray[index] = colourArray2D[x, y];
                index++;
            }
        }
        BlockTexAtlas.SetPixels(colourArray);
        BlockTexAtlas.Apply();
    }

    public TextureUV GetTextureUV(string textureName)
    {
        if (!_blockTextures.ContainsKey(textureName))
        {
            Debug.LogWarning($"Texture {textureName} not found!");
            return _blockTextures["_missing"];
        }
        return _blockTextures[textureName];
    }
}

public struct TextureUV
    {
        public Vector2 topLeft;
        public Vector2 topRight;
        public Vector2 bottomLeft;
        public Vector2 bottomRight;

        public TextureUV(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight)
        {
            this.topLeft = new Vector2(topLeft.x + 0.005f, topLeft.y - 0.005f);
            this.topRight = new Vector2(topRight.x - 0.005f, topRight.y - 0.005f);
            this.bottomLeft = new Vector2(bottomLeft.x + 0.005f, bottomLeft.y + 0.005f);
            this.bottomRight = new Vector2(bottomRight.x - 0.005f, bottomRight.y + 0.005f);
        }
    }
