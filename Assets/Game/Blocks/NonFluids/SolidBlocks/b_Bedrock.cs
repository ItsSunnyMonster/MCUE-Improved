// 
// Copyright (c) SunnyMonster
//

using UnityEngine;

public class b_Bedrock : SolidBlock
{
    protected override string Texture { get { return "mcue'bedrock"; } }

    public b_Bedrock(Vector3 position) : base(position)
    {
    }

    public b_Bedrock(float x, float y, float z) : base(x, y, z)
    {
    }
}
