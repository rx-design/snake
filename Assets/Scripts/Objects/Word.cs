using Enums;
using UnityEngine;

namespace Objects
{
    [CreateAssetMenu(fileName = "New Word", menuName = "Objects/Word")]
    public class Word : ScriptableObject
    {
        public new string name = "New Word";
        public string hint;

        public char[] chars =
        {
            (char)Letter.A,
            (char)Letter.P,
            (char)Letter.P,
            (char)Letter.L,
            (char)Letter.E
        };
    }
}