using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxtail.ProjectFears
{
    public enum RectBorder
    {
        None,
        Left,
        Top, 
        Right, 
        Bottom
    }

    public static class RectExtensions
    {
        //Returns the random point considering the object size and the border where the point is selected
        public static Vector2 RandomPointInsideBorders(this Rect rect, float objectHalfWidth, float objetHalfHeight, out RectBorder border, RectBorder exclude = RectBorder.None)
        {
            Vector2 randomPoint = Vector2.zero;
            border = RectBorder.Left;

            float value = Random.value;

            if (value <= 0.25 && exclude != RectBorder.Left) //Left Border
            {
                randomPoint.x = rect.min.x;
                randomPoint.y = Random.Range(rect.min.y + objetHalfHeight, rect.max.y - objetHalfHeight);
                border = RectBorder.Left;
            }
            else if (value <= 0.50f && exclude != RectBorder.Top) //Top Border
            {
                randomPoint.x = Random.Range(rect.min.x + objectHalfWidth, rect.max.x - objectHalfWidth);
                randomPoint.y = rect.max.y;
                border = RectBorder.Top;
            }
            else if (value <= 0.75f && exclude != RectBorder.Right) //Right Border
            {
                randomPoint.x = rect.max.x;
                randomPoint.y = Random.Range(rect.min.y + objetHalfHeight, rect.max.y - objetHalfHeight);
                border = RectBorder.Right;
            }
            else if (value <= 1f && exclude != RectBorder.Bottom) //Bottom Border
            {
                randomPoint.x = Random.Range(rect.min.x + objectHalfWidth, rect.max.x - objectHalfWidth);
                randomPoint.y = rect.min.y;
                border = RectBorder.Bottom;
            }

            return randomPoint;
        }

        public static Vector2 RandomPointInBorders(this Rect rect, out RectBorder border)
        {
            return RandomPointInsideBorders(rect, 0f, 0f, out border);
        }

        public static Vector2 RandomPointInside(this Rect rect)
        {
            Vector2 randomPoint = Vector2.zero;

            randomPoint.x = Random.Range(rect.min.x, rect.max.x);
            randomPoint.y = Random.Range(rect.min.y, rect.max.y);

            return randomPoint;
        }

        public static Vector2 RandomPointAroundPosition(this Rect rect, Vector3 position, float minDistance, float maxDistance)
        {
            Vector2 randomPoint = Vector2.zero;
            Rect newRect = new Rect();
            newRect.xMin = Mathf.Clamp(position.x - Random.Range(minDistance, maxDistance), rect.min.x, rect.max.x);
            newRect.xMax = Mathf.Clamp(position.x + Random.Range(minDistance, maxDistance), rect.min.x, rect.max.x);
            newRect.yMin = Mathf.Clamp(position.y - Random.Range(minDistance, maxDistance), rect.min.y, rect.max.y);
            newRect.yMax = Mathf.Clamp(position.y + Random.Range(minDistance, maxDistance), rect.min.y, rect.max.y);
            
            randomPoint = RandomPointInBorders(newRect, out RectBorder border);

            return randomPoint;
        }
    }
}

