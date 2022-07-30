/*
 *   Copyright (c) 2022 ItsSunnyMonster
 *   All rights reserved.

 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at

 *   http://www.apache.org/licenses/LICENSE-2.0

 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 */

using UnityEngine;

public struct Vector3UInt
{
    public uint x;
    public uint y;
    public uint z;

    public Vector3UInt(uint x, uint y, uint z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
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

    public static bool operator ==(Vector3UInt a, Vector3UInt b)
    {
        return a.x == b.x && a.y == b.y && a.z == b.z;
    }

    public static bool operator !=(Vector3UInt a, Vector3UInt b)
    {
        return !(a == b);
    }

    public static Vector3UInt operator +(Vector3UInt a, Vector3UInt b)
    {
        return new Vector3UInt(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vector3UInt operator -(Vector3UInt a, Vector3UInt b)
    {
        return new Vector3UInt(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static Vector3UInt operator *(Vector3UInt a, Vector3UInt b)
    {
        return new Vector3UInt(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    public static Vector3UInt operator /(Vector3UInt a, Vector3UInt b)
    {
        return new Vector3UInt(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    public static Vector3UInt operator *(Vector3UInt a, uint b)
    {
        return new Vector3UInt(a.x * b, a.y * b, a.z * b);
    }

    public static Vector3UInt operator /(Vector3UInt a, uint b)
    {
        return new Vector3UInt(a.x / b, a.y / b, a.z / b);
    }

    public static Vector3UInt operator *(uint a, Vector3UInt b)
    {
        return new Vector3UInt(a * b.x, a * b.y, a * b.z);
    }

    public static Vector3UInt operator /(uint a, Vector3UInt b)
    {
        return new Vector3UInt(a / b.x, a / b.y, a / b.z);
    }

    public static Vector3 operator +(Vector3 a, Vector3UInt b)
    {
        return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vector3 operator -(Vector3 a, Vector3UInt b)
    {
        return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static Vector3 operator *(Vector3 a, Vector3UInt b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    public static Vector3 operator /(Vector3 a, Vector3UInt b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    public static Vector3 operator +(Vector3UInt a, Vector3 b)
    {
        return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vector3 operator -(Vector3UInt a, Vector3 b)
    {
        return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static Vector3 operator *(Vector3UInt a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    public static Vector3 operator /(Vector3UInt a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }
}