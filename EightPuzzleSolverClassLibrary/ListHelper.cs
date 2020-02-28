using System.Collections.Generic;
using System.Linq;

namespace EightPuzzleSolverClassLibrary
{
    static class ListHelper
    {
        public static bool HasArray(this List<Node> list, int[] array)
        {
            if(list.Find(n => n.Array.SequenceEqual(array)) != null)
            {
                return true;
            }

            return false;
        }

        public static Node GetLowestF(this List<Node> list)
        {
            return list.OrderBy(n => n.F).First();
        }

        public static Node GetNode(this List<Node> list, int[] array)
        {
            return list.Find(n => n.Array.SequenceEqual(array));
        }
    }
}
