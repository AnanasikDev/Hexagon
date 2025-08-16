using System.Collections.Generic;
using UnityEngine;

namespace Hexagon
{
    public static partial class HexTransform
    {
        /// <summary>
        /// Iterates over all transforms attached to this transform as direct children. For recursive search see GetChildrenRecursive
        /// </summary>
        public static List<Transform> GetChildren(this Transform transform)
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform child in transform)
            {
                children.Add(child);
            }
            return children;
        }

        /// <summary>
        /// Recursively iterates over all transforms nested to this transform
        /// </summary>
        public static List<Transform> GetChildrenRecursive(this Transform transform)
        {
            List<Transform> children = new List<Transform>();

            void inner(Transform transform)
            {
                foreach (Transform child in transform)
                {
                    children.Add(child);

                    if (child.childCount > 0) inner(child);
                }
            }

            inner(transform);

            return children;
        }

        /// <summary>
        /// Rotates the transform to face the given position on the XY plane (Z os the given position is ignored and replaced with <c>transform.position.z</c>.
        /// </summary>
        /// <param name="transform">Transform to rotate.</param>
        /// <param name="position">Target position on the XY plane.</param>
        public static void LookAtXY(this Transform transform, Vector2 position)
        {
            transform.LookAt(position.WithZ3D(transform.position.z));
        }
    }
}