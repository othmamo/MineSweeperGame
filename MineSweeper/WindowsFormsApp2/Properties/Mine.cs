namespace demineur
{
    public class Mine
    {
        private int posX;
        private int posY;
        private int sign = -1;
        
        public Mine(int pX, int pY)
        {
            posX = pX;
            posY = pY;
        }

        public int GetPosX()
        {
            return posX;
        }
        
        public int GetPosY()
        {
            return posY;
        }

        public int GetSize()
        {
            return sign;
        }
    }
}