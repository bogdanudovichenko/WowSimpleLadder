using System;
using System.Collections.Generic;

namespace WowSimpleLadder.Web.Helpers
{
    public static class QueryStringParser
    {
        public static IDictionary<string, string> Parse(string queryString)
        {
            var paramsDictionary = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(queryString))
            {
                return paramsDictionary;
            }

            int querySymbolPosition = queryString.IndexOf("?", StringComparison.Ordinal);

            if (querySymbolPosition == -1)
            {
                return paramsDictionary;
            }

            queryString = queryString.Substring(querySymbolPosition + 1);

            if (queryString.Contains("&"))
            {
                string[] parametrs = queryString.Split('&');
                AddParamsToDictionary(parametrs, paramsDictionary);
            }
            else
            {
                AddParamToDictionary(queryString, paramsDictionary);
            }

            return paramsDictionary;
        }

        private static void AddParamsToDictionary(IEnumerable<string> parametrs, IDictionary<string, string> dictionary)
        {
            if (parametrs == null || dictionary == null)
            {
               return;
            }

            foreach (string param in parametrs)
            {
                AddParamToDictionary(param, dictionary);
            }
        }

        private static void AddParamToDictionary(string param, IDictionary<string, string> dictionary)
        {
            if (string.IsNullOrEmpty(param) || dictionary == null)
            {
                return;
            }

            string[] paramArr = param.Split('=');

            if (paramArr.Length != 2)
            {
                return;
            }

            string paramName = paramArr[0].ToLower();
            string paramValue = paramArr[1];

            if (string.IsNullOrEmpty(paramName) || string.IsNullOrEmpty(paramValue) || dictionary.ContainsKey(paramName))
            {
                return;
            }

            dictionary.Add(paramName, paramValue);
        }
    }
}