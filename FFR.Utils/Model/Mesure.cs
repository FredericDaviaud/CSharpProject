namespace FFR.Parser
{
    class Mesure
    {
        public int id { get; private set; }
        public int size { get; private set; }

        public Mesure(int id, int size)
        {
            this.id = id;
            this.size = size;
        }
    }
}
