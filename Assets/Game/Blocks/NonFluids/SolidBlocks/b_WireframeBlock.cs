// 
// Copyright 2021 SunnyMonster
//

using UnityEngine;

public class b_WireframeBlock : SolidBlock
{
    public b_WireframeBlock(Vector3UInt position) : base(position)
    {
    }

    public b_WireframeBlock(uint x, uint y, uint z) : base(x, y, z)
    {
    }

    protected override string Texture
    {
        get { return "mcue'wireframe_block"; }
    }
}
