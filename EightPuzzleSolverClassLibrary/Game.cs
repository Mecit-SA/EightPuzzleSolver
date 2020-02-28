using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EightPuzzleSolverClassLibrary
{
    public class Game
    {
        public Node CurrentNode { get; set; } = new Node();

        List<Node> OpenList { get; set; } = new List<Node>();
        List<Node> ClosedList { get; set; } = new List<Node>();

        public Game(int[] startArray, int[] goalArray)
        {
            Arrays.Assign(startArray, goalArray);
            CurrentNode = new Node(Arrays.StartArray, 0);
        }

        void SolveGame()
        {
            
            OpenList.Add(CurrentNode);

            Node node;

            while(true)
            {
                CurrentNode = OpenList.GetLowestF();
                ClosedList.Add(CurrentNode);
                OpenList.Remove(CurrentNode);

                if (CurrentNode.HasNeighbor(Direction.TOP))
                {
                    node = CurrentNode.GetNeighbor(Direction.TOP);

                    if (!ClosedList.HasArray(node.Array))
                    {
                        if (!OpenList.HasArray(node.Array))
                        {
                            OpenList.Add(node);
                            node.Parent = CurrentNode;
                        }
                        else
                        {
                            if(node.G < OpenList.GetNode(node.Array).G)
                            {
                                CurrentNode.Parent = node;
                            }
                        }
                    }
                }

                if (CurrentNode.HasNeighbor(Direction.DOWN))
                {
                    node = CurrentNode.GetNeighbor(Direction.DOWN);

                    if (!ClosedList.HasArray(node.Array))
                    {
                        if (!OpenList.HasArray(node.Array))
                        {
                            OpenList.Add(node);
                            node.Parent = CurrentNode;
                        }
                        else
                        {
                            if (node.G < OpenList.GetNode(node.Array).G)
                            {
                                CurrentNode.Parent = node;
                            }
                        }
                    }
                }

                if (CurrentNode.HasNeighbor(Direction.RIGHT))
                {
                    node = CurrentNode.GetNeighbor(Direction.RIGHT);

                    if (!ClosedList.HasArray(node.Array))
                    {
                        if (!OpenList.HasArray(node.Array))
                        {
                            OpenList.Add(node);
                            node.Parent = CurrentNode;
                        }
                        else
                        {
                            if (node.G < OpenList.GetNode(node.Array).G)
                            {
                                CurrentNode.Parent = node;
                            }
                        }
                    }
                }

                if (CurrentNode.HasNeighbor(Direction.LEFT))
                {
                    node = CurrentNode.GetNeighbor(Direction.LEFT);

                    if (!ClosedList.HasArray(node.Array))
                    {
                        if (!OpenList.HasArray(node.Array))
                        {
                            OpenList.Add(node);
                            node.Parent = CurrentNode;
                        }
                        else
                        {
                            if (node.G < OpenList.GetNode(node.Array).G)
                            {
                                CurrentNode.Parent = node;
                            }
                        }
                    }
                }
                

                if (ClosedList.HasArray(Arrays.GoalArray))
                {
                    break;
                }
            }            
        }

        public async Task Solve()
        {
            if (!IsSolvable())
            {
                throw new Exception("This case can not be solved!\nPlease try another case.");
            }
            await Task.Run(() => SolveGame());
        }

        public List<Node> GetPath()
        {
            List<Node> list = new List<Node>();

            while (CurrentNode != null)
            {
                list.Add(CurrentNode);
                CurrentNode = CurrentNode.Parent;
            }
            list.Reverse();

            return list;
        }

        bool IsSolvable()
        {
            int inversions = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = i + 1; j < 9; j++)
                {

                    for (int k = 0; k < Array.IndexOf(Arrays.GoalArray, Arrays.StartArray[i]); k++)
                    {
                        if (Arrays.GoalArray[k] == Arrays.StartArray[j] && Arrays.StartArray[i] != 0 && Arrays.StartArray[j] != 0)
                            inversions++;
                    }

                }
            }
            if (inversions % 2 == 0) return true;
            return false;
        }
    }
}
