namespace ControlDomotico.UI
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
            components = new System.ComponentModel.Container();
            TableLayoutPanel tableLayoutPanel1;
            Panel panelControls;
            Panel panelList;
            FlowLayoutPanel flowLayoutButtons;
            FlowLayoutPanel flowLayoutMasive;
            tableLayoutPanel1 = new TableLayoutPanel();
            panelControls = new Panel();
            lblNombre = new Label();
            txtNombre = new TextBox();
            btnAgregar = new Button();
            flowLayoutButtons = new FlowLayoutPanel();
            btnEncender = new Button();
            btnApagar = new Button();
            btnAlternar = new Button();
            flowLayoutMasive = new FlowLayoutPanel();
            btnEncenderTodos = new Button();
            btnApagarTodos = new Button();
            panelList = new Panel();
            lstDispositivos = new ListBox();
            lblResumen = new Label();
            errorProvider1 = new ErrorProvider(components);
            grpConexion = new GroupBox();
            lblEstadoConexion = new Label();
            btnDesconectar = new Button();
            btnConectar = new Button();
            btnActualizarPuertos = new Button();
            cmbPuertos = new ComboBox();
            grpMotor = new GroupBox();
            lblMotorVelocidad = new Label();
            trackBarMotor = new TrackBar();
            btnMotorStop = new Button();
            btnMotorReversa = new Button();
            btnMotorAdelante = new Button();
            grpSensor = new GroupBox();
            lblTemperaturaValor = new Label();
            lblTemperatura = new Label();
            btnLeerTemp = new Button();
            lstLog = new ListBox();
            tableLayoutPanel1.SuspendLayout();
            panelControls.SuspendLayout();
            flowLayoutButtons.SuspendLayout();
            flowLayoutMasive.SuspendLayout();
            panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            grpConexion.SuspendLayout();
            grpMotor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarMotor).BeginInit();
            grpSensor.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 370F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panelControls, 0, 0);
            tableLayoutPanel1.Controls.Add(panelList, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(870, 280);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panelControls
            // 
            panelControls.Controls.Add(lblNombre);
            panelControls.Controls.Add(txtNombre);
            panelControls.Controls.Add(btnAgregar);
            panelControls.Controls.Add(flowLayoutButtons);
            panelControls.Controls.Add(flowLayoutMasive);
            panelControls.Dock = DockStyle.Fill;
            panelControls.Location = new Point(3, 3);
            panelControls.Name = "panelControls";
            panelControls.Padding = new Padding(15);
            panelControls.Size = new Size(364, 274);
            panelControls.TabIndex = 0;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNombre.Location = new Point(15, 15);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(166, 23);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre dispositivo";
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 11F);
            txtNombre.Location = new Point(15, 41);
            txtNombre.Margin = new Padding(0, 0, 30, 0);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Ej: Lámpara sala";
            txtNombre.Size = new Size(275, 32);
            txtNombre.TabIndex = 0;
            txtNombre.TextChanged += txtNombre_TextChanged;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(0, 120, 215);
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Location = new Point(15, 79);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(305, 38);
            btnAgregar.TabIndex = 1;
            btnAgregar.Text = "➕ Agregar Dispositivo";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // flowLayoutButtons
            // 
            flowLayoutButtons.Controls.Add(btnEncender);
            flowLayoutButtons.Controls.Add(btnApagar);
            flowLayoutButtons.Controls.Add(btnAlternar);
            flowLayoutButtons.Location = new Point(15, 123);
            flowLayoutButtons.Name = "flowLayoutButtons";
            flowLayoutButtons.Size = new Size(350, 45);
            flowLayoutButtons.TabIndex = 2;
            // 
            // btnEncender
            // 
            btnEncender.BackColor = Color.FromArgb(16, 124, 16);
            btnEncender.FlatAppearance.BorderSize = 0;
            btnEncender.FlatStyle = FlatStyle.Flat;
            btnEncender.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEncender.ForeColor = Color.White;
            btnEncender.Location = new Point(3, 3);
            btnEncender.Name = "btnEncender";
            btnEncender.Size = new Size(110, 38);
            btnEncender.TabIndex = 0;
            btnEncender.Text = "💡 Encender";
            btnEncender.UseVisualStyleBackColor = false;
            btnEncender.Click += btnEncender_Click;
            // 
            // btnApagar
            // 
            btnApagar.BackColor = Color.FromArgb(192, 0, 0);
            btnApagar.FlatAppearance.BorderSize = 0;
            btnApagar.FlatStyle = FlatStyle.Flat;
            btnApagar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnApagar.ForeColor = Color.White;
            btnApagar.Location = new Point(119, 3);
            btnApagar.Name = "btnApagar";
            btnApagar.Size = new Size(100, 38);
            btnApagar.TabIndex = 1;
            btnApagar.Text = "🔴 Apagar";
            btnApagar.UseVisualStyleBackColor = false;
            btnApagar.Click += btnApagar_Click;
            // 
            // btnAlternar
            // 
            btnAlternar.BackColor = Color.FromArgb(255, 140, 0);
            btnAlternar.FlatAppearance.BorderSize = 0;
            btnAlternar.FlatStyle = FlatStyle.Flat;
            btnAlternar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAlternar.ForeColor = Color.White;
            btnAlternar.Location = new Point(225, 3);
            btnAlternar.Name = "btnAlternar";
            btnAlternar.Size = new Size(105, 38);
            btnAlternar.TabIndex = 2;
            btnAlternar.Text = "🔄 Alternar";
            btnAlternar.UseVisualStyleBackColor = false;
            btnAlternar.Click += btnAlternar_Click;
            // 
            // flowLayoutMasive
            // 
            flowLayoutMasive.Controls.Add(btnEncenderTodos);
            flowLayoutMasive.Controls.Add(btnApagarTodos);
            flowLayoutMasive.FlowDirection = FlowDirection.TopDown;
            flowLayoutMasive.Location = new Point(15, 174);
            flowLayoutMasive.Name = "flowLayoutMasive";
            flowLayoutMasive.Size = new Size(305, 90);
            flowLayoutMasive.TabIndex = 3;
            // 
            // btnEncenderTodos
            // 
            btnEncenderTodos.BackColor = Color.FromArgb(16, 124, 16);
            btnEncenderTodos.FlatAppearance.BorderSize = 0;
            btnEncenderTodos.FlatStyle = FlatStyle.Flat;
            btnEncenderTodos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEncenderTodos.ForeColor = Color.White;
            btnEncenderTodos.Location = new Point(3, 3);
            btnEncenderTodos.Name = "btnEncenderTodos";
            btnEncenderTodos.Size = new Size(304, 38);
            btnEncenderTodos.TabIndex = 0;
            btnEncenderTodos.Text = "💡 Encender Todos";
            btnEncenderTodos.UseVisualStyleBackColor = false;
            btnEncenderTodos.Click += btnEncenderTodos_Click;
            // 
            // btnApagarTodos
            // 
            btnApagarTodos.BackColor = Color.FromArgb(192, 0, 0);
            btnApagarTodos.FlatAppearance.BorderSize = 0;
            btnApagarTodos.FlatStyle = FlatStyle.Flat;
            btnApagarTodos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnApagarTodos.ForeColor = Color.White;
            btnApagarTodos.Location = new Point(3, 47);
            btnApagarTodos.Name = "btnApagarTodos";
            btnApagarTodos.Size = new Size(304, 38);
            btnApagarTodos.TabIndex = 1;
            btnApagarTodos.Text = "🔴 Apagar Todos";
            btnApagarTodos.UseVisualStyleBackColor = false;
            btnApagarTodos.Click += btnApagarTodos_Click;
            // 
            // panelList
            // 
            panelList.Controls.Add(lstDispositivos);
            panelList.Controls.Add(lblResumen);
            panelList.Dock = DockStyle.Fill;
            panelList.Location = new Point(373, 3);
            panelList.Name = "panelList";
            panelList.Size = new Size(494, 274);
            panelList.TabIndex = 1;
            // 
            // lstDispositivos
            // 
            lstDispositivos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstDispositivos.Font = new Font("Segoe UI", 10F);
            lstDispositivos.FormattingEnabled = true;
            lstDispositivos.ItemHeight = 23;
            lstDispositivos.Location = new Point(3, 10);
            lstDispositivos.Name = "lstDispositivos";
            lstDispositivos.Size = new Size(488, 233);
            lstDispositivos.TabIndex = 0;
            lstDispositivos.SelectedIndexChanged += lstDispositivos_SelectedIndexChanged;
            lstDispositivos.DoubleClick += lstDispositivos_DoubleClick;
            lstDispositivos.KeyDown += lstDispositivos_KeyDown;
            // 
            // lblResumen
            // 
            lblResumen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblResumen.AutoSize = true;
            lblResumen.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblResumen.ForeColor = Color.Gray;
            lblResumen.Location = new Point(3, 246);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(268, 20);
            lblResumen.TabIndex = 1;
            lblResumen.Text = "Total: 0 | Encendidos: 0 | Apagados: 0";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // grpConexion
            // 
            grpConexion.Controls.Add(lblEstadoConexion);
            grpConexion.Controls.Add(btnDesconectar);
            grpConexion.Controls.Add(btnConectar);
            grpConexion.Controls.Add(btnActualizarPuertos);
            grpConexion.Controls.Add(cmbPuertos);
            grpConexion.Location = new Point(2, 296);
            grpConexion.Name = "grpConexion";
            grpConexion.Size = new Size(369, 202);
            grpConexion.TabIndex = 6;
            grpConexion.TabStop = false;
            grpConexion.Text = "Conexión Serial";
            grpConexion.Enter += groupBox1_Enter;
            // 
            // lblEstadoConexion
            // 
            lblEstadoConexion.AutoSize = true;
            lblEstadoConexion.ForeColor = Color.Red;
            lblEstadoConexion.Location = new Point(6, 176);
            lblEstadoConexion.Name = "lblEstadoConexion";
            lblEstadoConexion.Size = new Size(107, 23);
            lblEstadoConexion.TabIndex = 4;
            lblEstadoConexion.Text = "Sin conexión";
            // 
            // btnDesconectar
            // 
            btnDesconectar.Location = new Point(202, 101);
            btnDesconectar.Name = "btnDesconectar";
            btnDesconectar.Size = new Size(135, 29);
            btnDesconectar.TabIndex = 3;
            btnDesconectar.Text = "Desconectar";
            btnDesconectar.UseVisualStyleBackColor = true;
            btnDesconectar.Click += btnDesconectar_Cilck;
            // 
            // btnConectar
            // 
            btnConectar.Location = new Point(243, 66);
            btnConectar.Name = "btnConectar";
            btnConectar.Size = new Size(94, 29);
            btnConectar.TabIndex = 2;
            btnConectar.Text = "Conectar";
            btnConectar.UseVisualStyleBackColor = true;
            btnConectar.Click += btnConectar_Click;
            // 
            // btnActualizarPuertos
            // 
            btnActualizarPuertos.Location = new Point(243, 31);
            btnActualizarPuertos.Name = "btnActualizarPuertos";
            btnActualizarPuertos.Size = new Size(94, 29);
            btnActualizarPuertos.TabIndex = 1;
            btnActualizarPuertos.Text = "Actualizar";
            btnActualizarPuertos.UseVisualStyleBackColor = true;
            btnActualizarPuertos.Click += btnActualizarPuertos_Click;
            // 
            // cmbPuertos
            // 
            cmbPuertos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPuertos.FormattingEnabled = true;
            cmbPuertos.Location = new Point(6, 29);
            cmbPuertos.Name = "cmbPuertos";
            cmbPuertos.Size = new Size(151, 31);
            cmbPuertos.TabIndex = 0;
            cmbPuertos.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // grpMotor
            // 
            grpMotor.Controls.Add(lblMotorVelocidad);
            grpMotor.Controls.Add(trackBarMotor);
            grpMotor.Controls.Add(btnMotorStop);
            grpMotor.Controls.Add(btnMotorReversa);
            grpMotor.Controls.Add(btnMotorAdelante);
            grpMotor.Location = new Point(2, 504);
            grpMotor.Name = "grpMotor";
            grpMotor.Size = new Size(369, 166);
            grpMotor.TabIndex = 8;
            grpMotor.TabStop = false;
            grpMotor.Text = "Control de Motor";
            // 
            // lblMotorVelocidad
            // 
            lblMotorVelocidad.AutoSize = true;
            lblMotorVelocidad.Location = new Point(155, 27);
            lblMotorVelocidad.Name = "lblMotorVelocidad";
            lblMotorVelocidad.Size = new Size(100, 23);
            lblMotorVelocidad.TabIndex = 4;
            lblMotorVelocidad.Text = "Velocidad: 0";
            // 
            // trackBarMotor
            // 
            trackBarMotor.LargeChange = 50;
            trackBarMotor.Location = new Point(6, 53);
            trackBarMotor.Maximum = 255;
            trackBarMotor.Minimum = 0;
            trackBarMotor.Name = "trackBarMotor";
            trackBarMotor.Size = new Size(357, 56);
            trackBarMotor.SmallChange = 10;
            trackBarMotor.TabIndex = 3;
            trackBarMotor.TickFrequency = 25;
            trackBarMotor.ValueChanged += trackBarMotor_ValueChanged;
            // 
            // btnMotorStop
            // 
            btnMotorStop.BackColor = Color.FromArgb(192, 0, 0);
            btnMotorStop.FlatStyle = FlatStyle.Flat;
            btnMotorStop.ForeColor = Color.White;
            btnMotorStop.Location = new Point(243, 115);
            btnMotorStop.Name = "btnMotorStop";
            btnMotorStop.Size = new Size(120, 38);
            btnMotorStop.TabIndex = 2;
            btnMotorStop.Text = "🛑 Detener";
            btnMotorStop.UseVisualStyleBackColor = false;
            btnMotorStop.Click += btnMotorStop_Click;
            // 
            // btnMotorReversa
            // 
            btnMotorReversa.BackColor = Color.FromArgb(255, 140, 0);
            btnMotorReversa.FlatStyle = FlatStyle.Flat;
            btnMotorReversa.ForeColor = Color.White;
            btnMotorReversa.Location = new Point(6, 115);
            btnMotorReversa.Name = "btnMotorReversa";
            btnMotorReversa.Size = new Size(110, 38);
            btnMotorReversa.TabIndex = 1;
            btnMotorReversa.Text = "⬅️ Reversa";
            btnMotorReversa.UseVisualStyleBackColor = false;
            btnMotorReversa.Click += btnMotorReversa_Click;
            // 
            // btnMotorAdelante
            // 
            btnMotorAdelante.BackColor = Color.FromArgb(16, 124, 16);
            btnMotorAdelante.FlatStyle = FlatStyle.Flat;
            btnMotorAdelante.ForeColor = Color.White;
            btnMotorAdelante.Location = new Point(122, 115);
            btnMotorAdelante.Name = "btnMotorAdelante";
            btnMotorAdelante.Size = new Size(115, 38);
            btnMotorAdelante.TabIndex = 0;
            btnMotorAdelante.Text = "➡️ Adelante";
            btnMotorAdelante.UseVisualStyleBackColor = false;
            btnMotorAdelante.Click += btnMotorAdelante_Click;
            // 
            // grpSensor
            // 
            grpSensor.Controls.Add(lblTemperaturaValor);
            grpSensor.Controls.Add(lblTemperatura);
            grpSensor.Controls.Add(btnLeerTemp);
            grpSensor.Location = new Point(377, 504);
            grpSensor.Name = "grpSensor";
            grpSensor.Size = new Size(250, 120);
            grpSensor.TabIndex = 9;
            grpSensor.TabStop = false;
            grpSensor.Text = "Sensor LM75";
            // 
            // lblTemperaturaValor
            // 
            lblTemperaturaValor.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTemperaturaValor.ForeColor = Color.FromArgb(0, 120, 215);
            lblTemperaturaValor.Location = new Point(6, 27);
            lblTemperaturaValor.Name = "lblTemperaturaValor";
            lblTemperaturaValor.Size = new Size(238, 35);
            lblTemperaturaValor.TabIndex = 2;
            lblTemperaturaValor.Text = "-- °C";
            lblTemperaturaValor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTemperatura
            // 
            lblTemperatura.AutoSize = true;
            lblTemperatura.Location = new Point(6, 62);
            lblTemperatura.Name = "lblTemperatura";
            lblTemperatura.Size = new Size(0, 23);
            lblTemperatura.TabIndex = 1;
            // 
            // btnLeerTemp
            // 
            btnLeerTemp.BackColor = Color.FromArgb(0, 120, 215);
            btnLeerTemp.FlatStyle = FlatStyle.Flat;
            btnLeerTemp.ForeColor = Color.White;
            btnLeerTemp.Location = new Point(6, 71);
            btnLeerTemp.Name = "btnLeerTemp";
            btnLeerTemp.Size = new Size(238, 38);
            btnLeerTemp.TabIndex = 0;
            btnLeerTemp.Text = "🌡️ Leer Temperatura";
            btnLeerTemp.UseVisualStyleBackColor = false;
            btnLeerTemp.Click += btnLeerTemp_Click;
            // 
            // lstLog
            // 
            lstLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstLog.FormattingEnabled = true;
            lstLog.ItemHeight = 23;
            lstLog.Location = new Point(633, 504);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(225, 166);
            lstLog.TabIndex = 7;
            // 
            // Form1
            // 
            AcceptButton = btnAgregar;
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 678);
            Controls.Add(grpSensor);
            Controls.Add(grpMotor);
            Controls.Add(lstLog);
            Controls.Add(grpConexion);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Segoe UI", 10F);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Control Domótico — GUI base";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            tableLayoutPanel1.ResumeLayout(false);
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            flowLayoutButtons.ResumeLayout(false);
            flowLayoutMasive.ResumeLayout(false);
            panelList.ResumeLayout(false);
            panelList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            grpConexion.ResumeLayout(false);
            grpConexion.PerformLayout();
            grpMotor.ResumeLayout(false);
            grpMotor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarMotor).EndInit();
            grpSensor.ResumeLayout(false);
            grpSensor.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtNombre;
        private Label lblNombre;
        private Button btnApagar;
        private Button btnEncender;
        private Button btnAgregar;
        private ListBox lstDispositivos;
        private Label lblResumen;
        private ErrorProvider errorProvider1;
        private GroupBox grpConexion;
        private ComboBox cmbPuertos;
        private Button btnConectar;
        private Button btnActualizarPuertos;
        private Label lblEstadoConexion;
        private Button btnDesconectar;
        private ListBox lstLog;

        // Alias properties for designer controls
        private Button button1 => btnAlternar;
        private Button button2 => btnEncenderTodos;
        private Button button3 => btnApagarTodos;
        private Label label2 => lblResumen;
        
        private Button btnAlternar;
        private Button btnEncenderTodos;
        private Button btnApagarTodos;
        private GroupBox grpMotor;
        private TrackBar trackBarMotor;
        private Button btnMotorStop;
        private Button btnMotorReversa;
        private Button btnMotorAdelante;
        private Label lblMotorVelocidad;
        private GroupBox grpSensor;
        private Button btnLeerTemp;
        private Label lblTemperatura;
        private Label lblTemperaturaValor;
    }
}
