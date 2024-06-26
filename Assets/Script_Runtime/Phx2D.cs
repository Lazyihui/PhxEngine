using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhxEngine2D {
    public class Phx2D {

        SortedList<int, RBEntity> all;

        public Vector2 gravity;

        HashSet<ulong> intersectedSet;

        public Action<RBEntity, RBEntity> OnTriggerEnterHandle;//交叉开始

        public Action<RBEntity, RBEntity> OnTriggerStayHandle;//交叉持续

        public Action<RBEntity, RBEntity> OnTriggerExitHandle;//交叉结束

        public Phx2D() {
            gravity = new Vector2(0, -9.8f);
            intersectedSet = new HashSet<ulong>();
            all = new SortedList<int, RBEntity>();
        }

        public void Tick(float dt) {
            // 1.引力应用
            foreach (RBEntity rb in all.Values) {
                if (rb.isStatic) {
                    //  静态是不受引力影响的
                    continue;
                }
                // 速度发生变化
                rb.velocity += gravity * rb.gravityScale * dt;
            }
            // 2.根据速度更新位置
            foreach (RBEntity rb in all.Values) {
                // 坐标发生变化
                rb.position += rb.velocity * dt;
            }
            // 3.交叉检测
            // aabb（中心对称，无旋转的包围盒）  obb（带旋转的包围盒） Circle  Convex凸多边形
            for (int i = 0; i < all.Count; i += 1) {
                RBEntity a = all.Values[i];
                // 和所有的刚体进行交叉检测
                for (int j = i + 1; j < all.Count; j += 1) {
                    RBEntity b = all.Values[j];
                    // aabb & aabb
                    Intersect_RB_RB(a, b);
                }
            }
            // 4、交叉检测事件触发

            // // 5.穿透恢复
            for (int i = 0; i < all.Count; i += 1) {
                RBEntity a = all.Values[i];
                for (int j = i + 1; j < all.Count; j += 1) {
                    RBEntity b = all.Values[j];
                    ulong key = GetCombineKey(a.id, b.id);
                    if (a.shapeType == ShapeType.Circle && b.shapeType == ShapeType.Circle) {

                        if (intersectedSet.Contains(key)) {
                            // 穿透恢复
                            Restoration.RestorePenetration_Circle_Cilcle(a, b);
                        }
                    }
                }


            }
            // 6.穿透恢复事件触发

        }

        // 添加刚体
        public RBEntity Add(int id, ShapeType shapeType, Vector2 size) {
            RBEntity rb = new RBEntity();
            rb.id = id;
            rb.shapeType = shapeType;
            rb.size = size;
            all.Add(id, rb);
            return rb;
        }
        // 移除刚体

        // 查找刚体
        // RB &Rb 检测
        void Intersect_RB_RB(RBEntity a, RBEntity b) {
            if (a.shapeType == ShapeType.Circle && b.shapeType == ShapeType.Circle) {
                // 圆和圆
                bool isIntersected = IntersectionUtil.IsIntersected_Circle_Circle(a.position, a.size.x, b.position, b.size.x, out _);
                if (isIntersected) {
                    // 本次交叉了
                    a.isIntersected = true;
                    b.isIntersected = true;
                    // 软和软才需要 触发Trigger
                    // 触发事件
                    ulong key = GetCombineKey(a.id, b.id);
                    if (intersectedSet.Contains(key)) {
                        // 交叉持续
                        if (a.isTrigger || b.isTrigger) {
                            OnTriggerStayHandle.Invoke(a, b);
                        }
                    } else {
                        // 交叉开始
                        intersectedSet.Add(key);

                        if (a.isStatic || b.isStatic) {
                            OnTriggerEnterHandle.Invoke(a, b);
                        }
                    
                    }
                    // 如果上次交叉了触发stay，否则触发enter

                } else {
                    // 本次没有交叉
                    // 如果上次交叉了触发exit
                    ulong key = GetCombineKey(a.id, b.id);
                    if (intersectedSet.Contains(key)) {
                        // 交叉结束
                        intersectedSet.Remove(key);
                        if (a.isStatic || b.isStatic) {

                            OnTriggerExitHandle.Invoke(a, b);
                        }
                    }

                    a.isIntersected = false;
                    b.isIntersected = false;
                }
            } else if (a.shapeType == ShapeType.Square && b.shapeType == ShapeType.Square) {
                // 
            } else if (a.shapeType == ShapeType.Circle && b.shapeType == ShapeType.Square) {
                // 
            } else if (a.shapeType == ShapeType.Square && b.shapeType == ShapeType.Circle) {
                // 
            } else {
                Debug.LogError("未知的形状类型");
            }
        }
        // 两个数合成一个数
        ulong GetCombineKey(int a, int b) {
            uint a_ = (uint)a;
            uint b_ = (uint)b;
            uint min = Math.Min(a_, b_);
            uint max = Math.Max(a_, b_);
            return (ulong)(min << 32) | max;

        }
    }
}
