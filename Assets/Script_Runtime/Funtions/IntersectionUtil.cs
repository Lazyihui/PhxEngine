using UnityEngine;
namespace PhxEngine2D {

    public static class IntersectionUtil {
        // AABB & AABB
        public static bool IntersectionUtil_AABB_AABB(Vector2 amin, Vector2 amax, Vector2 bmin, Vector2 bmax) {
            return amin.x <= bmax.x && amax.x >= bmin.x && amin.y <= bmax.y && amax.y >= bmin.y;
        }
        
    }
}