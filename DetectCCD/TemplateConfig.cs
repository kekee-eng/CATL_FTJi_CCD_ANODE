using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

using TextBox = DevExpress.XtraEditors.TextEdit;
using CheckBox = DevExpress.XtraEditors.CheckEdit;

namespace DetectCCD {

    /// <summary> 配置文件基类，提供与文件和窗体控件交互功能 </summary>
    [Serializable]
    public class TemplateConfig {

        //初始化
        public TemplateConfig(string path) {
            m_config_path = path;
            LoadAs(path);
            
        }

        //字段操作（基于反射）
        string getValueFromField(FieldInfo field) {
            string value;
            object tmp = field.GetValue(this);

            if (field.FieldType == typeof(string)) {
                value = (tmp ?? "").ToString();
            }
            else if (field.FieldType == typeof(int)) {
                value = (tmp ?? "").ToString();
            }
            else if (field.FieldType == typeof(bool)) {
                value = (tmp ?? "").ToString();
            }
            else if (field.FieldType == typeof(double)) {
                value = (tmp ?? "").ToString();
            }
            else {
                throw new Exception("BaseConfig: getValueFromField: UnDefine Type." + field.FieldType.ToString());
            }
            return value;
        }
        void setValueFromField(FieldInfo field, string text) {
            if (field.FieldType == typeof(string)) {
                field.SetValue(this, Convert.ToString(text));
            }
            else if (field.FieldType == typeof(int)) {
                field.SetValue(this, Convert.ToInt32(text));
            }
            else if (field.FieldType == typeof(bool)) {
                field.SetValue(this, Convert.ToBoolean(text));
            }
            else if (field.FieldType == typeof(double)) {
                field.SetValue(this, Convert.ToDouble(text));
            }
            else {
                throw new Exception("BaseConfig: setValueFromField: UnDefine Type." + field.FieldType.ToString());
            }
        }

        //复制
        public void CopyTo(TemplateConfig other) {

            //遍历所有公共字段
            var fields = this.GetType().GetFields();
            foreach (var field in fields) {
                other.setValueFromField(field, getValueFromField(field));
            }
        }

        //文件读写
        public void Save() {
            SaveAs(m_config_path);
        }
        public void Load() {
            LoadAs(m_config_path);
        }
        public void SaveAs(string filename) {

            //XML文档
            XmlDocument xmldoc = new XmlDocument();

            //添加声明段落
            xmldoc.AppendChild(xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", ""));

            //添加root
            xmldoc.AppendChild(xmldoc.CreateElement("root"));

            //遍历所有公共字段
            var fields = this.GetType().GetFields();
            foreach (var field in fields) {
                XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, field.Name, "");
                node.InnerText = getValueFromField(field);
                xmldoc.ChildNodes[1].AppendChild(node);
            }

            //保存
            xmldoc.Save(filename);
        }
        public void LoadAs(string filename) {

            if (!File.Exists(filename))
                return;

            //XML文档
            XmlDocument xmldoc = new XmlDocument();

            //加载文件
            xmldoc.Load(filename);

            //遍历所有公共字段
            var fields = this.GetType().GetFields();
            foreach (var field in fields) {
                XmlNode node = xmldoc.ChildNodes[1].SelectSingleNode(field.Name);
                if (node != null)
                    setValueFromField(field, node.InnerText);
            }
        }

        //绑定界面操作
        public void BindCheckBox(CheckBox cb, string name) {

            //加入绑定列表中
            cb.Tag = name;
            if (m_cbs == null)
                m_cbs = new List<CheckBox>();
            m_cbs.Add(cb);

            //载入值
            {
                bool findIt = false;
                var fields = this.GetType().GetFields();
                foreach (var field in fields) {
                    if (name == field.Name) {
                        cb.Checked = Convert.ToBoolean(getValueFromField(field));
                        findIt = true;
                        break;
                    }
                }

                if (!findIt) {
                    throw new Exception("DataBase: BindCheckBox: 未找到变量: " + name);
                }
            }

            //修改事件
            cb.CheckedChanged += (s1, e1) => {
                var fields = this.GetType().GetFields();
                foreach (var field in fields) {
                    if (name == field.Name) {
                        setValueFromField(field, Convert.ToString(cb.Checked));
                        OnValueChanged?.Invoke();
                        break;
                    }
                }
            };
        }
        public void BindTextBox(TextBox tb, string name) {

            //加入绑定列表中
            tb.Tag = name;
            if (m_tbs == null)
                m_tbs = new List<TextBox>();
            m_tbs.Add(tb);

            //载入值
            {
                bool findIt = false;
                var fields = this.GetType().GetFields();
                foreach (var field in fields) {
                    if (name == field.Name) {
                        tb.Text = getValueFromField(field);
                        tb.ForeColor = Color.Black;
                        findIt = true;
                        break;
                    }
                }

                if (!findIt) {
                    throw new Exception("DataBase: BindTextBox: 未找到变量: " + name);
                }
            }

            //修改事件
            tb.TextChanged += (s1, e1) => {
                var fields = this.GetType().GetFields();
                foreach (var field in fields) {
                    if (name == field.Name) {
                        if (m_provider == null)
                            m_provider = new ErrorProvider();

                        try {
                            setValueFromField(field, tb.Text);
                            m_provider.SetError(tb, "");
                            tb.BackColor = Color.LightGreen;

                            //Fix
                            OnValueChanged?.Invoke();
                        }
                        catch {
                            m_provider.SetError(tb, "格式错误");
                            tb.BackColor = Color.Pink;
                        }
                        break;
                    }
                }
            };
        }
        public void UpdateBind() {

            var fields = this.GetType().GetFields();
            foreach (var tb in m_tbs) {
                if (tb != null && tb.IsHandleCreated) {
                    foreach (var field in fields) {
                        if ((string)tb.Tag == field.Name) {
                            tb.Text = getValueFromField(field);
                            break;
                        }
                    }
                }
            }
            foreach (var cb in m_cbs) {
                if (cb != null && cb.IsHandleCreated) {
                    foreach (var field in fields) {
                        if ((string)cb.Tag == field.Name) {
                            cb.Checked = Convert.ToBoolean(getValueFromField(field));
                            break;
                        }
                    }
                }
            }
        }
        public void BindDataGridView(DataGridView _grid) {

            //保存对象
            m_grid = _grid;

            //初始化界面对象
            m_grid.AllowUserToAddRows = false;
            m_grid.AllowUserToDeleteRows = false;

            m_grid.Columns.Clear();
            m_grid.Columns.Add("c1", "参数名称");
            m_grid.Columns.Add("c2", "值");

            m_grid.Columns[0].Width = 200;
            m_grid.Columns[1].Width = 200;

            m_grid.Columns[0].ReadOnly = true;

            //添加行
            m_grid.Rows.Clear();
            {
                var fields = this.GetType().GetFields();
                foreach (var field in fields) {
                    var name = field.Name;
                    var value = getValueFromField(field);
                    
                    m_grid.Rows.Add(name, value);
                }
            }

            //修改事件
            m_grid.CellValueChanged += (sender, e) => {
                var fields = this.GetType().GetFields();
                foreach (var field in fields) {
                    if (field.Name == m_grid.Rows[e.RowIndex].Cells[0].Value.ToString()) {
                        try {
                            string value = m_grid.Rows[e.RowIndex].Cells[1].Value.ToString();
                            setValueFromField(field, value);

                            m_grid.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.LightGreen;

                            //Fix
                            OnValueChanged?.Invoke();
                        }
                        catch {
                            m_grid.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.Pink;
                        }
                    }
                }
            };

        }

        //界面操作反馈事件
        public event Action OnValueChanged;

        //内部变量
        string m_config_path = "";
        [NonSerialized]
        ErrorProvider m_provider = null;
        [NonSerialized]
        List<TextBox> m_tbs = null;
        [NonSerialized]
        List<CheckBox> m_cbs = null;
        [NonSerialized]
        DataGridView m_grid;

    }

}
