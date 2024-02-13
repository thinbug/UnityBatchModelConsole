using UnityEngine;

using Mono.Data.Sqlite;
using System;


public class SQLiteHelper
{
    /// <summary>
    /// ���ݿ����Ӷ���
    /// </summary>
    private SqliteConnection dbConnection;

    /// <summary>
    /// SQL�����
    /// </summary>
    private SqliteCommand dbCommand;

    /// <summary>
    /// ���ݶ�ȡ����
    /// </summary>
    private SqliteDataReader dataReader;

    /// <summary>
    /// ���캯��   
    /// </summary>
    /// <param name="connectionString">���ݿ������ַ���</param>
    public SQLiteHelper(string connectionString)
    {
        try
        {
            //�������ݿ�����
            dbConnection = new SqliteConnection(connectionString);
            //�����ݿ�
            dbConnection.Open();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    /// <summary>
    /// ִ��SQL����
    /// </summary>
    /// <returns>The query.</returns>
    /// <param name="queryString">SQL�����ַ���</param>
    public SqliteDataReader ExecuteQuery(string queryString)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = queryString;
        dataReader = dbCommand.ExecuteReader();
        return dataReader;
    }

    /// <summary>
    /// �ر����ݿ�����
    /// </summary>
    public void CloseConnection()
    {
        //����Command
        if (dbCommand != null)
        {
            dbCommand.Cancel();
        }
        dbCommand = null;

        //����Reader
        if (dataReader != null)
        {
            dataReader.Close();
        }
        dataReader = null;

        //����Connection
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
        dbConnection = null;
    }

    /// <summary>
    /// ��ȡ�������ݱ�
    /// </summary>
    /// <returns>The full table.</returns>
    /// <param name="tableName">���ݱ�����</param>
    public SqliteDataReader ReadFullTable(string tableName)
    {
        string queryString = "SELECT * FROM " + tableName;
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// ��ָ�����ݱ��в�������
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">���ݱ�����</param>
    /// <param name="values">�������ֵ</param>
    public SqliteDataReader InsertValues(string tableName, string[] values)
    {
        //��ȡ���ݱ����ֶ���Ŀ
        int fieldCount = ReadFullTable(tableName).FieldCount;
        //����������ݳ��Ȳ������ֶ���Ŀʱ�����쳣
        if (values.Length != fieldCount)
        {
            throw new SqliteException("values.Length!=fieldCount");
        }

        string queryString = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; i++)
        {
            queryString += ", " + values[i];
        }
        queryString += " )";
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// ����ָ�����ݱ��ڵ�����
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">���ݱ�����</param>
    /// <param name="colNames">�ֶ���</param>
    /// <param name="colValues">�ֶ�����Ӧ������</param>
    /// <param name="key">�ؼ���</param>
    /// <param name="value">�ؼ�����Ӧ��ֵ</param>
    public SqliteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string operation, string value)
    {
        //���ֶ����ƺ��ֶ���ֵ����ȷӦʱ�����쳣
        if (colNames.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length");
        }

        string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += ", " + colNames[i] + "=" + colValues[i];
        }
        queryString += " WHERE " + key + operation + value;
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// ɾ��ָ�����ݱ��ڵ�����
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">���ݱ�����</param>
    /// <param name="colNames">�ֶ���</param>
    /// <param name="colValues">�ֶ�����Ӧ������</param>
    public SqliteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //���ֶ����ƺ��ֶ���ֵ����ȷӦʱ�����쳣
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }

        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += "OR " + colNames[i] + operations[0] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// ɾ��ָ�����ݱ��ڵ�����
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">���ݱ�����</param>
    /// <param name="colNames">�ֶ���</param>
    /// <param name="colValues">�ֶ�����Ӧ������</param>
    public SqliteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //���ֶ����ƺ��ֶ���ֵ����ȷӦʱ�����쳣
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }

        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += " AND " + colNames[i] + operations[i] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// �������ݱ�
    /// </summary> +
    /// <returns>The table.</returns>
    /// <param name="tableName">���ݱ���</param>
    /// <param name="colNames">�ֶ���</param>
    /// <param name="colTypes">�ֶ�������</param>
    public SqliteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
    {
        string queryString = "CREATE TABLE " + tableName + "( " + colNames[0] + " " + colTypes[0];
        for (int i = 1; i < colNames.Length; i++)
        {
            queryString += ", " + colNames[i] + " " + colTypes[i];
        }
        queryString += "  ) ";
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// Reads the table.
    /// </summary>
    /// <returns>The table.</returns>
    /// <param name="tableName">Table name.</param>
    /// <param name="items">Items.</param>
    /// <param name="colNames">Col names.</param>
    /// <param name="operations">Operations.</param>
    /// <param name="colValues">Col values.</param>
    public SqliteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
    {
        string queryString = "SELECT " + items[0];
        for (int i = 1; i < items.Length; i++)
        {
            queryString += ", " + items[i];
        }
        queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
        for (int i = 0; i < colNames.Length; i++)
        {
            queryString += " AND " + colNames[i] + " " + operations[i] + " " + colValues[0] + " ";
        }
        return ExecuteQuery(queryString);
    }
}