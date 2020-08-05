using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SapCo2.Helper;
using SapCo2.Wrapper.Enumeration;

namespace SapCo2.Query
{
    public class QueryOperator:IDisposable
    {
        #region Variables

        private bool _disposed;
        // ReSharper disable once InconsistentNaming
        private static bool _isEmpty;
        // ReSharper disable once InconsistentNaming
        private static bool _listFilled;
        private static readonly CultureInfo UsCultureInfo = new CultureInfo("en-US");

        #endregion

        #region Properties

        private static string Query { get; set; } = string.Empty;
        private static List<string> QueryList { get; set; } = new List<string>();

        public bool ListFilled
        {
            get => _listFilled;
            set => _listFilled = value;
        }

        public bool IsEmpty
        {
            get => _isEmpty;
            set => _isEmpty = value;
        }

        #endregion

        #region Methods

        public string GetQuery()
        {
            return Query;
        }

        public List<string> GetQueryList()
        {
            return QueryList;
        }


        #region Operator Methods

        public static QueryOperator Equal(string field, object value, RfcDataTypes dataType = RfcDataTypes.String)
        {
            Query = $"{field} EQ '{TypeConversionHelper.ConvertToRfcType(value, dataType)}'";
            return new QueryOperator();
        }

        public static QueryOperator NotEqual(string field, object value, RfcDataTypes dataType = RfcDataTypes.String)
        {
            Query = $"{field} NE '{TypeConversionHelper.ConvertToRfcType(value, dataType)}'";
            return new QueryOperator();
        }

        public static QueryOperator GreaterThan(string field, object value, RfcDataTypes dataType = RfcDataTypes.String)
        {
            Query = $"{field} GT '{TypeConversionHelper.ConvertToRfcType(value, dataType)}'";
            return new QueryOperator();
        }

        public static QueryOperator GreaterThanOrEqual(string field, object value, RfcDataTypes dataType = RfcDataTypes.String)
        {
            Query = $"{field} GE '{TypeConversionHelper.ConvertToRfcType(value, dataType)}'";
            return new QueryOperator();
        }

        public static QueryOperator LessThan(string field, object value, RfcDataTypes dataType = RfcDataTypes.String)
        {
            Query = $"{field} LT '{TypeConversionHelper.ConvertToRfcType(value, dataType)}'";
            return new QueryOperator();
        }

        public static QueryOperator LessThanOrEqual(string field, object value, RfcDataTypes dataType = RfcDataTypes.String)
        {
            Query = $"{field} LE '{TypeConversionHelper.ConvertToRfcType(value, dataType)}'";
            return new QueryOperator();
        }

        public static QueryOperator StartsWith(string field, object value)
        {
            Query = $"{field} LIKE '{value}%'";
            return new QueryOperator();
        }

        public static QueryOperator EndsWith(string field, object value)
        {
            Query = $"{field} LIKE '%{value}'";
            return new QueryOperator();
        }

        public static QueryOperator Contains(string field, object value)
        {
            Query = $"{field} LIKE '%{value}%'";
            return new QueryOperator();
        }

        public static QueryOperator NotContains(string field, object value)
        {
            Query = $"{field} NOT LIKE '%{value}%'";
            return new QueryOperator();
        }

        public static QueryOperator Between(string field, object firstValue, object lastValue, RfcDataTypes dataType = RfcDataTypes.String)
        {
            Query =
                $"{field} BETWEEN '{TypeConversionHelper.ConvertToRfcType(firstValue, dataType)}' AND '{TypeConversionHelper.ConvertToRfcType(lastValue, dataType)}'";
            return new QueryOperator();
        }

        public static QueryOperator In(string field, List<object> valueList, RfcDataTypes dataType = RfcDataTypes.String)
        {
            List<string> queryList = new List<string>();
            if (valueList.Any())
            {
                for (int no = 0; no <= valueList.Count - 1; no++)
                {
                    if (no == 0)
                    {
                        queryList.Add(valueList.Count == 1
                            ? string.Format("{1} IN ('{0}')", TypeConversionHelper.ConvertToRfcType(valueList[no], dataType), field.ToUpper(UsCultureInfo))
                            : string.Format("{1} IN ('{0}'", TypeConversionHelper.ConvertToRfcType(valueList[no], dataType), field.ToUpper(UsCultureInfo)));
                    }
                    else if (no == valueList.Count - 1)
                        queryList.Add($",'{TypeConversionHelper.ConvertToRfcType(valueList[no], dataType)}') ");
                    else
                        queryList.Add($",'{TypeConversionHelper.ConvertToRfcType(valueList[no], dataType)}'");
                }

                QueryList = queryList;
                _listFilled = true;
                _isEmpty = false;
            }
            else
            {
                _isEmpty = true;
            }

            return new QueryOperator();
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Query = string.Empty;
                QueryList.Clear();
            }

            _disposed = true;
        }

        #endregion

        #endregion
    }
}