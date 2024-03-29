using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhxEngine2D {
    public class RBEntity {
        // ID
        public int id;
        // 形状
        public ShapeType shapeType;
        public Vector2 size;
        // 速度

        public Vector2 velocity;
        // 坐标
        public Vector2 position;
        // 面向
        public float rotation;
        // 引力倍数
        public float gravityScale;
        // 是否静态
        public bool isStatic;
        // 是否交叉
        public bool isIntersected;

        public bool isTrigger;

        public RBEntity() {

        }

#if UNITY_EDITOR
        public void DrawGizmos() {
            Gizmos.color = Color.green;
            if (isIntersected) {
                Gizmos.color = Color.red;
            }
            if (shapeType == ShapeType.Square) {
                Vector2 halfSize = size * 0.5f;
                Vector2 a = new Vector2(halfSize.x, halfSize.y);
                Vector2 b = new Vector2(-halfSize.x, halfSize.y);
                Vector2 c = new Vector2(-halfSize.x, -halfSize.y);
                Vector2 d = new Vector2(halfSize.x, -halfSize.y);
                a = Rotate(a, rotation) + position;
                b = Rotate(b, rotation) + position;
                c = Rotate(c, rotation) + position;
                d = Rotate(d, rotation) + position;
                Gizmos.DrawLine(a, b);
                Gizmos.DrawLine(b, c);
                Gizmos.DrawLine(c, d);
                Gizmos.DrawLine(d, a);
            } else if (shapeType == ShapeType.Circle) {
                Gizmos.DrawWireSphere(position, size.x);
            }
        }

        Vector2 Rotate(Vector2 v, float angle) {
            float rad = angle * Mathf.Deg2Rad;
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);
            return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
        }
#endif

        // #if UNITY_EDITOR

        //         public void DrawGizmos() {
        //             // Gazmos.color = Color.red;
        //             if (shapeType == ShapeType.Square) {
        //                 Vector2 halfSize = size * 0.5f;
        //                 Vector2 a = new Vector2(-halfSize.x, -halfSize.y);
        //                 Vector2 b = new Vector2(halfSize.x, -halfSize.y);
        //                 Vector2 c = new Vector2(halfSize.x, halfSize.y);
        //                 Vector2 d = new Vector2(-halfSize.x, halfSize.y);
        //                 a = Rotate(a, rotation) + position;
        //                 b = Rotate(b, rotation) + position;
        //                 c = Rotate(c, rotation) + position;
        //                 d = Rotate(d, rotation) + position;
        //                 Gizmos.DrawLine(a, b);
        //                 Gizmos.DrawLine(b, c);
        //                 Gizmos.DrawLine(c, d);
        //                 Gizmos.DrawLine(d, a);

        //             } else if (shapeType == ShapeType.Circle) {
        //                 Gizmos.DrawWireSphere(position, size.x);
        //             }

        //             Vector2 Rotate(Vector2 v, float angle) {
        //                 float red = angle * Mathf.Deg2Rad;
        //                 float sin = Mathf.Sin(red);
        //                 float cos = Mathf.Cos(red);
        //                 return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
        //             }
        // #endif

    }
}