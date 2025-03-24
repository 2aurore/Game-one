using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public class Constant
    {


        public static Color GetItemGradeColor(int grade)
        {
            switch (grade)
            {
                case 0:
                    return new Color(0.5f, 0.5f, 0.5f);
                case 1:
                    return new Color(1f, 1f, 1f);
                case 2:
                    return new Color(0f, 0.5f, 0f);
                case 3:
                    return new Color(0f, 0f, 1f);
                case 4:
                    return new Color(0.5f, 0f, 0.5f);
                case 5:
                    return new Color(1f, 0.5f, 0f);
                case 6:
                    return new Color(1f, 0f, 0f);
                default:
                    return new Color(0.5f, 0.5f, 0.5f);
            }
        }
    }
}
