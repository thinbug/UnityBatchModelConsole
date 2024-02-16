using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏数据库接口
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
        //各平台下数据库存储的绝对路径(通用)
        //PC：sql = new SQLiteHelper("data source=" + Application.dataPath + "/sqlite4unity.db");
        //Mac：sql = new SQLiteHelper("data source=" + Application.dataPath + "/sqlite4unity.db");
        //Android：sql = new SQLiteHelper("URI=file:" + Application.persistentDataPath + "/sqlite4unity.db");
        //iOS：sql = new SQLiteHelper("data source=" + Application.persistentDataPath + "/sqlite4unity.db");

        //PC平台下的相对路径
        //sql = new SQLiteHelper("data source="sqlite4unity.db");
        //编辑器：Assets/sqlite4unity.db
        //编译后：和AppName.exe同级的文件夹下，这里比較奇葩
        //当然能够用更任意的方式sql = new SQLiteHelper("data source="D://SQLite//sqlite4unity.db");
        //确保路径存在就可以否则会错误发生

        //假设是事先创建了一份数据库
        //能够将这个数据库放置在StreamingAssets文件夹下然后再复制到
        //Application.persistentDataPath + "/sqlite4unity.db"路径就可以

        //创建名为sqlite4unity的数据库
        sql = new SQLiteHelper("data source=" + Application.dataPath + "/game.db");

        ////创建名为table1的数据表
        //sql.CreateTable("table1", new string[] { "ID", "Name", "Age", "Email" }, new string[] { "INTEGER", "TEXT", "INTEGER", "TEXT" });

        ////插入两条数据
        //sql.InsertValues("table1", new string[] { "'1'", "'张三'", "'22'", "'Zhang3@163.com'" });
        //sql.InsertValues("table1", new string[] { "'2'", "'李四'", "'25'", "'Li4@163.com'" });

        ////更新数据。将Name="张三"的记录中的Name改为"Zhang3"
        //sql.UpdateValues("table1", new string[] { "Name" }, new string[] { "'Zhang3'" }, "Name", "=", "'张三'");

        ////插入3条数据
        //sql.InsertValues("table1", new string[] { "3", "'王五'", "25", "'Wang5@163.com'" });
        //sql.InsertValues("table1", new string[] { "4", "'王五'", "26", "'Wang5@163.com'" });
        //sql.InsertValues("table1", new string[] { "5", "'王五'", "27", "'Wang5@163.com'" });

        ////删除Name="王五"且Age=26的记录,DeleteValuesOR方法相似
        //sql.DeleteValuesAND("table1", new string[] { "Name", "Age" }, new string[] { "=", "=" }, new string[] { "'王五'", "'26'" });

        ////读取整张表
        //SqliteDataReader reader = sql.ReadFullTable("table1");
        //while (reader.Read())
        //{
        //    //读取ID
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
        //    //读取Name
        //    Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
        //    //读取Age
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("Age")));
        //    //读取Email
        //    Debug.Log(reader.GetString(reader.GetOrdinal("Email")));
        //}

        ////读取数据表中Age>=25的全部记录的ID和Name
        //reader = sql.ReadTable("table1", new string[] { "ID", "Name" }, new string[] { "Age" }, new string[] { ">=" }, new string[] { "'25'" });
        //while (reader.Read())
        //{
        //    //读取ID
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
        //    //读取Name
        //    Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
        //}

        ////自己定义SQL,删除数据表中全部Name="王五"的记录
        //sql.ExecuteQuery("DELETE FROM table1 WHERE NAME='王五'");

        ////关闭数据库连接
        //sql.CloseConnection();
    }

    private void OnDestroy()
    {
        sql.CloseConnection();
    }

    public bool Login(string username,string pwd)
    {
        SqliteDataReader reader;
        //读取数据表中Age>=25的全部记录的ID和Name
        reader = sql.ReadTable("TAccount", new string[] { "accountID", "pwd" }, new string[] { "accountID" }, new string[] { "=" }, new string[] { "'"+ username + "'" });

        bool had = reader.Read();
        if (had)
        {
            //读取ID
            string dbid = reader.GetString(reader.GetOrdinal("accountID"));
            //读取Name
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
