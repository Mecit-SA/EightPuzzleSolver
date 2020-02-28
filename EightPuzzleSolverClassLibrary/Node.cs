using System.Linq;

namespace EightPuzzleSolverClassLibrary
{
    public class Node
    {
        public Node Parent { get; set; }

        public int[] Array { get; set; } = new int[9];


        public int G { get; set; }

        public int H {

            get
            {
                int counter = 0;

                for (int i = 0; i < 9; i++)
                {
                    if (Array[i] == 0 || Array[i] == Arrays.GoalArray[i])
                    {
                        continue;
                    }

                    counter++;
                }

                return counter;
            }
                
        }

        public int F {

            get
            {
                return G + H;
            } 
        
        }


        public Node()
        {

        }
        public Node(int[] array, int g)
        {
            array.CopyTo(Array, 0);
            G = g;
        }


        public bool HasNeighbor(Direction direction)
        {
            int spaceIndex = Array.ToList().IndexOf(0);

            switch (direction)
            {
                case Direction.TOP:
                    return spaceIndex - 3 > -1;

                case Direction.DOWN:
                    return spaceIndex + 3 < 9;

                case Direction.RIGHT:
                    return spaceIndex % 3 != 2;

                case Direction.LEFT:
                    return spaceIndex % 3 != 0;
            }

            return false;
        }

        public Node GetNeighbor(Direction direction)
        {
            int spaceIndex = Array.ToList().IndexOf(0);

            Node node = new Node(Array, G + 1);

            switch (direction)
            {
                case Direction.TOP:
                    node.Switch(spaceIndex, spaceIndex - 3);
                    break;

                case Direction.DOWN:
                    node.Switch(spaceIndex, spaceIndex + 3);
                    break;

                case Direction.RIGHT:
                    node.Switch(spaceIndex, spaceIndex + 1);
                    break;

                case Direction.LEFT:
                    node.Switch(spaceIndex, spaceIndex - 1);
                    break;
            }

            return node;
        }

        void Switch(int index1, int index2)
        {
            int temp = Array[index1];
            Array[index1] = Array[index2];
            Array[index2] = temp;
        }
    }
}
