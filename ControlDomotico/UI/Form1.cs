using ControlDomotico.Domain;
using System.Linq;
using System.Net.NetworkInformation;
using System.IO.Ports;
namespace ControlDomotico.UI
{
    public partial class Form1 : Form
    {
        private readonly ControladorDomotico _ctrl = new();
        private readonly SerialPort _sp = new SerialPort();
        public Form1()
        {
            InitializeComponent();

            _sp.DataReceived += Sp_DataReceived;  // ← LÍNEA NUEVA 1

            ConfigurarEstadoInicial();
            CargarPuertos();  // ← LÍNEA NUEVA 2
        }
        private void ConfigurarEstadoInicial()
        {
            RefrescarLista();
            ActualizarBotones();
            ActualizarResumen();
        }
        // NUEVO: alternar el estado del seleccionado
        private void btnAlternar_Click(object sender, EventArgs e)
        {
            var d = DispositivoSeleccionado();
            if (d is null) return;

            if (d.EstaEncendido)
            {
                d.Apagar();
                EnviarComando("LED1:OFF");
            }
            else
            {
                d.Encender();
                EnviarComando("LED1:ON");
            }

            RefrescarLista(preservarSeleccion: true);
            ActualizarResumen();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var nombre = txtNombre.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                errorProvider1.SetError(txtNombre, "Ingrese un nombre.");
                txtNombre.Focus();
                return;
            }

            var lampara = new Lampara(nombre);

            // Intenta agregar, verifica si fue exitoso
            if (!_ctrl.Agregar(lampara))
            {
                errorProvider1.SetError(txtNombre, "Ya existe un dispositivo con ese nombre.");
                txtNombre.SelectAll();
                txtNombre.Focus();
                return;
            }

            // Si se agregó correctamente, limpia el error
            errorProvider1.SetError(txtNombre, string.Empty);
            txtNombre.Clear();
            txtNombre.Focus();

            Log($"✓ Dispositivo '{nombre}' agregado");
            RefrescarLista();
            ActualizarResumen();
        }
        private void btnEncender_Click(object sender, EventArgs e)
        {
            var d = DispositivoSeleccionado();
            if (d is null) return;

            d.Encender();
            EnviarComando("LED1:ON");
            RefrescarLista(preservarSeleccion: true);
            ActualizarResumen();
        }
        private void btnApagar_Click(object sender, EventArgs e)
        {
            var d = DispositivoSeleccionado();
            if (d is null) return;

            d.Apagar();
            EnviarComando("LED1:OFF");
            RefrescarLista(preservarSeleccion: true);
            ActualizarResumen();
        }
        private void lstDispositivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarBotones();
        }
        private IActuable? DispositivoSeleccionado()
        {
            int idx = lstDispositivos.SelectedIndex;
            var lista = _ctrl.Dispositivos;
            if (idx < 0 || idx >= lista.Count) return null;
            return lista[idx];
        }
        private void RefrescarLista(bool preservarSeleccion = false, bool seleccionarUltimoSiAgrego = false)
        {
            int idxPrevio = lstDispositivos.SelectedIndex;
            bool habiaSeleccion = idxPrevio >= 0;

            lstDispositivos.BeginUpdate();
            lstDispositivos.Items.Clear();

            foreach (var d in _ctrl.Dispositivos)
                lstDispositivos.Items.Add(d.ToString());

            lstDispositivos.EndUpdate();

            // Restaurar selección
            if (preservarSeleccion && habiaSeleccion)
            {
                if (idxPrevio >= 0 && idxPrevio < lstDispositivos.Items.Count)
                    lstDispositivos.SelectedIndex = idxPrevio;
                else if (lstDispositivos.Items.Count > 0)
                    lstDispositivos.SelectedIndex = lstDispositivos.Items.Count - 1;
            }
            else if (seleccionarUltimoSiAgrego && lstDispositivos.Items.Count > 0)
            {
                lstDispositivos.SelectedIndex = lstDispositivos.Items.Count - 1;
            }

            ActualizarBotones();
        }
        private void ActualizarBotones()
        {
            bool haySeleccion = lstDispositivos.SelectedIndex >= 0;
            btnEncender.Enabled = haySeleccion;
            btnApagar.Enabled = haySeleccion;
            btnAlternar.Enabled = haySeleccion;

            // Habilitar botones masivos si existe al menos un dispositivo
            bool hayAlMenosUno = _ctrl.Dispositivos.Count > 0;
            btnEncenderTodos.Enabled = hayAlMenosUno;
            btnApagarTodos.Enabled = hayAlMenosUno;
        }
        private void ActualizarResumen()
        {
            int total = _ctrl.Dispositivos.Count;
            int encendidos = _ctrl.Dispositivos.Count(d => d.EstaEncendido);
            int apagados = total - encendidos;

            lblResumen.Text = $"Total: {total} | Encendidos: {encendidos} | Apagados: {apagados}";
        }

        private void CargarPuertos()
        {
            try
            {
                cmbPuertos.Items.Clear();
                string[] puertos = SerialPort.GetPortNames();
                cmbPuertos.Items.AddRange(puertos);

                if (puertos.Length > 0)
                {
                    cmbPuertos.SelectedIndex = 0;
                    lblEstadoConexion.Text = "Seleccione un puerto y presione Conectar.";
                    lblEstadoConexion.ForeColor = Color.Blue;
                }
                else
                {
                    lblEstadoConexion.Text = "No se detectan puertos COM.";
                    lblEstadoConexion.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblEstadoConexion.Text = $"Error al listar puertos: {ex.Message}";
                lblEstadoConexion.ForeColor = Color.Red;
            }
        }

        private void Log(string mensaje)
        {
            // Verifica si estamos en otro hilo (necesario para la comunicación serial)
            if (lstLog.InvokeRequired)
            {
                lstLog.BeginInvoke(new Action(() => Log(mensaje)));
                return;
            }

            // Agrega el mensaje con la hora actual
            lstLog.Items.Add($"{DateTime.Now:HH:mm:ss} - {mensaje}");

            // Se desplaza automáticamente al último elemento
            lstLog.TopIndex = lstLog.Items.Count - 1;
        }

        private void Conectar()
        {
            try
            {
                // Verifica que haya un puerto seleccionado
                if (cmbPuertos.SelectedItem == null)
                {
                    lblEstadoConexion.Text = "Seleccione un puerto antes de conectar.";
                    lblEstadoConexion.ForeColor = Color.Red;
                    return;
                }

                // Verifica que no esté ya conectado
                if (_sp.IsOpen)
                {
                    lblEstadoConexion.Text = "Ya existe una conexión abierta.";
                    lblEstadoConexion.ForeColor = Color.Orange;
                    return;
                }

                // Configuración del puerto serial
                _sp.PortName = cmbPuertos.SelectedItem.ToString();  // Nombre del puerto (COM7)
                _sp.BaudRate = 115200;  // Velocidad de comunicación (debe coincidir con ESP32)
                _sp.NewLine = "\n";  // Terminador de línea

                // Abre el puerto
                _sp.Open();

                // Actualiza la interfaz
                lblEstadoConexion.Text = $"Conectado a {_sp.PortName}";
                lblEstadoConexion.ForeColor = Color.Green;
                Log($"Conexión establecida en {_sp.PortName}");
            }
            catch (Exception ex)
            {
                // Si hay error (puerto ocupado, desconectado, etc.)
                lblEstadoConexion.Text = $"Error al conectar: {ex.Message}";
                lblEstadoConexion.ForeColor = Color.Red;
                Log($"Error al conectar: {ex.Message}");
            }
        }

        private void Desconectar()
        {
            try
            {
                if (_sp.IsOpen)
                {
                    _sp.Close();
                    lblEstadoConexion.Text = "Conexión cerrada.";
                    lblEstadoConexion.ForeColor = Color.Gray;
                    Log("Puerto cerrado correctamente.");
                }
                else
                {
                    lblEstadoConexion.Text = "No hay conexión activa.";
                    lblEstadoConexion.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                lblEstadoConexion.Text = $"Error al cerrar: {ex.Message}";
                lblEstadoConexion.ForeColor = Color.Red;
                Log($"Error al cerrar: {ex.Message}");
            }
        }

        private void lblResumen_Click(object sender, EventArgs e)
        {
        }

        private void btnEncenderTodos_Click(object sender, EventArgs e)
        {
            if (_ctrl.Dispositivos.Count == 0) return;
            
            _ctrl.EncenderTodos();
            EnviarComando("LED1:ON");
            RefrescarLista(preservarSeleccion: true);
            ActualizarResumen();
        }

        private void btnApagarTodos_Click(object sender, EventArgs e)
        {
            if (_ctrl.Dispositivos.Count == 0) return;
            
            _ctrl.ApagarTodos();
            EnviarComando("LED1:OFF");
            RefrescarLista(preservarSeleccion: true);
            ActualizarResumen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            var nombre = txtNombre.Text?.Trim();
            bool vacio = string.IsNullOrWhiteSpace(nombre);

            // Habilitar/Deshabilitar "Agregar"
            btnAgregar.Enabled = !vacio;

            // Limpiar el error si existe
            errorProvider1.SetError(txtNombre, string.Empty);
        }

        private void lstDispositivos_DoubleClick(object sender, EventArgs e)
        {
            var d = DispositivoSeleccionado();
            if (d is null) return;

            if (d.EstaEncendido)
            {
                d.Apagar();
                EnviarComando("LED1:OFF");
            }
            else
            {
                d.Encender();
                EnviarComando("LED1:ON");
            }

            RefrescarLista(preservarSeleccion: true);
            ActualizarResumen();
        }

        private void lstDispositivos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                btnAlternar.PerformClick();
                e.Handled = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnActualizarPuertos_Click(object sender, EventArgs e)
        {
            CargarPuertos();

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            Conectar();

        }

        private void btnDesconectar_Cilck(object sender, EventArgs e)
        {
            Desconectar();

        }

        private void EnviarComando(string comando)
        {
            try
            {
                // Verifica que el puerto esté abierto
                if (!_sp.IsOpen)
                {
                    Log("Puerto no abierto. No se pudo enviar el comando.");
                    lblEstadoConexion.Text = "Puerto no abierto.";
                    lblEstadoConexion.ForeColor = Color.Red;
                    return;
                }

                // Envía el comando seguido de salto de línea
                _sp.WriteLine(comando);

                // Registra en el log con el prefijo >>
                Log($">> {comando}");
            }
            catch (Exception ex)
            {
                Log($"Error al enviar: {ex.Message}");
                lblEstadoConexion.Text = "Error al enviar.";
                lblEstadoConexion.ForeColor = Color.Red;
            }
        }

        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // Lee una línea completa (hasta encontrar \n)
                string linea = _sp.ReadLine();

                // Elimina espacios y caracteres extraños
                string limpio = linea.Trim();

                // Como DataReceived se ejecuta en un hilo secundario,
                // usamos BeginInvoke para actualizar la interfaz de forma segura
                this.BeginInvoke(new Action(() =>
                {
                    // Registra en el log con el prefijo 
                    Log($"<< {limpio}");

                    // Procesar respuesta de temperatura
                    if (limpio.StartsWith("TEMP:"))
                    {
                        string tempStr = limpio.Substring(5).Trim();
                        if (float.TryParse(tempStr, out float temperatura))
                        {
                            lblTemperaturaValor.Text = $"{temperatura:F2} °C";
                            Log($"Temperatura actualizada: {temperatura:F2} °C");
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new Action(() =>
                {
                    Log($"Error en recepción: {ex.Message}");
                }));
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_sp.IsOpen)
                {
                    // Desconecta el evento para evitar errores
                    _sp.DataReceived -= Sp_DataReceived;

                    // Cierra el puerto
                    _sp.Close();
                }
            }
            catch
            {
                // Ignorar errores al cerrar
            }
        }

        // === Control de Motor ===

        private void trackBarMotor_ValueChanged(object sender, EventArgs e)
        {
            int velocidad = trackBarMotor.Value;
            lblMotorVelocidad.Text = $"Velocidad: {velocidad}";
        }

        private void btnMotorAdelante_Click(object sender, EventArgs e)
        {
            int velocidad = trackBarMotor.Value;
            if (velocidad == 0) velocidad = 150; // valor por defecto
            EnviarComando($"M1:SET:{velocidad}");
            Log($"Motor adelante a velocidad {velocidad}");
        }

        private void btnMotorReversa_Click(object sender, EventArgs e)
        {
            int velocidad = trackBarMotor.Value;
            if (velocidad == 0) velocidad = 150; // valor por defecto
            EnviarComando($"M1:SET:{-velocidad}");
            Log($"Motor reversa a velocidad {velocidad}");
        }

        private void btnMotorStop_Click(object sender, EventArgs e)
        {
            EnviarComando("M1:STOP");
            trackBarMotor.Value = 0;
            Log("Motor detenido");
        }

        // === Sensor de Temperatura ===

        private void btnLeerTemp_Click(object sender, EventArgs e)
        {
            EnviarComando("TEMP?");
            Log("Solicitando temperatura...");
        }
    }
}

