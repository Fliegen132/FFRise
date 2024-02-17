using UnityEngine;

namespace GurpsSystem
{
    public class Dice : IDice
    {
        public int RollDice(int cubeCount, int ability)
        {
            int result = 0;
            for (int i = 0; i < cubeCount; i++)
            {
                result += Random.Range(1, 7); //от 1 до 7-1
                
            }
            result += ability;
            return result;
        }
    }
}