﻿namespace DataLayer.Utils.Paginations
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

        public bool OrderByDesc { get; set; }
        public string? Query { get; set; }

        public static int GetQyt(int qyt)
        {
            return qyt == 0 ? 10 : qyt;
        }

    }
}
