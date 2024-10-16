namespace Debugging.Functions
{
    public static class PureCalculations
    {
        public static int CalculateSomethingCool(int x, int y, int z)
        {
            var innerCalculation = (int a, int b) => (int c) =>
            {
                var d = a + b;
                throw new Exception("lol wat");
                return d * c;
            };

            var applied = innerCalculation(x, y);

            return applied(z);
        }
    }
}
