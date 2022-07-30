// 
// Copyright 2021 SunnyMonster
//

using UnityEngine;

public class b_Bedrock : SolidBlock
{
    protected override string Texture { get { return "mcue'bedrock"; } }

    public b_Bedrock(Vector3UInt position) : base(position)
    {
    }

    public b_Bedrock(uint x, uint y, uint z) : base(x, y, z)
    {
    }
}
