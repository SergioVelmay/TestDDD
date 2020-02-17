using System;
namespace MongoDB.Driver
{
    /// <summary>
    /// Builder extensions.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Gets the base filter.
        /// </summary>
        /// <returns>The base filter.</returns>
        /// <param name="builder">Builder.</param>
        /// <param name="tableName">Collection name.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static FilterDefinition<T> GetBaseFilter<T>(this FilterDefinitionBuilder<T> builder, String tableName)
        {
            return builder.Eq("TableName", tableName);
        }

        /// <summary>
        /// Append the specified filter and newFilter.
        /// </summary>
        /// <returns>The append.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="newFilter">New filter.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static FilterDefinition<T> Append<T>(this FilterDefinition<T> filter, FilterDefinition<T> newFilter)
        {
            return filter & newFilter;
        }
    }

}
