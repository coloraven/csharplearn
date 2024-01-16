using System.Data;
using System.Text;
using Ude;

namespace MyWinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private async void btnSelectCSVFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "E:\\ 数据分析"; // 初始目录
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"; // 文件筛选器
                openFileDialog.FilterIndex = 1; // 默认筛选器索引
                openFileDialog.RestoreDirectory = true; // 在关闭对话框后还原当前目录

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 获取用户选择的文件路径
                    string filePath = openFileDialog.FileName;

                    // 使用文件路径（例如打开文件或显示路径）
                    //...
                    Tbox_showPath.Text = filePath;

                    // 异步加载 CSV 文件并显示在 DataGridView
                    await LoadCsvToDataGridView(filePath);
                }
            }
        }
        private async Task<DataTable> ConvertToDataTableAsync(string filePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                string[] headers = (await sr.ReadLineAsync()).Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = (await sr.ReadLineAsync()).Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }


        private async Task LoadCsvToDataGridView(string filePath)
        {

            DataTable dt = await ConvertToDataTableAsync(filePath);
            showcsvDGV.DataSource = dt;

        }

    }

}