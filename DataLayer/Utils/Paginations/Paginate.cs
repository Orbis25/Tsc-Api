namespace DataLayer.Utils.Paginations
{
    public class Paginate
    {
        /// <summary>
        /// Actual page
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Quantity by page
        /// </summary>
        public int Qyt { get; set; } = 10;

        public static int GetQyt(int qyt)
        {
            return qyt == 0 ? 10 : qyt;
        }

    }
}
