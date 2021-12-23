// 
// Copyright (c) SunnyMonster
//

using UnityEngine;

public class b_WireframeBlock : SolidBlock
{
    public b_WireframeBlock(Vector3 position) : base(position)
    {
    }

    public b_WireframeBlock(float x, float y, float z) : base(x, y, z)
    {
    }

    protected override string Texture
    {
        get { return "mcue'wireframe_block"; }
    }
}
