using UnityEngine;

namespace CodeBase
{
    public static class UnityExtensions
    {
        public static Vector3 AddY(this Vector3 vector3, float floatY) =>
            new Vector3(vector3.x, vector3.y + floatY, vector3.z);
    }
}