namespace EightPuzzleSolverClassLibrary
{
    public class Arrays
    {
        public static int[] StartArray { get; set; } = new int[9] { 1, 2, 3, 8, 0, 4, 7, 6, 5 };
        public static int[] GoalArray { get; set; } = new int[9] { 1, 2, 3, 8, 0, 4, 7, 6, 5 };

        public static void Assign(int[] startArray, int[] goalArray)
        {
            startArray.CopyTo(StartArray, 0);
            goalArray.CopyTo(GoalArray, 0);
        }
    }
}
