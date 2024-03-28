using UnityEngine;

namespace PhxEngine2D
{
    public class RBEntity
    {
        // ID
        public int id;
        // 形状
        public ShapeType shapeType;
        public Vector2 size;
        // 速度
        public Vector2 velocity;
        // 坐标
        public Vector2 position;
        // 引力倍数
        public float gravityScale;  

        public RBEntity()
        {

        }

    }
}