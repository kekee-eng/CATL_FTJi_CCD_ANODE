using System;
using System.Collections.Generic;
using System.Data;

#if false
using TypeConn = Mono.Data.Sqlite.SqliteConnection;
using TypeAdapter = Mono.Data.Sqlite.SqliteDataAdapter;
#else
using TypeConn = System.Data.SQLite.SQLiteConnection;
using TypeAdapter = System.Data.SQLite.SQLiteDataAdapter;
#endif

public class TemplateDB : IDisposable {

    //
    public virtual bool Open(string path) {

        if (isOpen)
            return false;

        try {
            m_conn = new TypeConn();
            m_conn.ConnectionString = "Data Source=" + path;
            m_conn.Open();

            isOpen = true;
            return true;
        }
        catch {
            return false;
        }
    }
    public void Dispose() {

        isOpen = false;
        if (m_conn != null) {
            m_conn.Dispose();
            TypeConn.ClearPool(m_conn);

            m_conn.Dispose();
            m_conn = null;
        }
    }

    public bool Transaction(Action act) {
        if (!isOpen) return false;

        //使用事务
        var trans = m_conn.BeginTransaction();
        try {
            act();
            trans.Commit();
            return true;
        }
        catch {
            trans.Rollback();
            return false;
        }

    }

    //
    public TypeConn m_conn = null;
    public bool isOpen = false;

    //
    public void Write(string cmdtext, params object[] args) {
        if (!isOpen) return;

        var cmd = m_conn.CreateCommand();
        cmd.CommandText = cmdtext;
        foreach (var g in args)
            cmd.Parameters.AddWithValue("", g);
        cmd.ExecuteNonQuery();
        cmd.Dispose();

    }
    public List<object[]> Read(string cmdtext) {
        if (!isOpen) return null;

        DataSet ds = new DataSet();
        {
            var adapter = new TypeAdapter(cmdtext, m_conn);
            adapter.FillSchema(ds, SchemaType.Source, "root");
            adapter.Fill(ds, "root");
            adapter.Dispose();
        }
        var dt = ds.Tables["root"];

        int xcount = dt.Columns.Count;
        int ycount = dt.Rows.Count;

        List<object[]> data = new List<object[]>();
        for (int y = 0; y < ycount; y++) {
            object[] objs = new object[xcount];
            for (int x = 0; x < xcount; x++) {
                objs[x] = dt.Rows[y][x];
            }
            data.Add(objs);
        }
        return data;
    }
    public int Count(string table) {
        try { return (int)(long)Read("SELECT COUNT(0) FROM " + table)[0][0]; }
        catch { return 0; }
    }

}
