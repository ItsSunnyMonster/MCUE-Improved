//
// Copyright 2021 SunnyMonster
//

using UnityEngine;

public class SolidBlock : Block
{
    protected virtual string Texture 
    {
        get { return "_default"; }
    }

    public SolidBlock(Vector3 position) : base(position)
    {
    }

    public SolidBlock(float x, float y, float z) : base(x, y, z)
    {
    }

    public TextureUV Uv { get { return TextureStitcher.Instance.GetTextureUV(Texture); } }
}