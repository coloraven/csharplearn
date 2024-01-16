namespace MyWinFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnprePage = new Button();
            btnnextPage = new Button();
            labelPageNum = new Label();
            showcsvDGV = new DataGridView();
            btnSelectCSVFile = new Button();
            Tbox_showPath = new TextBox();
            ((System.ComponentModel.ISupportInitialize)showcsvDGV).BeginInit();
            SuspendLayout();
            // 
            // btnprePage
            // 
            btnprePage.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnprePage.Location = new Point(973, 969);
            btnprePage.Name = "btnprePage";
            btnprePage.Size = new Size(169, 52);
            btnprePage.TabIndex = 0;
            btnprePage.Text = "上一页";
            btnprePage.UseVisualStyleBackColor = true;
            // 
            // btnnextPage
            // 
            btnnextPage.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnnextPage.Location = new Point(1182, 969);
            btnnextPage.Name = "btnnextPage";
            btnnextPage.Size = new Size(169, 52);
            btnnextPage.TabIndex = 1;
            btnnextPage.Text = "下一页";
            btnnextPage.UseVisualStyleBackColor = true;
            // 
            // labelPageNum
            // 
            labelPageNum.Anchor = AnchorStyles.Bottom;
            labelPageNum.AutoSize = true;
            labelPageNum.Location = new Point(671, 978);
            labelPageNum.Name = "labelPageNum";
            labelPageNum.Size = new Size(43, 35);
            labelPageNum.TabIndex = 2;
            labelPageNum.Text = " / ";
            // 
            // showcsvDGV
            // 
            showcsvDGV.AllowUserToOrderColumns = true;
            showcsvDGV.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            showcsvDGV.BackgroundColor = Color.WhiteSmoke;
            showcsvDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            showcsvDGV.Location = new Point(45, 27);
            showcsvDGV.Name = "showcsvDGV";
            showcsvDGV.RowHeadersWidth = 92;
            showcsvDGV.RowTemplate.Height = 44;
            showcsvDGV.Size = new Size(1306, 821);
            showcsvDGV.TabIndex = 3;
            // 
            // btnSelectCSVFile
            // 
            btnSelectCSVFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSelectCSVFile.Location = new Point(45, 969);
            btnSelectCSVFile.Name = "btnSelectCSVFile";
            btnSelectCSVFile.Size = new Size(169, 52);
            btnSelectCSVFile.TabIndex = 4;
            btnSelectCSVFile.Text = "选择CSV文件";
            btnSelectCSVFile.UseVisualStyleBackColor = true;
            btnSelectCSVFile.Click += btnSelectCSVFile_Click;
            // 
            // Tbox_showPath
            // 
            Tbox_showPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Tbox_showPath.Location = new Point(45, 873);
            Tbox_showPath.Multiline = true;
            Tbox_showPath.Name = "Tbox_showPath";
            Tbox_showPath.Size = new Size(1306, 63);
            Tbox_showPath.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(16F, 35F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1401, 1059);
            Controls.Add(Tbox_showPath);
            Controls.Add(btnSelectCSVFile);
            Controls.Add(showcsvDGV);
            Controls.Add(labelPageNum);
            Controls.Add(btnnextPage);
            Controls.Add(btnprePage);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "CSV数据展示工具";
            ((System.ComponentModel.ISupportInitialize)showcsvDGV).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnprePage;
        private Button btnnextPage;
        private Label labelPageNum;
        private DataGridView showcsvDGV;
        private Button btnSelectCSVFile;
        private TextBox Tbox_showPath;
    }
}
