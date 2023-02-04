using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class KeycodeExtensions
    {
        public static char ToChar(this KeyCode code)
        {
            return code.ToString().ToLower().ToCharArray()[0];
        }

        public static bool IsArrowKey(this KeyCode code)
        {
            var arrowKeys = new HashSet<KeyCode>()
                {
                    KeyCode.UpArrow,
                    KeyCode.DownArrow,
                    KeyCode.LeftArrow,
                    KeyCode.RightArrow

                };
            return arrowKeys.Contains(code);
        }

        public static bool IsUtilityKey(this KeyCode code)
        {
            var arrowKeys = new HashSet<KeyCode>()
            {
                KeyCode.Mouse0,
                KeyCode.Mouse1,
                KeyCode.Mouse2,
                KeyCode.Mouse3,
                KeyCode.Mouse4,
                KeyCode.Mouse5,
                KeyCode.Escape

            };
            return arrowKeys.Contains(code);
        }
    }
}