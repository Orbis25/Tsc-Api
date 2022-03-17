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

        /// <summary>
        /// Order by
        /// </summary>
        public string? OrderBy { get; set; } = null;

        /// <summary>
        /// flag order decs or asc
        /// </summary>
        public bool OrderByDesc { get; set; }

        /// <summary>
        /// param to filter
        /// </summary>
        public string? Query { get; set; }
    }
}
