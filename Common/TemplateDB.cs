﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
    public class TemplateDB {

        //
        public bool Open(string path) {

            if (m_isOpen)
                return false;

            try {
                m_conn.ConnectionString = "Data Source=" + path;
                m_conn.Open();
            }
            catch {
                return false;
            }
            m_isOpen = true;
            return true;
        }
        public void Close() {
            m_conn.Close();
            m_isOpen = false;
        }

        public void Transaction(Action act) {

            //使用事务
            var trans = m_conn.BeginTransaction();
            try {
                act();
                trans.Commit();
            }
            catch {
                trans.Rollback();
                throw;
            }

        }

        //
        public SQLiteConnection m_conn = new SQLiteConnection();
        public bool m_isOpen = false;

        //
        protected void write(string cmdtext, params object[] args) {
            var cmd = m_conn.CreateCommand();
            cmd.CommandText = cmdtext;
            foreach (var g in args)
                cmd.Parameters.AddWithValue("", g);
            cmd.ExecuteNonQuery();
        }
        protected List<object[]> read(string cmdtext) {
            DataSet ds = new DataSet();
            {
                var adapter = new SQLiteDataAdapter(cmdtext, m_conn);
                adapter.FillSchema(ds, SchemaType.Source, "root");
                adapter.Fill(ds, "root");
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
        protected int count(string table) {
            try { return Convert.ToInt32(read("SELECT COUNT(0) FROM " + table)[0][0]); }
            catch { return 0; }
        }

    }
}
