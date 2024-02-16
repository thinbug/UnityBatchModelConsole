using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ϸ���ݿ�ӿ�
public class DBMain : MonoBehaviour
{
    public static DBMain inst;
    SQLiteHelper sql;
    private void Awake()
    {
        inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //��ƽ̨�����ݿ�洢�ľ���·��(ͨ��)
        //PC��sql = new SQLiteHelper("data source=" + Application.dataPath + "/sqlite4unity.db");
        //Mac��sql = new SQLiteHelper("data source=" + Application.dataPath + "/sqlite4unity.db");
        //Android��sql = new SQLiteHelper("URI=file:" + Application.persistentDataPath + "/sqlite4unity.db");
        //iOS��sql = new SQLiteHelper("data source=" + Application.persistentDataPath + "/sqlite4unity.db");

        //PCƽ̨�µ����·��
        //sql = new SQLiteHelper("data source="sqlite4unity.db");
        //�༭����Assets/sqlite4unity.db
        //����󣺺�AppName.exeͬ�����ļ����£�������^����
        //��Ȼ�ܹ��ø�����ķ�ʽsql = new SQLiteHelper("data source="D://SQLite//sqlite4unity.db");
        //ȷ��·�����ھͿ��Է���������

        //���������ȴ�����һ�����ݿ�
        //�ܹ���������ݿ������StreamingAssets�ļ�����Ȼ���ٸ��Ƶ�
        //Application.persistentDataPath + "/sqlite4unity.db"·���Ϳ���

        //������Ϊsqlite4unity�����ݿ�
        sql = new SQLiteHelper("data source=" + Application.dataPath + "/game.db");

        ////������Ϊtable1�����ݱ�
        //sql.CreateTable("table1", new string[] { "ID", "Name", "Age", "Email" }, new string[] { "INTEGER", "TEXT", "INTEGER", "TEXT" });

        ////������������
        //sql.InsertValues("table1", new string[] { "'1'", "'����'", "'22'", "'Zhang3@163.com'" });
        //sql.InsertValues("table1", new string[] { "'2'", "'����'", "'25'", "'Li4@163.com'" });

        ////�������ݡ���Name="����"�ļ�¼�е�Name��Ϊ"Zhang3"
        //sql.UpdateValues("table1", new string[] { "Name" }, new string[] { "'Zhang3'" }, "Name", "=", "'����'");

        ////����3������
        //sql.InsertValues("table1", new string[] { "3", "'����'", "25", "'Wang5@163.com'" });
        //sql.InsertValues("table1", new string[] { "4", "'����'", "26", "'Wang5@163.com'" });
        //sql.InsertValues("table1", new string[] { "5", "'����'", "27", "'Wang5@163.com'" });

        ////ɾ��Name="����"��Age=26�ļ�¼,DeleteValuesOR��������
        //sql.DeleteValuesAND("table1", new string[] { "Name", "Age" }, new string[] { "=", "=" }, new string[] { "'����'", "'26'" });

        ////��ȡ���ű�
        //SqliteDataReader reader = sql.ReadFullTable("table1");
        //while (reader.Read())
        //{
        //    //��ȡID
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
        //    //��ȡName
        //    Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
        //    //��ȡAge
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("Age")));
        //    //��ȡEmail
        //    Debug.Log(reader.GetString(reader.GetOrdinal("Email")));
        //}

        ////��ȡ���ݱ���Age>=25��ȫ����¼��ID��Name
        //reader = sql.ReadTable("table1", new string[] { "ID", "Name" }, new string[] { "Age" }, new string[] { ">=" }, new string[] { "'25'" });
        //while (reader.Read())
        //{
        //    //��ȡID
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
        //    //��ȡName
        //    Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
        //}

        ////�Լ�����SQL,ɾ�����ݱ���ȫ��Name="����"�ļ�¼
        //sql.ExecuteQuery("DELETE FROM table1 WHERE NAME='����'");

        ////�ر����ݿ�����
        //sql.CloseConnection();
    }

    private void OnDestroy()
    {
        sql.CloseConnection();
    }

    public bool Login(string username,string pwd)
    {
        SqliteDataReader reader;
        //��ȡ���ݱ���Age>=25��ȫ����¼��ID��Name
        reader = sql.ReadTable("TAccount", new string[] { "accountID", "pwd" }, new string[] { "accountID" }, new string[] { "=" }, new string[] { "'"+ username + "'" });

        bool had = reader.Read();
        if (had)
        {
            //��ȡID
            string dbid = reader.GetString(reader.GetOrdinal("accountID"));
            //��ȡName
            string dbpwd = reader.GetString(reader.GetOrdinal("pwd"));

            if (dbpwd != pwd)
                return false;
        }
        else
        {
            int insertno = sql.Insert("TAccount", new string[] { "accountID", "pwd" }, new string[] { "'" + username+ "'" , "'" + pwd+"'" });
            if (insertno < 1)
                return false;
        }
        return true;
    }
}
