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

    public SolidBlock(Vector3UInt position) : base(position)
    {
    }

    public SolidBlock(uint x, uint y, uint z) : base(x, y, z)
    {
    }

    public TextureUV Uv { get { return TextureStitcher.Instance.GetTextureUV(Texture); } }
}