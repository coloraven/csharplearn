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
                openFileDialog.InitialDirectory = "E:\\ ���ݷ���"; // ��ʼĿ¼
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"; // �ļ�ɸѡ��
                openFileDialog.FilterIndex = 1; // Ĭ��ɸѡ������
                openFileDialog.RestoreDirectory = true; // �ڹرնԻ����ԭ��ǰĿ¼

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // ��ȡ�û�ѡ����ļ�·��
                    string filePath = openFileDialog.FileName;

                    // ʹ���ļ�·����������ļ�����ʾ·����
                    //...
                    Tbox_showPath.Text = filePath;

                    // �첽���� CSV �ļ�����ʾ�� DataGridView
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