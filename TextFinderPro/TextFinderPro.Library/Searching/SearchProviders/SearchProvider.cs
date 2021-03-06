﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFinderPro.Library.Searching.SearchProviders.SearchResullt;

namespace TextFinderPro.Library.Searching.SearchProviders
{
    /// <summary>
    /// Provides methods for searching of many types
    /// </summary>
    public abstract class SearchProvider
    {
        protected string _sourceText;
        private SearchProviderType _providerType;
        
        protected SearchProvider(SearchProviderType providerType  , string sourceText)
        {
            _providerType = providerType;
            _sourceText = sourceText.ToLower();
        }

        /// <summary>
        /// Search one pattern in text. Finding all incomings
        /// </summary>
        /// <param name="patterns">Pattern for search</param>
        /// <returns></returns>
        public abstract IEnumerable<FoundText> SearchOne(string pattern);

        /// <summary>
        /// Search many patterns in text. Search stops when found incommings of one pattern
        /// </summary>
        /// <param name="patterns">Patterns for search</param>
        /// <returns></returns>
        public abstract IEnumerable<FoundText> SearchAny(IEnumerable<string> patterns);

        /// <summary>
        /// Search all incomings of all patterns in text
        /// </summary>
        /// <param name="patterns">Patterns for search</param>
        /// <returns></returns>
        public abstract IEnumerable<FoundText> SearchAll(IEnumerable<string> patterns);

        /// <summary>
        /// Search many patterns in text. Search stops when found incommings of one pattern
        /// </summary>
        /// <param name="patterns">Patterns for search</param>
        /// <returns></returns>
        public IEnumerable<FoundText> SearchAny(params string[] patterns) => SearchAny(patterns.AsEnumerable());

        /// <summary>
        /// Search all incomings of all patterns in text
        /// </summary>
        /// <param name="patterns">Patterns for search</param>
        /// <returns></returns>
        public IEnumerable<FoundText> SearchAll(params string[] patterns) => SearchAll(patterns.AsEnumerable());
    }
}
