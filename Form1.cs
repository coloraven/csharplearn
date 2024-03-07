using System.Data;
using System.Text;
using System.Windows.Forms;
using Ude;

namespace MyWinFormsApp
{
    public partial class Form1 : Form
    {
        private int currentPage = 1;
        private int totalRows = 0;
        private const int rowsPerPage = 100;
        private string currentFilePath = "";
        public Form1()
        {
            InitializeComponent();
        }
        private async void btnSelectCSVFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "E:\\数据分析"; // 初始目录
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"; // 文件筛选器
                openFileDialog.FilterIndex = 1; // 默认筛选器索引
                openFileDialog.RestoreDirectory = true; // 在关闭对话框后还原当前目录

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 获取用户选择的文件路径
                    string filePath = openFileDialog.FileName;

                    // 使用文件路径（例如打开文件或显示路径）
                    // ...
                    Tbox_showPath.Text = filePath;
                    currentFilePath = filePath;
                    totalRows = await CountTotalRowsAsync(filePath);
                    // 加载并显示第一页数据
                    await LoadCsvToDataGridView(filePath, 0, 100);
                }
            }
        }
        private async Task<int> CountTotalRowsAsync(string filePath)
        {
            int count = 0;
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    await sr.ReadLineAsync();
                    count++;
                }
            }
            return count; // Returns total count including header
        }
        private async Task<DataTable> ConvertToDataTableAsync(string filePath, int startIndex, int rowCount)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                string[] headers = (await sr.ReadLineAsync()).Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                // Skip rows before the desired start index
                for (int i = 0; i < startIndex; i++)
                {
                    await sr.ReadLineAsync();
                }

                // Read the desired number of rows
                for (int i = 0; i < rowCount; i++)
                {
                    if (sr.EndOfStream) break;

                    string[] rows = (await sr.ReadLineAsync()).Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < headers.Length; j++)
                    {
                        dr[j] = rows[j];
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        private async Task LoadCsvToDataGridView(string filePath, int startIndex, int rowCount)
        {
            DataTable dt = await ConvertToDataTableAsync(filePath, startIndex, rowCount);
            showcsvDGV.DataSource = dt;
            UpdatePageLabel();
        }

        private void UpdatePageLabel()
        {
            int startRow = (currentPage - 1) * rowsPerPage + 1;
            int endRow = startRow + rowsPerPage - 1;
            labelPageNum.Text = $"TRows {totalRows} CurrentRows {startRow}-{endRow}";
        }

        private async void btnNextPage_Click(object sender, EventArgs e)
        {
            if (currentFilePath == "") return;
            if ((currentPage - 1) * rowsPerPage < totalRows)
            {
                currentPage++;
                await LoadCsvToDataGridView(currentFilePath, (currentPage - 1) * rowsPerPage, rowsPerPage);
            }
        }

        private async void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (currentFilePath == "" || currentPage == 1) return;

            currentPage--;
            await LoadCsvToDataGridView(currentFilePath, (currentPage - 1) * rowsPerPage, rowsPerPage);
        }
    }
}